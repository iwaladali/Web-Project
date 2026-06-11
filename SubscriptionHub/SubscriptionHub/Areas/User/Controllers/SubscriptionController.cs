using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult MySubscriptions(string? search, int? categoryId, string? status, decimal? minPrice)
        {
            var subscriptions = _context.Subscriptions
                .Include(s => s.Service)
                .ThenInclude(s => s.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                subscriptions = subscriptions.Where(s => s.Service.Name.Contains(search));
            }

            if (categoryId.HasValue)
            {
                subscriptions = subscriptions.Where(s => s.Service.CategoryID == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                subscriptions = subscriptions.Where(s => s.Status == status);
            }

            if (minPrice.HasValue)
            {
                subscriptions = subscriptions.Where(s => s.Service.Price >= minPrice.Value);
            }

            ViewData["Categories"] = _context.Categories.ToList();

            return View(subscriptions.ToList());
        }
    }
}