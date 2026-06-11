using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;
using SubscriptionHub.Models;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? search, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var services = _context.Services
                .Include(s => s.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                services = services.Where(s => s.Name.Contains(search));
            }

            if (categoryId.HasValue)
            {
                services = services.Where(s => s.CategoryID == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                services = services.Where(s => s.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                services = services.Where(s => s.Price <= maxPrice.Value);
            }

            ViewData["Categories"] = _context.Categories.ToList();

            return View(services.ToList());
        }

        public IActionResult Details(int id)
        {
            var service = _context.Services
                .Include(s => s.Category)
                .FirstOrDefault(s => s.ServiceID == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
    }
}