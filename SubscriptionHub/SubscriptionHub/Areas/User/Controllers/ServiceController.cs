using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;
using SubscriptionHub.Models;

namespace SubscriptionHub.Areas.User.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Service Details
        public IActionResult Details(int id)
        {
            Service service = _context.Find<Service>(id)!;

            return View(service);
        }

    }
}
