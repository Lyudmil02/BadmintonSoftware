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
    public class CompetesController : Controller
    {
        private readonly BadmintonSoftwareContext _context;

        public CompetesController(BadmintonSoftwareContext context)
        {
            _context = context;
        }

        // GET: Competes
        public async Task<IActionResult> Index()
        {
            var badmintonSoftwareContext = _context.Competes.Include(c => c.Competition).Include(c => c.PlayerOne).Include(c => c.PlayerTwo);
            return View(await badmintonSoftwareContext.ToListAsync());
        }

        // GET: Competes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compete = await _context.Competes
                .Include(c => c.Competition)
                .Include(c => c.PlayerOne)
                .Include(c => c.PlayerTwo)
                .FirstOrDefaultAsync(m => m.CompeteId == id);
            if (compete == null)
            {
                return NotFound();
            }

            return View(compete);
        }

        public async Task<IActionResult> Match(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competes = await _context.Competes
                .Include(x => x.Competition)
                .Include(x => x.PlayerOne)
                .Include(x => x.PlayerTwo)
                .FirstOrDefaultAsync(x => x.CompeteId == id);

            if (competes == null)
            {
                return NotFound();
            }

            return View(competes);
        }



        // GET: Competes/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName");
            ViewData["PlayerOneId"] = new SelectList(_context.Players, "PlayerId", "FullName");
            ViewData["PlayerTwoId"] = new SelectList(_context.Players, "PlayerId", "FullName");
            return View();
        }

        // POST: Competes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompeteId,PlayerOneId,PlayerTwoId,CompetitionId")] Compete compete)
        {
            if (ModelState.IsValid)
            {
                var registerPOne = _context.Registers
                    .Include(x => x.Competition)
                    .Include(x => x.Player)
                    .FirstOrDefault(x => x.PlayerId == compete.PlayerOneId);
                var registerPWto = _context.Registers
                   .Include(x => x.Competition)
                   .Include(x => x.Player)
                   .FirstOrDefault(x => x.PlayerId == compete.PlayerTwoId);
                if (registerPOne != null && registerPWto != null)
                {
                    _context.Add(compete);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else if (registerPOne==null)
                {
                    ViewData["Warning"] = "Player one does not participate in the competition!!";
                    ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", compete.CompetitionId);
                    ViewData["PlayerOneId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerOneId);
                    ViewData["PlayerTwoId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerTwoId);
                    return View(compete);
                }
                else if(registerPWto==null)
                {
                    ViewData["Warning"] = "Player two does not participate in the competition!!";
                    ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", compete.CompetitionId);
                    ViewData["PlayerOneId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerOneId);
                    ViewData["PlayerTwoId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerTwoId);
                    return View(compete);
                }

               
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", compete.CompetitionId);
            ViewData["PlayerOneId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerOneId);
            ViewData["PlayerTwoId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerTwoId);
            return View(compete);
        }

        // GET: Competes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compete = await _context.Competes.FindAsync(id);
            if (compete == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", compete.CompetitionId);
            ViewData["PlayerOneId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerOneId);
            ViewData["PlayerTwoId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerTwoId);
            return View(compete);
        }

        // POST: Competes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompeteId,PlayerOneId,PlayerTwoId,CompetitionId")] Compete compete)
        {
            if (id != compete.CompeteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompeteExists(compete.CompeteId))
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
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "CompetitionName", compete.CompetitionId);
            ViewData["PlayerOneId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerOneId);
            ViewData["PlayerTwoId"] = new SelectList(_context.Players, "PlayerId", "FullName", compete.PlayerTwoId);
            return View(compete);
        }

        

        // GET: Competes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compete = await _context.Competes
                .Include(c => c.Competition)
                .Include(c => c.PlayerOne)
                .Include(c => c.PlayerTwo)
                .FirstOrDefaultAsync(m => m.CompeteId == id);
            if (compete == null)
            {
                return NotFound();
            }

            return View(compete);
        }

        // POST: Competes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compete = await _context.Competes.FindAsync(id);
            _context.Competes.Remove(compete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompeteExists(int id)
        {
            return _context.Competes.Any(e => e.CompeteId == id);
        }
    }
}
