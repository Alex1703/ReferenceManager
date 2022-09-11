using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReferenceManager.App.Core.Filters;
using ReferenceManager.App.Models;
using ReferenceManager.App.Models.Enum;
using ReferenceManager.App.Models.Login;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace ReferenceManager.App.Controllers
{
    public class AuthController : Controller
    {
        private readonly DBReferenciasContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthController(DBReferenciasContext context, IConfiguration configuration, ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Models.Login.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IActionResult response = Unauthorized();
                var usuario = await GetUsuarioByCorreo(model.Username);
                if (usuario == null)
                {
                    TempData["Msg"] = "Usuario no registrado.";
                    return View();
                }
                else if (!VerifyPasswordHash(model.Password, usuario.Contrasena, usuario.ContrasenaKey))
                {
                    TempData["Msg"] = "La contraseña es incorrecta.";
                    return View();
                }

                string token = _tokenService.CreatedToken(usuario);

                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("JWToken", token);

                    var temp = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    HttpContext.Session.SetString("IdUsuario", temp.Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value);
                    HttpContext.Session.SetString("Name", temp.Claims.FirstOrDefault(x => x.Type.Contains("name")).Value);
                    HttpContext.Session.SetString("Actor", temp.Claims.FirstOrDefault(x => x.Type.Contains("actor")).Value);
                }
                ChangeUpdateOnLine(usuario, OnlineStatus.Conectado);
                if (!IsFirstEntry(model.Password))
                {
                    return RedirectToAction("ChangePassword");
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private void ChangeUpdateOnLine(Usuario usuario, OnlineStatus status)
        {
            try
            {
                usuario.EnLinea = status.ToString();
                _context.Update(usuario);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(LoginChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(HttpContext.Session.GetString("JWToken"));
                var idUser = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;
                Usuario User = _context.Usuarios.Include(c => c.FkPerfilNavigation).FirstOrDefault(x => x.Id == int.Parse(idUser));

                User.CambioContrasena = DateTime.Now.AddMonths(int.Parse(_configuration.GetSection("PasswordChangePeriod").Value));
                CreatePasswordHash(model.Password, out byte[] passwordHast, out byte[] passwordSalt);
                User.Contrasena = passwordHast;
                User.ContrasenaKey = passwordSalt;

                _context.Update(User);
                _context.SaveChanges();

                string token = _tokenService.CreatedToken(User);
                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("JWToken", token);
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private async Task<Usuario>? GetUsuarioByCorreo(string correo)
        {
            return await _context.Usuarios
                .Include(u => u.FkPerfilNavigation)
                .Where(u => u.Correo == correo)
                .FirstOrDefaultAsync();

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHast, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHast);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHast, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHast = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool IsFirstEntry(string password)
        {
            return _configuration.GetSection("PasswordDefault").Value != password;
        }

        public async Task<IActionResult> LogOff()
        {
            try
            {
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(HttpContext.Session.GetString("JWToken"));
                var idUser = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "IdUsuario").Value;
                Usuario User = _context.Usuarios.Include(c => c.FkPerfilNavigation).FirstOrDefault(x => x.Id == int.Parse(idUser));

                ChangeUpdateOnLine(User, OnlineStatus.Desconectado);

                HttpContext.Session.Clear();
                return Redirect("Index");
            }
            catch (Exception)
            {
                return Redirect("Index");
            }
        }


    }

}
