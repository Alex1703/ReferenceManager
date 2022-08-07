using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;

namespace ReferenceManager.App.Controllers
{
    public class RefProveedorsController : Controller
    {
        private readonly DBReferenciasContext _context;

        public RefProveedorsController(DBReferenciasContext context)
        {
            _context = context;
        }

        // GET: RefProveedors
        public async Task<IActionResult> Index()
        {
            return View(await _context.RefProveedors.ToListAsync());
        }

        // GET: RefProveedors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RefProveedors == null)
            {
                return NotFound();
            }

            var refProveedor = await _context.RefProveedors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refProveedor == null)
            {
                return NotFound();
            }

            return View(refProveedor);
        }

        // GET: RefProveedors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RefProveedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConfirmarNombre,Cargo,DireccionProveedor,TelefonoProveedor,TiempoVenta,ProductoVenta,FrecuenciaCompra,PromedioCompra,ValorUltimaCompra,ContadoCredito,PlazoCredito,CupoCredito,PagoCredito,Concepto")] RefProveedor refProveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refProveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(refProveedor);
        }

        // GET: RefProveedors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RefProveedors == null)
            {
                return NotFound();
            }

            var refProveedor = await _context.RefProveedors.FindAsync(id);
            if (refProveedor == null)
            {
                return NotFound();
            }
            return View(refProveedor);
        }

        // POST: RefProveedors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConfirmarNombre,Cargo,DireccionProveedor,TelefonoProveedor,TiempoVenta,ProductoVenta,FrecuenciaCompra,PromedioCompra,ValorUltimaCompra,ContadoCredito,PlazoCredito,CupoCredito,PagoCredito,Concepto")] RefProveedor refProveedor)
        {
            if (id != refProveedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refProveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefProveedorExists(refProveedor.Id))
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
            return View(refProveedor);
        }

        // GET: RefProveedors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RefProveedors == null)
            {
                return NotFound();
            }

            var refProveedor = await _context.RefProveedors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (refProveedor == null)
            {
                return NotFound();
            }

            return View(refProveedor);
        }

        // POST: RefProveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RefProveedors == null)
            {
                return Problem("Entity set 'DBReferenciasContext.RefProveedors'  is null.");
            }
            var refProveedor = await _context.RefProveedors.FindAsync(id);
            if (refProveedor != null)
            {
                _context.RefProveedors.Remove(refProveedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefProveedorExists(int id)
        {
            return _context.RefProveedors.Any(e => e.Id == id);
        }
    }
}
