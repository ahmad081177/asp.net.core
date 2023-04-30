using AuthWithRolesWebApp.Controllers;
using AuthWithRolesWebApp.Misc;
using AuthWithRolesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthWithRolesWebApp.Services
{
    //Should be defined at Controller level or some method of Controller
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserRoleFilter : Attribute, IActionFilter
    {
        //The role to check with
        public string Role { get; private set; }

        public UserRoleFilter(string role)
        {
            Role = role;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if there is no session which key is "app_user",
            //user will not access to specified action and redirect to login page.
            var userEmail = context.HttpContext.Session.GetString(Constants.SESSION_APP_USER_EMAIL);
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                //Try to take the DBContext from the controller thru IDBContextFilterSupport 
                if ((context.Controller) is IDBContextFilterSupport dbContext)
                {
                    //Check current user
                    var user = dbContext.AuthDbContext.Users.Where(u => u.Email == userEmail).FirstOrDefault();
                    //get current user's role
                    var role = dbContext.AuthDbContext.Roles.Where(r=>r.Name==Role.ToLower()).FirstOrDefault();
                    if (user != null && role != null)
                    {
                        //If user has the specified role
                        var temp = dbContext.AuthDbContext.UserRoles.Where(u => u.UserId == user.Id && u.RoleId == role.Id).FirstOrDefault();
                        if(temp!=null)
                        {
                            //OK
                            return;
                        }
                    }
                }
            }
            //Something bad happens! return access is denied
            context.Result = new RedirectToActionResult("AccessDenied", nameof(HomeController).ControllerName(), null);
            return;

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
