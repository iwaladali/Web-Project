using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Data;
using SubscriptionHub.Models;
using System.Diagnostics;

namespace SubscriptionHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        
        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context=context;
        }
        public IActionResult Index()
        {
            var user_id = HttpContext.Session.GetInt32("ID");
            if (user_id == null)
                return RedirectToAction("Login", "Account", new
                {
                    area="User"
                });
            ViewBag.UsersCount = _context.Users.ToList().Count;
            ViewBag.ServicesCount = _context.Services.ToList().Count;
            ViewBag.CategoriesCount = _context.Categories.ToList().Count;
            ViewBag.SubscriptionsCount = _context.Subscriptions.ToList().Count;
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
