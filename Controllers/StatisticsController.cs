using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Data;
using AuthSystem.Migrations;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Player = AuthSystem.Models.Player;
using Position = AuthSystem.Models.Position;

namespace AuthSystem.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AuthDbContext _context;
        double weightGoals;
        double weightAssists;
        double weightMatchesPlayed;
        double weightTotalKm;
        double weightAverageSpeed;

        public StatisticsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Statistics
        public IActionResult Index()
        {
            var statistics = _context.Statistics.Include(s => s.Player).ThenInclude(p => p.Team).ToList();
            return View(statistics);
        }

        public async Task<IActionResult> Details (int? id)
        {
            var existingStatistic = await _context.Statistics
                .Include(s => s.Player)
                .FirstOrDefaultAsync(s => s.Id == id);
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: Statistics/Create
        public IActionResult Create()
        {
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            ViewBag.Players = _context.Players.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.FirstName + " " + p.LastName }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Statistic statistic)
        {
            // Buscar la estadística existente del jugador
            var existingStatistic = await _context.Statistics.FirstOrDefaultAsync(s => s.PlayerId == statistic.PlayerId);
            var player = await _context.Players.FindAsync(statistic.PlayerId);
            statistic.TeamId = player.TeamId;
            statistic.Player = player;

            if (existingStatistic != null)
            {
                existingStatistic.Matches += statistic.Matches;
                existingStatistic.Minutes += statistic.Minutes;
                existingStatistic.Goals += statistic.Goals;
                existingStatistic.Assists += statistic.Assists;
                existingStatistic.YellowCards += statistic.YellowCards;
                existingStatistic.RedCards += statistic.RedCards;
                existingStatistic.Km += statistic.Km;

                if (existingStatistic.Player.Position == Position.GK)
                {
                    existingStatistic.AverageSpeed += statistic.AverageSpeed;
                }
                else
                {
                    if (existingStatistic.AverageSpeed == 0)
                    {
                        existingStatistic.AverageSpeed += statistic.AverageSpeed;
                    }
                    else
                    {
                        existingStatistic.AverageSpeed = (existingStatistic.AverageSpeed + statistic.AverageSpeed) / 2;
                    }

                }

                if (statistic.Player.Position == Position.LW || statistic.Player.Position == Position.RW ||
                    statistic.Player.Position == Position.CF || statistic.Player.Position == Position.ST)
                {
                    weightGoals = 0.4;
                    weightAssists = 0.3;
                    weightMatchesPlayed = 0.1;
                    weightTotalKm = 0.1;
                    weightAverageSpeed = 0.1;
                }
                else if (statistic.Player.Position == Position.CDM || statistic.Player.Position == Position.CM ||
                    statistic.Player.Position == Position.LM || statistic.Player.Position == Position.RM ||
                    statistic.Player.Position == Position.CAM)
                {
                    weightGoals = 0.2;
                    weightAssists = 0.4;
                    weightMatchesPlayed = 0.2;
                    weightTotalKm = 0.1;
                    weightAverageSpeed = 0.1;

                }
                else if (statistic.Player.Position == Position.CB || statistic.Player.Position == Position.LB ||
                    statistic.Player.Position == Position.RB || statistic.Player.Position == Position.SW)
                {
                    weightGoals = 0.2;
                    weightAssists = 0.3;
                    weightMatchesPlayed = 0.2;
                    weightTotalKm = 0.2;
                    weightAverageSpeed = 0.1;

                }
                else if (statistic.Player.Position == Position.GK)
                {
                    weightGoals = 0.1;
                    weightAssists = 0.1;
                    weightMatchesPlayed = 0.6;
                    weightTotalKm = 0.1;
                    weightAverageSpeed = 0.1;

                }

                // Calcula la media ponderada
                double playerAverage = (existingStatistic.Goals * weightGoals +
                                 existingStatistic.Assists * weightAssists +
                                 existingStatistic.Matches * weightMatchesPlayed +
                                 (existingStatistic.Km / existingStatistic.Matches) * weightTotalKm +
                                 existingStatistic.AverageSpeed * weightAverageSpeed);

                // Aplica el límite superior de 100
                playerAverage = Math.Min(playerAverage, 100);

                existingStatistic.Player.Valoration = (int)playerAverage;
                _context.Update(existingStatistic);
                await _context.SaveChangesAsync();

            }
            else
            {
                // Obtener el jugador seleccionado
                player = await _context.Players.FindAsync(statistic.PlayerId);

                statistic.TeamId = player.TeamId;
                statistic.Player = player;

                weightGoals = 0.4;
                weightAssists = 0.3;
                weightMatchesPlayed = 0.1;
                weightTotalKm = 0.1;
                weightAverageSpeed = 0.1;

                // Calcula la media ponderada
                double playerAverage = (statistic.Goals * weightGoals +
                                 statistic.Assists * weightAssists +
                                 statistic.Matches * weightMatchesPlayed +
                                 (statistic.Km / statistic.Matches) * weightTotalKm +
                                 statistic.AverageSpeed * weightAverageSpeed);

                // Aplica el límite superior de 100
                playerAverage = Math.Min(playerAverage, 100);

                statistic.Player.Valoration = (int)playerAverage;

                _context.Statistics.Add(statistic);
                _context.SaveChanges();
                return RedirectToAction("Index", "Statistics");
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        // GET: Statistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistic = await _context.Statistics.FindAsync(id);
            if (statistic == null)
            {
                return NotFound();
            }

            ViewBag.Players = _context.Players.ToList();
            return View(statistic);
        }

        // POST: Statistics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matches,Minutes,Goals,Assists,YellowCards,RedCards,AverageSpeed,Km")] Statistic updatedStatistic)
        {
            if (id != updatedStatistic.Id)
            {
                return NotFound();
            }
            try
            {
                var existingStatistic = await _context.Statistics
                .Include(s => s.Player)
                .FirstOrDefaultAsync(s => s.Id == id);


                if (existingStatistic != null)
                {
                    existingStatistic.Matches += updatedStatistic.Matches;
                    existingStatistic.Minutes += updatedStatistic.Minutes;
                    existingStatistic.Goals += updatedStatistic.Goals;
                    existingStatistic.Assists += updatedStatistic.Assists;
                    existingStatistic.YellowCards += updatedStatistic.YellowCards;
                    existingStatistic.RedCards += updatedStatistic.RedCards;
                    existingStatistic.Km += updatedStatistic.Km;

                    if (existingStatistic.Player.Position == Position.GK)
                    {
                        existingStatistic.AverageSpeed += updatedStatistic.AverageSpeed;
                    }
                    else
                    {
                        if (existingStatistic.AverageSpeed == 0)
                        {
                            existingStatistic.AverageSpeed += updatedStatistic.AverageSpeed;
                        }
                        else
                        {
                            existingStatistic.AverageSpeed = (existingStatistic.AverageSpeed + updatedStatistic.AverageSpeed) / 2;
                        }
                    }
                    if (existingStatistic.Player.Position == Position.LW || existingStatistic.Player.Position == Position.RW ||
                        existingStatistic.Player.Position == Position.CF || existingStatistic.Player.Position == Position.ST)
                    {
                        weightGoals = 0.4;
                        weightAssists = 0.3;
                        weightMatchesPlayed = 0.1;
                        weightTotalKm = 0.1;
                        weightAverageSpeed = 0.1;
                    }
                    else if (existingStatistic.Player.Position == Position.CDM || existingStatistic.Player.Position == Position.CM ||
                    existingStatistic.Player.Position == Position.LM || existingStatistic.Player.Position == Position.RM ||
                        existingStatistic.Player.Position == Position.CAM)
                    {
                        weightGoals = 0.2;
                        weightAssists = 0.4;
                        weightMatchesPlayed = 0.2;
                        weightTotalKm = 0.1;
                        weightAverageSpeed = 0.1;
                    }
                    else if (existingStatistic.Player.Position == Position.CB || existingStatistic.Player.Position == Position.LB ||
                        existingStatistic.Player.Position == Position.RB || existingStatistic.Player.Position == Position.SW)
                    {
                        weightGoals = 0.2;
                        weightAssists = 0.3;
                        weightMatchesPlayed = 0.2;
                        weightTotalKm = 0.2;
                        weightAverageSpeed = 0.1;

                    }
                    else if (existingStatistic.Player.Position == Position.GK)
                    {
                        weightGoals = 0.1;
                        weightAssists = 0.1;
                        weightMatchesPlayed = 0.6;
                        weightTotalKm = 0.1;
                        weightAverageSpeed = 0.1;

                    }

                    // Calcula la media ponderada
                    double playerAverage = (existingStatistic.Goals * weightGoals +
                                     existingStatistic.Assists * weightAssists +
                                     existingStatistic.Matches * weightMatchesPlayed +
                                     (existingStatistic.Km / existingStatistic.Matches) * weightTotalKm +
                                     existingStatistic.AverageSpeed * weightAverageSpeed);

                    // Aplica el límite superior de 100
                    playerAverage = Math.Min(playerAverage, 100);

                    existingStatistic.Player.Valoration = (int)playerAverage;
                    _context.Update(existingStatistic);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticExists(updatedStatistic.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        // GET: Statistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistic = await _context.Statistics.Include(s => s.Player).FirstOrDefaultAsync(m => m.Id == id);
            if (statistic == null)
            {
                return NotFound();
            }

            return View(statistic);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statistic = await _context.Statistics.FindAsync(id);
            _context.Statistics.Remove(statistic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatisticExists(int id)
        {
            return _context.Statistics.Any(e => e.Id == id);
        }


    }

}