using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using AuthWebApp.Misc;
using AuthWebApp.Controllers;

public class SessionFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        //if there is no session which key is "app_user",
        //user will not access to specified action and redirect to login page.
        var result = context.HttpContext.Session.GetString(Constants.SESSION_APP_USER_EMAIL);
        if (result == null)
        {
            context.Result = new RedirectToActionResult("AccessDenied", nameof(HomeController).ControllerName(), null);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
