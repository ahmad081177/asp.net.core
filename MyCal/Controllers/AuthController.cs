using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCal.Models;
using System.ComponentModel.DataAnnotations;

namespace MyCal.Controllers
{
    public class AuthController : Controller
    {
        private readonly CalDbContext dbContext;

        public AuthController(CalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //check if user exists
            if (ModelState.IsValid)
            {
                var user = await dbContext.AppUsers.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();
                if (user == null)
                {
                    var teacer = await dbContext.AppTeachers.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefaultAsync();
                    if (teacer == null)
                    {
                        ModelState.AddModelError("Email", "Email or Password does not exist");
                        return View();
                    }
                    HttpContext.Session.SetString("email", teacer.Email);
                    HttpContext.Session.SetString("userid", teacer.Id.ToString());
                    HttpContext.Session.SetString("type", "teacer");
                    return RedirectToAction("TeacherCal", "Cal");
                }
                else
                {
                    HttpContext.Session.SetString("email", model.Email);
                    HttpContext.Session.SetString("userid", user.Id.ToString());
                    HttpContext.Session.SetString("type", "user");
                    return RedirectToAction("UserCal", "Cal");
                }
            }
            return View();
        }
    }
}
