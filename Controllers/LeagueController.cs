using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Models;
using AuthSystem.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AuthSystem.Controllers
{
    public class LeagueController : Controller
    {
        private readonly AuthDbContext _context;

        public LeagueController(AuthDbContext context)
        {
            _context = context;
        }

        [Route("api/[controller]")]
        [Produces("application/json")]
        // GET: api/League
        [HttpGet]
        public ActionResult<IEnumerable<League>> Get()
        {
            var leagues = _context.Leagues.ToList();

            return Ok(leagues);
        }

        public IActionResult Index()
        {
            var leagues = _context.Leagues.ToList();
            return View(leagues);
        }

        [Authorize(Roles = "Admin")]
        // GET: Leagues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Country")] League league)
        {
            if (ModelState.IsValid)
            {
                _context.Add(league);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(league);
        }

        [Authorize(Roles = "Admin")]
        // GET: Leagues/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = _context.Leagues.Find(id);

            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Country")] League league)
        {
            if (id != league.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(league);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueExists(league.Id))
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
            return View(league);
        }

        [Authorize(Roles = "Admin")]
        // GET: Leagues/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var league = _context.Leagues.Find(id);

            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var league = _context.Leagues.Find(id);
            _context.Leagues.Remove(league);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueExists(int id)
        {
            return _context.Leagues.Any(e => e.Id == id);
        }
    }
}

