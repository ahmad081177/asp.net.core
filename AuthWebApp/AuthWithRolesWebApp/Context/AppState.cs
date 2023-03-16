using AuthWithRolesWebApp.Models;

namespace AuthWithRolesWebApp.Context
{
    public class AppState
    {
        public AppUser?  CurrentUser { get; set; }
        public string? SomeString { get; set; }
    }
}
