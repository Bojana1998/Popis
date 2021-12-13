using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Popis.Data;
using Popis.Models;

namespace Popis.Controllers
{
    public class InventarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inventar.Include(i => i.Lokacija);
            return View(await applicationDbContext.ToListAsync());
        }
      



        // GET: Inventars/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar
                .Include(i => i.Lokacija)
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventar == null)
            {
                return NotFound();
            }

            return View(inventar);
        }

        // GET: Inventars/Create
        public IActionResult Create()
        {
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv");
            return View();
        }

        // POST: Inventars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Kreirano,Zavrseno,LokacijaId")] Inventar inventar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv", inventar.LokacijaId);
            return View(inventar);
        }

        // GET: Inventars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar.FindAsync(id);
            if (inventar == null)
            {
                return NotFound();
            }
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv", inventar.LokacijaId);
            return View(inventar);
        }

        // POST: Inventars/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Kreirano,Zavrseno,LokacijaId")] Inventar inventar)
        {
            if (id != inventar.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarExists(inventar.id))
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
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv", inventar.LokacijaId);
            return View(inventar);
        }

        // GET: Inventars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar
                .Include(i => i.Lokacija)
                .FirstOrDefaultAsync(m => m.id == id);
            if (inventar == null)
            {
                return NotFound();
            }

            return View(inventar);
        }

        // POST: Inventars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventar = await _context.Inventar.FindAsync(id);
            _context.Inventar.Remove(inventar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarExists(int id)
        {
            return _context.Inventar.Any(e => e.id == id);
        }
    }
}
