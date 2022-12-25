using Microsoft.EntityFrameworkCore;

namespace AuthWebApp.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
    }
}
