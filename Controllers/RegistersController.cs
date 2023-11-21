using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadmintonSoftware.Data;

namespace BadmintonSoftware.Controllers
{
    public class RegistersController : Controller
    {
        private readonly BadmintonSoftwareContext _context;

        public RegistersController(BadmintonSoftwareContext context)
        {
            _context = context;
        }

        // GET: Registers
        public async Task<IActionResult> Index()
        {
            var badmintonSoftwareContext = _context.Registers.Include(r => r.Competition).Include(r => r.Player);
            return View(await badmintonSoftwareContext.ToListAsync());
        }

        // GET: Registers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var register = await _context.Registers
                .Include(r => r.Competition)
                .Include(r => r.Player)
                .FirstOrDefaultAsync(m => m.RegisterId == id);
            if (register == null)
            {
                return NotFound();
            }

            return View(register);
        }

        // GET: Registers/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName");
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName");
            return View();
        }

        // POST: Registers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegisterId,PlayerId,CompetitionId,RegisterDate")] Register register)
        {
            if (ModelState.IsValid)
            {
                register.RegisterDate = DateTime.Now;
                var player = _context.Players.Where(x => x.PlayerId == register.PlayerId).FirstOrDefault();
                var competition = _context.Competitions.Where(x => x.CompetitionId == register.CompetitionId).FirstOrDefault();
                if (player.Age>competition.MaxAge)
                {
                    ViewData["Warning"] = "Player is too old to compete in this competition";

                    ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", register.CompetitionId);
                    ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", register.PlayerId);
                    return View(register);
                }
                else
                {
                    _context.Add(register);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
               
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", register.CompetitionId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", register.PlayerId);
            return View(register);
        }

        // GET: Registers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var register = await _context.Registers.FindAsync(id);
            if (register == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", register.CompetitionId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", register.PlayerId);
            return View(register);
        }

        // POST: Registers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegisterId,PlayerId,CompetitionId,RegisterDate")] Register register)
        {
            if (id != register.RegisterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(register);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterExists(register.RegisterId))
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
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", register.CompetitionId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "FullName", register.PlayerId);
            return View(register);
        }

        // GET: Registers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var register = await _context.Registers
                .Include(r => r.Competition)
                .Include(r => r.Player)
                .FirstOrDefaultAsync(m => m.RegisterId == id);
            if (register == null)
            {
                return NotFound();
            }

            return View(register);
        }

        // POST: Registers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var register = await _context.Registers.FindAsync(id);
            _context.Registers.Remove(register);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterExists(int id)
        {
            return _context.Registers.Any(e => e.RegisterId == id);
        }
    }
}
