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
    public class RefArrendadorLocalsController : Controller
    {
        private readonly DBReferenciasContext _context;

        public RefArrendadorLocalsController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: RefArrendadorLocals
        public async Task<IActionResult> Index()
        {
              return View(await _context.RefArrendadorLocals.ToListAsync());
        }

        // GET: RefArrendadorLocals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RefArrendadorLocals == null)
            {
                return NotFound();
            }

            var refArrendadorLocal = await _context.RefArrendadorLocals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refArrendadorLocal == null)
            {
                return NotFound();
            }

            return View(refArrendadorLocal);
        }

        // GET: RefArrendadorLocals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RefArrendadorLocals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QueArrienda,TiempoArriendo,TipoNegocio,CantidadEmpleados,DireccionLocal,QuienVive,EstadoCivil,NombreConyuge,CanonArriendo,IncluyeServicios,PuntualResponsable,Concepto")] RefArrendadorLocal refArrendadorLocal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refArrendadorLocal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(refArrendadorLocal);
        }

        // GET: RefArrendadorLocals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RefArrendadorLocals == null)
            {
                return NotFound();
            }

            var refArrendadorLocal = await _context.RefArrendadorLocals.FindAsync(id);
            if (refArrendadorLocal == null)
            {
                return NotFound();
            }
            return View(refArrendadorLocal);
        }

        // POST: RefArrendadorLocals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QueArrienda,TiempoArriendo,TipoNegocio,CantidadEmpleados,DireccionLocal,QuienVive,EstadoCivil,NombreConyuge,CanonArriendo,IncluyeServicios,PuntualResponsable,Concepto")] RefArrendadorLocal refArrendadorLocal)
        {
            if (id != refArrendadorLocal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refArrendadorLocal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefArrendadorLocalExists(refArrendadorLocal.Id))
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
            return View(refArrendadorLocal);
        }

        // GET: RefArrendadorLocals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RefArrendadorLocals == null)
            {
                return NotFound();
            }

            var refArrendadorLocal = await _context.RefArrendadorLocals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refArrendadorLocal == null)
            {
                return NotFound();
            }

            return View(refArrendadorLocal);
        }

        // POST: RefArrendadorLocals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RefArrendadorLocals == null)
            {
                return Problem("Entity set 'DBReferenciasContext.RefArrendadorLocals'  is null.");
            }
            var refArrendadorLocal = await _context.RefArrendadorLocals.FindAsync(id);
            if (refArrendadorLocal != null)
            {
                _context.RefArrendadorLocals.Remove(refArrendadorLocal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefArrendadorLocalExists(int id)
        {
          return _context.RefArrendadorLocals.Any(e => e.Id == id);
        }
    }
}
