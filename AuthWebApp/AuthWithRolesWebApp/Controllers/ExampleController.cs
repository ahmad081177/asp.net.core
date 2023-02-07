using AuthWithRolesWebApp.Misc;
using AuthWithRolesWebApp.Models;
using AuthWithRolesWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthWithRolesWebApp.Controllers
{
    public class ExampleController : Controller, IDBContextFilterSupport
    {
        private readonly AuthDbContext context;

        public ExampleController(AuthDbContext context)
        {
            this.context = context;
        }
        public AuthDbContext AuthDbContext => context;
        public IActionResult AnyUser()
        {
            //This might be null if user didn't log-in
            ViewBag.UserName  = HttpContext.Session.GetString(Constants.SESSION_APP_USER_NAME);
            return View();
        }
        [SessionFilter]
        public IActionResult LoggedInUser()
        {
            //This won't be null, since the action is wrapped by: SessionFilter
            ViewBag.UserName = HttpContext.Session.GetString(Constants.SESSION_APP_USER_NAME);
            return View();
        }
        [UserRoleFilter(AppRoleConstants.ADMIN_ROLE_NAME)]
        public IActionResult AdminUser()
        {
            return View();
        }
    }
}
