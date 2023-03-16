using AuthWithRolesWebApp.Models;
using AuthWithRolesWebApp.Services;
using System.Text;

namespace AuthWithRolesWebApp.Misc
{
    public static class Utils
    {
        public static string ControllerName(this string controllerName)
        {
            if (string.IsNullOrWhiteSpace(controllerName)) throw new ArgumentNullException(nameof(controllerName));
            
            if(controllerName.Contains("Controller"))
                return controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            
            return controllerName;
        }

        public static string Hash(string str)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create().
                ComputeHash(Encoding.UTF8.GetBytes(str))
                   );
        }
        public static AppUser? GetCurrentUser(ISession session, AuthDbContext dbContext)
        {
            AppUser user = null;
            var userEmail = session.GetString(Constants.SESSION_APP_USER_EMAIL);
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                user = dbContext.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            }
            return user;
        }
    }
}
