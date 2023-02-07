using AuthWithRolesWebApp.Models;

namespace AuthWithRolesWebApp.Services
{
    public interface IDBContextFilterSupport
    {
        public AuthDbContext AuthDbContext { get; }
    }
}
