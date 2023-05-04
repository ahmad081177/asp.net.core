using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCal.Models;
using System.Text.Json.Serialization;

namespace MyCal.Controllers
{
    public class CalController : Controller
    {
        private readonly CalDbContext dbContext;

        public CalController(CalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> TeacherCal()
        {
            string? sid = HttpContext.Session.GetString("userid");
            var success = int.TryParse(sid, out int uid);
            if (!success)
            {
                return BadRequest("Invalid teacher id format");
            }
            _ = await dbContext.Appointments.ToListAsync(); //load all

            var mystudents = await dbContext.AppUsers.Where(s => s.AppTeacherId == uid).ToListAsync();

            return View(mystudents);
        }
        public IActionResult UserCal()
        {
            string? sid = HttpContext.Session.GetString("userid");
            bool f = int.TryParse(sid, out int uid);
            if (!f)
                return BadRequest("Invalid user ID format");
            _ = dbContext.Appointments.ToList(); //load them
            var user = dbContext.AppUsers.Find(uid);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> SetupMeeting([FromBody] string dt)
        {
            //Add to DB
            var datetime = DateTime.Parse(dt);
            string? uid = HttpContext.Session.GetString("userid");
            if (int.TryParse(uid, out int id))
            {
                var user = await dbContext.AppUsers.Where(u => u.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest("User not found!");
                }
                //send data to db
                Appointment eventData = new Appointment()
                {
                    AppUserId = user.Id,
                    Start = datetime,
                    End = datetime.AddMinutes(30)
                };
                dbContext.Appointments.Add(eventData);
                dbContext.SaveChanges();
                return Ok(System.Text.Json.JsonSerializer.Serialize(eventData).ToLower());
            }
            return BadRequest("User not registered!");
        }
    }
}
