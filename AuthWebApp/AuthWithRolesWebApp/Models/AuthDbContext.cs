using AuthWithRolesWebApp.Misc;
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
            //Add hard-coded roles
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole() { Id = AppRoleConstants.USER_ROLE_ID, Name = AppRoleConstants.USER_ROLE_NAME},
                new AppRole() { Id = AppRoleConstants.TEACHER_ROLE_ID, Name = AppRoleConstants.TEACHER_ROLE_NAME},
                new AppRole() { Id = AppRoleConstants.ADMIN_ROLE_ID, Name = AppRoleConstants.ADMIN_ROLE_NAME }
               );

            //AppUserRole has no single primary key, but both UserId & RoleId and both PKEY
            modelBuilder.Entity<AppUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            //Create the first Admin user
            modelBuilder.Entity<AppUser>().HasData(new AppUser()
            {
                Email="admin@gmail.com",
                Id = 1,
                Name = "Admin",
                LastName = "Manager",
                Password = "admin",
                Phone = "123456",
                PasswordHash = Utils.Hash("admin"),
            });
            //Add the user to Admin role
            modelBuilder.Entity<AppUserRole>().HasData(new AppUserRole()
            {
                RoleId = AppRoleConstants.ADMIN_ROLE_ID,
                UserId = 1
            });
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
    }
}
