using System.Text;

namespace AuthWebApp.Misc
{
    public class Utils
    {
        public static string Hash(string str)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create().
                ComputeHash(Encoding.UTF8.GetBytes(str))
                   );
        }
    }
}
