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
    public class LokacijasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LokacijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lokacijas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lokacija.ToListAsync());
        }

        // GET: Lokacijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacija = await _context.Lokacija
                .FirstOrDefaultAsync(m => m.id == id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return View(lokacija);
        }

        // GET: Lokacijas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lokacijas/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Naziv")] Lokacija lokacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lokacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lokacija);
        }

        // GET: Lokacijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacija = await _context.Lokacija.FindAsync(id);
            if (lokacija == null)
            {
                return NotFound();
            }
            return View(lokacija);
        }

        // POST: Lokacijas/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Naziv")] Lokacija lokacija)
        {
            if (id != lokacija.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lokacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LokacijaExists(lokacija.id))
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
            return View(lokacija);
        }

        // GET: Lokacijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacija = await _context.Lokacija
                .FirstOrDefaultAsync(m => m.id == id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return View(lokacija);
        }

        // POST: Lokacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lokacija = await _context.Lokacija.FindAsync(id);
            _context.Lokacija.Remove(lokacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LokacijaExists(int id)
        {
            return _context.Lokacija.Any(e => e.id == id);
        }
    }
}
