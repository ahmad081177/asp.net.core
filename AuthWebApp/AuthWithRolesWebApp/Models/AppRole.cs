using System.Data;

namespace AuthWithRolesWebApp.Models
{
    public class AppRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
    public static class AppRoleConstants
    {
        public const int USER_ROLE_ID = 1;
        public const string USER_ROLE_NAME = "user";

        public const int TEACHER_ROLE_ID = 2;
        public const string TEACHER_ROLE_NAME = "teacher";

        public const int ADMIN_ROLE_ID = 3;
        public const string ADMIN_ROLE_NAME = "admin";
    }
}
