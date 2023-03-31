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
    public class CarsController : Controller
    {
        private readonly AutomasterContext _context;

        public CarsController(AutomasterContext context)
        {
            _context = context;
        }

        // GET: Cars
        
        public async Task<IActionResult> Index(SortState sortOrder = SortState.CarIdAsc)
        {
            IQueryable<Car>? users = _context.Cars;
            ViewData["CarIdSort"] = sortOrder == SortState.CarIdAsc ? SortState.CarIdDesc : SortState.CarIdAsc;
            ViewData["MarkSort"] = sortOrder == SortState.MarkAsc ? SortState.MarkDesc : SortState.MarkAsc;
            ViewData["RegNumberSort"] = sortOrder == SortState.RegNumberAsc ? SortState.RegNumberDesc : SortState.RegNumberAsc;
            ViewData["AdmissionSort"] = sortOrder == SortState.AdmissionAsc ? SortState.AdmissionDesc : SortState.AdmissionAsc;
            ViewData["DefectSort"] = sortOrder == SortState.DefectAsc ? SortState.DefectDesc : SortState.DefectAsc;
            ViewData["EndingSort"] = sortOrder == SortState.EndingAsc ? SortState.EndingDesc : SortState.EndingAsc;
            ViewData["CostSort"] = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;

            users = sortOrder switch
            {
                SortState.CarIdDesc => users.OrderByDescending(s => s.Id),
                SortState.MarkAsc => users.OrderBy(s => s.Mark),
                SortState.MarkDesc => users.OrderByDescending(s => s.Mark),
                SortState.RegNumberAsc => users.OrderBy(s => s.RegNumber),
                SortState.RegNumberDesc => users.OrderByDescending(s => s.RegNumber),
                SortState.AdmissionAsc => users.OrderBy(s => s.Admission),
                SortState.AdmissionDesc => users.OrderByDescending(s => s.Admission),
                SortState.DefectAsc => users.OrderBy(s => s.Defect),
                SortState.DefectDesc => users.OrderByDescending(s => s.Defect),
                SortState.EndingAsc => users.OrderBy(s => s.Ending),
                SortState.EndingDesc => users.OrderByDescending(s => s.Ending),
                SortState.CostAsc => users.OrderBy(s => s.Cost),
                SortState.CostDesc => users.OrderByDescending(s => s.Cost),
                _ => users.OrderBy(s => s.Id),
            };
            return View(await users.AsNoTracking().ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mark,RegNumber,Admission,Defect,Ending,Cost")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mark,RegNumber,Admission,Defect,Ending,Cost")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'AutomasterContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return _context.Cars.Any(e => e.Id == id);
        }
        private string CarById(int id)
        {
            var car = _context.Cars.FirstOrDefault(e => e.Id == id);
            return car.Mark;

        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}