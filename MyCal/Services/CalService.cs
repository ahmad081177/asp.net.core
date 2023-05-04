using Microsoft.EntityFrameworkCore;
using MyCal.Models;

namespace MyCal.Services
{
    public class CalService
    {
        private readonly CalDbContext dbContext;
        public CalService(CalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<AppUser>> GetAllUsersAsync(int teacherId)
        {
          var users = await dbContext.AppUsers.Where(u=>u.AppTeacherId == teacherId).ToListAsync();
            return users;
        }
        public async Task<List<Appointment>> GetAppointmentsOfUserAsync(int uid)
        {
            var user = await dbContext.AppUsers.Where(u=>u.Id == uid).FirstOrDefaultAsync();
            if(user == null)
            {
                return new List<Appointment>();
            }
            else
            {
                //TODO - what if the Appointments is not loaded?
                return user.Appointments;
            }
        }
    }
}
