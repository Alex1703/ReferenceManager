using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;
using ReferenceManager.App.Models.Enum;

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
        public IActionResult Create(string Id)
        {
            return View();
        }

        // POST: RefArrendadorLocals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RefArrendadorLocal refArrendadorLocal, string CheckCalificacion, string txtComentario1, int txtIdReferencia1, string txtIdUser1)
        {
            try
            {

                _context.Add(refArrendadorLocal);
                await _context.SaveChangesAsync();

                //Se guarda el comentario en el detalle de la comunicacion
                var Comunicacion = new DetalleComunicacion()
                {
                    Descripcion = txtComentario1,
                    FkUsuario = Convert.ToInt32(txtIdUser1),
                    FkListaReferencia = (int)txtIdReferencia1,
                    FkRefArrendadorLocal = refArrendadorLocal.Id
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
            catch (Exception)
            {

                throw;
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
            if (gestion != null)
            {
                gestion.Estado = estado.ToString();
                if (estado == EstadoGestion.EnCola)
                {
                    gestion.FkUsuario = null;
                }
                _context.Update(gestion);
                await _context.SaveChangesAsync();

            }

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
