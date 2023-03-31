using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoRepairShop.Models;

namespace AutoRepairShop.Controllers
{
    public class WorkersController : Controller
    {
        private readonly AutomasterContext _context;

        public WorkersController(AutomasterContext context)
        {
            _context = context;
        }

        // GET: Workers
        
        public async Task<IActionResult> Index(SortState sortOrder = SortState.WorkerIdAsc)
        {
            IQueryable<Worker>? users = _context.Workers;
            ViewData["WorkerIdSort"] = sortOrder == SortState.WorkerIdAsc ? SortState.WorkerIdDesc : SortState.WorkerIdAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            ViewData["SpecSort"] = sortOrder == SortState.SpecAsc ? SortState.SpecDesc : SortState.SpecAsc;
            ViewData["RecruitmentSort"] = sortOrder == SortState.RecruitmentAsc ? SortState.RecruitmentDesc : SortState.RecruitmentAsc;
            
            users = sortOrder switch
            {
                SortState.WorkerIdDesc => users.OrderByDescending(s => s.Id),
                SortState.NameAsc => users.OrderBy(s => s.Name),
                SortState.NameDesc => users.OrderByDescending(s => s.Name),
                SortState.SurnameAsc => users.OrderBy(s => s.Surname),
                SortState.SurnameDesc => users.OrderByDescending(s => s.Surname),
                SortState.SpecAsc => users.OrderBy(s => s.Spec),
                SortState.SpecDesc => users.OrderByDescending(s => s.Spec),
                SortState.RecruitmentAsc => users.OrderBy(s => s.Recruitment),
                SortState.RecruitmentDesc => users.OrderByDescending(s => s.Recruitment),
                _ => users.OrderBy(s => s.Id),
            };
            return View(await users.AsNoTracking().ToListAsync());
        }
        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Spec,Recruitment")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Spec,Recruitment")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Workers == null)
            {
                return NotFound();
            }

            var worker = await _context.Workers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workers == null)
            {
                return Problem("Entity set 'AutomasterContext.Workers'  is null.");
            }
            var worker = await _context.Workers.FindAsync(id);
            if (worker != null)
            {
                _context.Workers.Remove(worker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
          return _context.Workers.Any(e => e.Id == id);
        }
    }
}
