using AuthWithRolesWebApp.Context;
using AuthWithRolesWebApp.Misc;
using AuthWithRolesWebApp.Models;
using AuthWithRolesWebApp.Services;
using AuthWithRolesWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace AuthWithRolesWebApp.Controllers
{
    public class AuthController : Controller, IDBContextFilterSupport
    {
        private readonly AuthDbContext dbContext;
        private readonly AppState appState;
        public AuthController(AuthDbContext context, AppState appState)
        {
            this.dbContext = context;
            this.appState = appState;
        }
        public AuthDbContext AuthDbContext => dbContext;

        [UserRoleFilter(AppRoleConstants.ADMIN_ROLE_NAME)]
        public async Task<IActionResult> AllUsers()
        {
            var users = await dbContext.Users.ToListAsync();
            return View(users);
        }
        [UserRoleFilter(AppRoleConstants.ADMIN_ROLE_NAME)]
        public async Task<IActionResult> EditUser(int id)
        {
            List<AppRole> roles = await this.dbContext.Roles.ToListAsync();
            ViewBag.Roles = roles;

            var user = await dbContext.Users.FindAsync(id);
            return View(user);
        }
        [UserRoleFilter(AppRoleConstants.ADMIN_ROLE_NAME)]
        [HttpPost]
        public async Task<IActionResult> EditUser(AppUser model, List<int> roles)
        {
            if(ModelState.IsValid)
            {
                var user = await dbContext.Users.FindAsync(model.Id);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Could not find user!");
                    return View(model);
                }
                else
                {
                    user.Email = model.Email;
                    user.Name= model.Name;
                    user.LastName= model.LastName;
                    user.Phone = model.Phone;
                    
                    //TODO Check for password change, and update hash

                    dbContext.Users.Update(user);
                    dbContext.SaveChanges();
                    
                    //get all roles of the user
                    dbContext.UserRoles.Where(u => u.UserId == user.Id).ExecuteDelete();
                    foreach (var role in roles)
                    {
                        dbContext.UserRoles.Add(new AppUserRole()
                        {
                            RoleId = role,
                            UserId = user.Id
                        });
                    }
                    //Save changes in roles
                    dbContext.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
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
                AppUser? user = dbContext.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError("Email", "User with same email already exists!");
                    return View(model);
                }
                else
                {
                    string hash = Utils.Hash(model.Password);
                    model.PasswordHash = hash;

                    dbContext.Users.Add(model);
                    dbContext.SaveChanges();
                    //
                    var uu = dbContext.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                    if (uu != null)
                    {
                        dbContext.UserRoles.Add(new AppUserRole()
                        {
                            RoleId = AppRoleConstants.USER_ROLE_ID,
                            UserId = uu.Id
                        });
                        dbContext.SaveChanges();
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
                AppUser? user = dbContext.Users.Where(u => u.Email == model.Email && u.PasswordHash == pwd).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email or Password are invalid");
                    return View(model);
                }
                HttpContext.Session.SetString(Constants.SESSION_APP_USER_EMAIL, user.Email);
                HttpContext.Session.SetString(Constants.SESSION_APP_USER_NAME, user.Name + " " + user.LastName);

                appState.CurrentUser=user;

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
            
            appState.CurrentUser = null;
            
            return RedirectToAction("Index", nameof(HomeController).ControllerName());
        }
        [SessionFilter]
        public IActionResult EditCurrentUser()
        {
            AppUser? user = Utils.GetCurrentUser(HttpContext.Session, dbContext);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("AccessDenied", nameof(HomeController).ControllerName());
            }
            
        }
        [HttpPost]
        public IActionResult EditCurrentUser(AppUser model)
        {
            if (ModelState.IsValid)
            {
                model.PasswordHash = Utils.Hash(model.Password);
                dbContext.SaveChanges();
                return RedirectToAction("Index", nameof(HomeController).ControllerName());
            }
            else
            {
                return View(model);
            }
        }

    }
}
