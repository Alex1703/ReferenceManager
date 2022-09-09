using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;
using ReferenceManager.App.Models.Enum;

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
        public IActionResult Create(string Id)
        {
            return View();
        }

        // POST: RefProveedors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RefProveedor refProveedor, string CheckCalificacion, string txtComentario1, int txtIdReferencia1, string txtIdUser1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(refProveedor);
                    await _context.SaveChangesAsync();



                    //Se guarda el comentario en el detalle de la comunicacion
                    var Comunicacion = new DetalleComunicacion()
                    {
                        Descripcion = txtComentario1,
                        FkUsuario = Convert.ToInt32(txtIdUser1),
                        FkListaReferencia = (int)txtIdReferencia1,
                        FkRefProveedor = refProveedor.Id
                    };
                    _context.Add(Comunicacion);
                    await _context.SaveChangesAsync();


                    //Se cambia el estado de la referencia 
                    var estado = CheckCalificacion == "Buena" ? EstadoGestion.Buena : EstadoGestion.Deficiente;
                    await ChangeRefercesStatus(Convert.ToInt32(txtIdReferencia1), estado);

                    //Se cambia el estado de la asignacion 

                    await ChangeManagementStatus(Convert.ToInt32(txtIdReferencia1), Convert.ToInt32(txtIdUser1), EstadoGestion.Gestionada);

                    var idCliente = _context.ListaReferencia.FirstOrDefault(x => x.Id == (int)txtIdReferencia1).FkCliente;

                    return RedirectToAction(nameof(Create), "ListaReferenciums", new { idCliente = idCliente });
                    
                }
            }
            catch (Exception)
            {

                throw;
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

        //txtComentario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveComentario(string txtComentario, int txtIdReferencia, string slTipificacion, string txtIdUser)
        {
            //Se guarda el comentario en el detalle de la comunicacion
            var Comunicacion = new DetalleComunicacion()
            {
                Descripcion = slTipificacion + "|" + txtComentario,
                FkUsuario = Convert.ToInt32(txtIdUser),
                FkListaReferencia = txtIdReferencia
            };
            _context.Add(Comunicacion);
            await _context.SaveChangesAsync();

            //Se cambia el estado de la referencia
            if (slTipificacion == "Dificil ubicacion")
            {
                await ChangeRefercesStatus((int)txtIdReferencia, EstadoGestion.Cambio);
                await ChangeManagementStatus(Convert.ToInt32(txtIdReferencia), Convert.ToInt32(txtIdUser), EstadoGestion.Gestionada);
            }
            else //Se reasigna la solictud 
            {
                await ChangeManagementStatus(Convert.ToInt32(txtIdReferencia), Convert.ToInt32(txtIdUser), EstadoGestion.EnCola);
            }

            return RedirectToAction("index", "Home");
        }

        public async Task ChangeManagementStatus(int idReferencia, int idUser, EstadoGestion estado)
        {
            var gestion = _context.GestionReferencia.FirstOrDefault(x => x.FkListaReferencia == idReferencia && x.FkUsuario == idUser && x.Estado == EstadoGestion.Asignado.ToString());
            gestion.Estado = estado.ToString();
            if (estado == EstadoGestion.EnCola)
            {
                gestion.FkUsuario = null;
            }
            _context.Update(gestion);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeRefercesStatus(int idReferencia, EstadoGestion estado)
        {
            var referencia = _context.ListaReferencia.FirstOrDefault(x => x.Id == idReferencia);
            referencia.Estado = estado.ToString();
            _context.Update(referencia);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public ActionResult GetDataReferencia(string IdReferencia)
        {
            var list = _context.ListaReferencia.Include(x => x.FkClienteNavigation).Include(x => x.FkTipoReferenciaNavigation).FirstOrDefault(x => x.Id == Convert.ToInt32(IdReferencia));
            return Json(list);
        }
    }
}
