using Microsoft.EntityFrameworkCore;

namespace MyCal.Models
{
    public class CalDbContext : DbContext
    {
        public CalDbContext(DbContextOptions<CalDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppTeacher> AppTeachers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
 
    }

}
