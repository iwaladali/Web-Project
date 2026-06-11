using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Data;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            ViewData["Categories"] = _context.Categories.ToList();

            return View(services);
        }
    }
}