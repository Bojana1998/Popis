using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Popis.Data;
using Popis.Models;


namespace Popis.Controllers
{
    public class SredstvoesController : Controller
    {
        

        private  ApplicationDbContext _context { get; }

        public SredstvoesController(ApplicationDbContext context)
        {
           this._context = context;
        }

       

        // GET: Sredstvoes
        public async Task<IActionResult> Index()
        {
           
            var applicationDbContext = _context.Sredstvo.Include(s => s.Korisnik).Include(s => s.Lokacija);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("id"),
                                        new DataColumn("Naziv"),
                                        new DataColumn("Opis"),
                                        new DataColumn("DatumNabavke"),
                                        new DataColumn("DatumIsteka") ,
                                        new DataColumn("KorisnikId"),
                                        new DataColumn("LokacijaId") });

            var sredstvoes = from sredstvo in this._context.Sredstvo.Take(10)
                            select sredstvo;

            foreach (var sredstvo in sredstvoes)
            {
                dt.Rows.Add(sredstvo.id, sredstvo.Naziv,sredstvo.Opis, sredstvo.DatumNabavke, sredstvo.DatumIsteka,sredstvo.KorisnikId,sredstvo.LokacijaId);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }


        // GET: Sredstvoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sredstvo = await _context.Sredstvo
                .Include(s => s.Korisnik)
                .Include(s => s.Lokacija)
                .FirstOrDefaultAsync(m => m.id == id);
            if (sredstvo == null)
            {
                return NotFound();
            }

            return View(sredstvo);
        }

        // GET: Sredstvoes/Create
        public IActionResult Create()
        {

            
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "id", "Naziv");
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv");
            return View();
        }

        // POST: Sredstvoes/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Naziv,Opis,DatumNabavke,DatumIsteka,KorisnikId,LokacijaId")] Sredstvo sredstvo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sredstvo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "id", "Naziv", sredstvo.KorisnikId);
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv", sredstvo.LokacijaId);
            return View(sredstvo);
        }

        // GET: Sredstvoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sredstvo = await _context.Sredstvo.FindAsync(id);
            if (sredstvo == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "id", "Naziv", sredstvo.KorisnikId);
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv", sredstvo.LokacijaId);
            return View(sredstvo);
        }

        // POST: Sredstvoes/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Naziv,Opis,DatumNabavke,DatumIsteka,KorisnikId,LokacijaId")] Sredstvo sredstvo)
        {
            if (id != sredstvo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sredstvo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SredstvoExists(sredstvo.id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "id", "Naziv", sredstvo.KorisnikId);
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "id", "Naziv", sredstvo.LokacijaId);
            return View(sredstvo);
        }

        // GET: Sredstvoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sredstvo = await _context.Sredstvo
                .Include(s => s.Korisnik)
                .Include(s => s.Lokacija)
                .FirstOrDefaultAsync(m => m.id == id);
            if (sredstvo == null)
            {
                return NotFound();
            }

            return View(sredstvo);
        }

        // POST: Sredstvoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sredstvo = await _context.Sredstvo.FindAsync(id);
            _context.Sredstvo.Remove(sredstvo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SredstvoExists(int id)
        {
            return _context.Sredstvo.Any(e => e.id == id);
        }

    }
}
