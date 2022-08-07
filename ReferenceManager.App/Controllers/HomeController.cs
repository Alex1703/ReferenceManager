using Microsoft.AspNetCore.Mvc;
using ReferenceManager.App.Core.Filters;
using ReferenceManager.App.Models;
using System.Diagnostics;

namespace ReferenceManager.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
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
    }
}