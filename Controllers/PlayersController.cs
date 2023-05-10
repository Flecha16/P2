using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Models;
using AuthSystem.Migrations;
using Player = AuthSystem.Models.Player;

namespace AuthSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AuthDbContext _context;

        public PlayerController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players
                .Include(p => p.League)
                .Include(p => p.Team)
                .ToListAsync();

            return View(players);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewBag.LeagueId = new SelectList(_context.Leagues, "Id", "Name");
            ViewBag.TeamId = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            var team = await _context.Teams.Include(t => t.League).FirstOrDefaultAsync(t => t.Id == player.TeamId);

            if (team != null)
            {
                player.Team = team;  // Asignar el objeto Team directamente
                player.League = team.League;
            }

            // Convertir la cadena de fecha a DateTime
            player.DateOfBirth = DateTime.Parse(player.DateOfBirth.ToShortDateString());

            _context.Add(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            ViewBag.LeagueId = new SelectList(_context.Leagues, "Id", "Name", player.LeagueId);
            ViewBag.TeamId = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,Nationality,Position,Valoration,LeagueId,TeamId")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
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

            ViewBag.LeagueId = new SelectList(_context.Leagues, "Id", "Name", player.LeagueId);
            ViewBag.TeamId = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.League)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
