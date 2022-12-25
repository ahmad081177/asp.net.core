using AuthWebApp.Misc;
using AuthWebApp.Models;
using AuthWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthDbContext context;

        public AuthController(AuthDbContext context)
        {
            this.context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AppUser model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = context.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError("Email", "User with same email already exists!");
                    return View(model);
                }
                else
                {
                    string hash = Utils.Hash(model.Password);
                    model.PasswordHash = hash;
                    context.Users.Add(model);
                    context.SaveChanges();
                    //TODO
                    return RedirectToAction("Login", "Auth");
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pwd = Utils.Hash(model.Password);
                AppUser user = context.Users.Where(u => u.Email == model.Email && u.PasswordHash == pwd).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email or Password are invalid");
                    return View(model);
                }
                HttpContext.Session.SetString(Constants.SESSION_APP_USER_EMAIL, user.Email);
                HttpContext.Session.SetString(Constants.SESSION_APP_USER_NAME, user.Name + " " + user.LastName);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Constants.SESSION_APP_USER_EMAIL);
            HttpContext.Session.Remove(Constants.SESSION_APP_USER_NAME);
            return RedirectToAction("Index", "Home");
        }

    }
}
