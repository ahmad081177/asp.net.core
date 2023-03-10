using AuthWithRolesWebApp.Misc;
using AuthWithRolesWebApp.Models;
using AuthWithRolesWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthWithRolesWebApp.Controllers
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
            //AppUser model = new()
            //{
            //    UserRoles = new List<AppUserRole>
            //{
            //    new AppUserRole()
            //    {
            //        Name = RoleType.User
            //    }
            //    }
            //};
            //return View(model);
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
                    //
                    var uu = context.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                    if (uu != null)
                    {
                        context.UserRoles.Add(new AppUserRole()
                        {
                            RoleId = AppRoleConstants.USER_ROLE_ID,
                            UserId = uu.Id
                        });
                        context.SaveChanges();
                    }
                    //TODO
                    return RedirectToAction(nameof(Login));
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

                return RedirectToAction("Index", nameof(HomeController).ControllerName());
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
            return RedirectToAction("Index", nameof(HomeController).ControllerName());
        }

    }
}
