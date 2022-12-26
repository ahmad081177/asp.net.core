using System.Text;

namespace AuthWebApp.Misc
{
    public static class Utils
    {
        public static string ControllerName(this string controllerName)
        {
            return controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
        }

        public static string Hash(string str)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create().
                ComputeHash(Encoding.UTF8.GetBytes(str))
                   );
        }
    }
}
