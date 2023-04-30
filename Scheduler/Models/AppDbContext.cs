using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AppStudent> Users { get; set; }
    }
}
