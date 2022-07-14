using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Controllers
{
    public class TipoCampoesController : Controller
    {
        private readonly DBReferenciasContext _context;

        public TipoCampoesController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: TipoCampoes
        public async Task<IActionResult> Index()
        {
              return _context.TipoCampos != null ? 
                          View(await _context.TipoCampos.ToListAsync()) :
                          Problem("Entity set 'DBReferenciasContext.TipoCampos'  is null.");
        }

        // GET: TipoCampoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoCampos == null)
            {
                return NotFound();
            }

            var tipoCampo = await _context.TipoCampos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCampo == null)
            {
                return NotFound();
            }

            return View(tipoCampo);
        }

        // GET: TipoCampoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCampoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activio,Nombre,Descripcon")] TipoCampo tipoCampo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCampo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCampo);
        }

        // GET: TipoCampoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoCampos == null)
            {
                return NotFound();
            }

            var tipoCampo = await _context.TipoCampos.FindAsync(id);
            if (tipoCampo == null)
            {
                return NotFound();
            }
            return View(tipoCampo);
        }

        // POST: TipoCampoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,Nombre,Descripcon")] TipoCampo tipoCampo)
        {
            if (id != tipoCampo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCampo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCampoExists(tipoCampo.Id))
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
            return View(tipoCampo);
        }

        // GET: TipoCampoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoCampos == null)
            {
                return NotFound();
            }

            var tipoCampo = await _context.TipoCampos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCampo == null)
            {
                return NotFound();
            }

            return View(tipoCampo);
        }

        // POST: TipoCampoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoCampos == null)
            {
                return Problem("Entity set 'DBReferenciasContext.TipoCampos'  is null.");
            }
            var tipoCampo = await _context.TipoCampos.FindAsync(id);
            if (tipoCampo != null)
            {
                _context.TipoCampos.Remove(tipoCampo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCampoExists(int id)
        {
          return (_context.TipoCampos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
