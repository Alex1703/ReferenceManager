using Microsoft.AspNetCore.Mvc;
using ReferenceManager.App.Core;
using ReferenceManager.App.Core.Filters;
using ReferenceManager.App.Models;
using System.Diagnostics;

namespace ReferenceManager.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public HomeController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("JWToken") != null)
            {
                var result = _tokenService.ValidateToken(HttpContext.Session.GetString("JWToken"));
                if (result == null)
                {
                    return RedirectToAction("Index", "Auth");
                }
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[HttpGet]
        //public IActionResult GetNotification()
        //{
        //    var notificationRegisterTime = HttpContext.Session.GetString("LastTimeNotified") != null ? Convert.ToDateTime(HttpContext.Session.GetString("LastTimeNotified")) : DateTime.Now;
        //    GestionReferenciaRepository repository = new GestionReferenciaRepository();
        //    var list = repository.ObtenerReferencias();

        //    //UPDATE SESSION FOR GETTING NEWLY ADDED INFORMATION ONLY
        //    HttpContext.Session.SetString("LastTimeNotified", DateTime.Now.ToString());
        //    return Ok(list);
        //}
    }
}