using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Data;
using SubscriptionHub.Models;
using SubscriptionHub.Models.DTO;

namespace SubscriptionHub.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginRequest req)
        {
            if (!ModelState.IsValid)
                return View(req);
           
            var user = _context.Users.FirstOrDefault(u => u.Email == req.Email);
            
            if (user == null)
                return View(req);
            
            if(user.Password != req.Password)
                return View(req);

            HttpContext.Session.SetInt32("ID", user.UserID);

            //return RedirectToAction("Index", "Dashboard");
            return RedirectToAction("Profile");
            
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterRequest req)
        {
            if(!ModelState.IsValid)
                return View(req);
            if(req.ConfirmPassword!= req.Password)
                return View(req);

            if(_context.Users.FirstOrDefault( u=>u.Email == req.Email) !=null )
                return View(req);

            SubscriptionHub.Models.User user= new SubscriptionHub.Models.User() ;
            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.Email = req.Email;
            user.Password= req.Password;
            user.isAdmin = false;

            _context.Users.Add(user);
            _context.SaveChanges();

           return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var user_id=HttpContext.Session.GetInt32("ID");
            if(user_id==null)
           return RedirectToAction("Login");

            var user = _context.Users.Find(user_id);

            return View(user);
        }
    }
}