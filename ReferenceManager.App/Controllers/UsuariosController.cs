using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Models;
using System.Security.Cryptography;
using System.Text;

namespace ReferenceManager.App.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DBReferenciasContext _context;
        private readonly IConfiguration _configuration;

        public UsuariosController(DBReferenciasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var dBReferenciasContext = _context.Usuarios.Include(u => u.FkPerfilNavigation);
            return View(await dBReferenciasContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.FkPerfilNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            usuario.CambioContrasena = DateTime.Now.AddMonths(int.Parse(_configuration.GetSection("PasswordChangePeriod").Value));
            CreatePasswordHash(_configuration.GetSection("PasswordDefault").Value, out byte[] passwordHast, out byte[] passwordSalt);
            usuario.Contrasena = passwordHast;
            usuario.ContrasenaKey = passwordSalt;
            usuario.Correo = usuario.Correo.ToLower();
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Nombre", usuario.FkPerfil);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Id", usuario.FkPerfil);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activio,Nombre,Identificacion,Contrasena,ContrasenaKey,EnLinea,Correo,CambioContrasena,FkPerfil")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }


            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["FkPerfil"] = new SelectList(_context.Perfils, "Id", "Id", usuario.FkPerfil);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.FkPerfilNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'DBReferenciasContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHast, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHast = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
