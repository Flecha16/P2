using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Models;
using AuthSystem.Migrations;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AuthSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly AuthDbContext _context;

        public TeamController(AuthDbContext context)
        {
            _context = context;
        }

        [Route("api/[controller]")]
        [Produces("application/json")]
        // GET: api/teams
        [HttpGet]
        public ActionResult<IEnumerable<Statistic>> Get()
        {
            var teams = _context.Teams.Include(t => t.League).ToList();

            return Ok(teams);
        }

        // GET: Team
        public IActionResult Index(string leagueName)
        {
            var teams = _context.Teams.Include(t => t.League).ToList();

            if (!string.IsNullOrEmpty(leagueName))
            {
                leagueName = leagueName.ToLower();
                teams = teams.Where(t => t.League.Name.ToLower().Contains(leagueName)).ToList();
            }

            return View(teams);
        }


        [Authorize(Roles = "Admin")]
        // GET: Team/Create
        public IActionResult Create()
        {
            // Load list of leagues into ViewBag.LeagueId
            ViewBag.LeagueId = new SelectList(_context.Leagues, "Id", "Name");

            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            // Obtener la liga seleccionada
            var league = await _context.Leagues.FindAsync(team.LeagueId);

            // Asignar el país de la liga al equipo
            team.Country = league.Country;

            // Guardar el equipo en la base de datos
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewBag.LeagueId = new SelectList(_context.Leagues, "Id", "Name", team.LeagueId);
            var league = await _context.Leagues.FindAsync(team.LeagueId);
            ViewData["LeagueName"] = league.Name;

            return View(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Stadium,City,LeagueId")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            try
            {
                var teamToUpdate = await _context.Teams.FindAsync(id);

                if (teamToUpdate == null)
                {
                    return NotFound();
                }

                // Actualizar solo los campos modificables
                teamToUpdate.Name = team.Name;
                teamToUpdate.Stadium = team.Stadium;
                teamToUpdate.City = team.City;
                teamToUpdate.LeagueId = team.LeagueId;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.Id))
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
        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }

    }
}