using Microsoft.EntityFrameworkCore;

namespace AuthWithRolesWebApp.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole() { Id = AppRoleConstants.USER_ROLE_ID, Name = AppRoleConstants.USER_ROLE_NAME},
                new AppRole() { Id = AppRoleConstants.TEACHER_ROLE_ID, Name = AppRoleConstants.TEACHER_ROLE_NAME},
                new AppRole() { Id = AppRoleConstants.ADMIN_ROLE_ID, Name = AppRoleConstants.ADMIN_ROLE_NAME }
               );

            modelBuilder.Entity<AppUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            
            modelBuilder.Entity<AppUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            
            modelBuilder.Entity<AppUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
    }
}
