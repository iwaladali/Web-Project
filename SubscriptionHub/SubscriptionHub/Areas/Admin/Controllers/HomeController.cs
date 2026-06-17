using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Models;
using System.Diagnostics;

namespace SubscriptionHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account", new
                {
                    area="User"
                });
            return View();
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
