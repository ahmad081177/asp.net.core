using AuthWebApp.Misc;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApp.Controllers
{
    public class ExampleController : Controller
    {
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
    }
}
