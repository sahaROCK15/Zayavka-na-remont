using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zayavka_na_remont.Models;

namespace Zayavka_na_remont.Controllers
{
    public class FixesController : Controller
    {
        private readonly DbContext _context;

        public FixesController(DbContext context)
        {
            _context = context;
        }

        // GET: Fixes
        public async Task<IActionResult> Index()
        {
              return _context.RequestFix != null ? 
                          View(await _context.RequestFix.ToListAsync()) :
                          Problem("Entity set 'DbContext.RequestFix'  is null.");
        }

        // GET: Fixes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestFix == null)
            {
                return NotFound();
            }

            var requestFix = await _context.RequestFix
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestFix == null)
            {
                return NotFound();
            }

            return View(requestFix);
        }

        // GET: Fixes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,LastName,Post,Auditory,Task")] RequestFix requestFix)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestFix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requestFix);
        }

        // GET: Fixes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestFix == null)
            {
                return NotFound();
            }

            var requestFix = await _context.RequestFix.FindAsync(id);
            if (requestFix == null)
            {
                return NotFound();
            }
            return View(requestFix);
        }

        // POST: Fixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,SecondName,LastName,Post,Auditory,Task")] RequestFix requestFix)
        {
            if (id != requestFix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestFix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestFixExists(requestFix.Id))
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
            return View(requestFix);
        }

        // GET: Fixes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestFix == null)
            {
                return NotFound();
            }

            var requestFix = await _context.RequestFix
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestFix == null)
            {
                return NotFound();
            }

            return View(requestFix);
        }

        // POST: Fixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestFix == null)
            {
                return Problem("Entity set 'DbContext.RequestFix'  is null.");
            }
            var requestFix = await _context.RequestFix.FindAsync(id);
            if (requestFix != null)
            {
                _context.RequestFix.Remove(requestFix);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestFixExists(int id)
        {
          return (_context.RequestFix?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
