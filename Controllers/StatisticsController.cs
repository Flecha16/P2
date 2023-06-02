using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Data;
using AuthSystem.Migrations;
using AuthSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Player = AuthSystem.Models.Player;

namespace AuthSystem.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AuthDbContext _context;

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

        // GET: Statistics/Create
        public IActionResult Create()
        {
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            ViewBag.Players = _context.Players.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.FirstName + " " + p.LastName }).ToList();
            return View();
        }

        // POST: Statistics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Statistic statistic)
        {

            // Obtener el jugador seleccionado
            var player = await _context.Players.FindAsync(statistic.PlayerId);

            statistic.TeamId = player.TeamId;
            statistic.Player = player;

            _context.Statistics.Add(statistic);
            _context.SaveChanges();
            return RedirectToAction("Index", "Statistics");

        }

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
                    if (existingStatistic.AverageSpeed == 0)
                    {
                        existingStatistic.AverageSpeed += updatedStatistic.AverageSpeed;
                    }
                    else
                    {
                        existingStatistic.AverageSpeed = (existingStatistic.AverageSpeed + updatedStatistic.AverageSpeed) / 2;
                    }
                    
                    existingStatistic.Km += updatedStatistic.Km;
                    
                    double weightGoals = 0.4;
                    double weightAssists = 0.3;
                    double weightMatchesPlayed = 0.1;
                    double weightTotalKm = 0.1;
                    double weightAverageSpeed = 0.1;

                    // Calcula la media ponderada
                    double playerAverage = (existingStatistic.Goals * weightGoals +
                                     existingStatistic.Assists * weightAssists +
                                     existingStatistic.Matches * weightMatchesPlayed +
                                     (existingStatistic.Km/existingStatistic.Matches) * weightTotalKm +
                                     existingStatistic.AverageSpeed * weightAverageSpeed);

                    // Aplica el límite superior de 100
                    playerAverage = Math.Min(playerAverage, 100);

                    existingStatistic.Player.Valoration = (int) playerAverage;

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

        double CalculatePlayerAverage(int goals, int assists, int matchesPlayed, double totalKm, double averageSpeed)
        {
            // Asigna los pesos para cada estadística
            double weightGoals = 0.4;
            double weightAssists = 0.3;
            double weightMatchesPlayed = 0.1;
            double weightTotalKm = 0.1;
            double weightAverageSpeed = 0.1;

            // Calcula la media ponderada
            double average = (goals * weightGoals +
                             assists * weightAssists +
                             matchesPlayed * weightMatchesPlayed +
                             totalKm * weightTotalKm +
                             averageSpeed * weightAverageSpeed);

            // Aplica el límite superior de 100
            average = Math.Min(average, 100);

            return average;
        }

    }

}