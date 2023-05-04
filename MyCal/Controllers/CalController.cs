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
        public IActionResult TeacherCal()
        {
            string sid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(sid))
            {
                return BadRequest();
            }
            int uid = int.Parse(sid);
            var teacher = dbContext.AppTeachers.Where(t => t.Id == uid).FirstOrDefault();
            if(teacher == null)
            {
                return BadRequest();
            }
            dbContext.Appointments.ToList(); //load all
            var students = dbContext.AppUsers.ToList();
            //get my students
            var mystudents = new List<AppUser>();
            foreach(var s in students)
            {
                if(s.AppTeacherId == teacher.Id)
                {
                    mystudents.Add(s);
                }
            }

            return View(mystudents);
        }
        public IActionResult UserCal()
        {
            string sid = HttpContext.Session.GetString("userid");
            if(string.IsNullOrEmpty(sid))
            {
                return BadRequest();
            }
            int uid = int.Parse(sid);
            dbContext.Appointments.ToList(); //load them
            var user = dbContext.AppUsers.Find(uid);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> SetupMeeting([FromBody] string dt)
        {
            //Add to DB
            var datetime = DateTime.Parse(dt);
            string uid = HttpContext.Session.GetString("userid");
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
