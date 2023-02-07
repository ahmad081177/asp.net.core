using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AuthWithRolesWebApp.Models
{
    public class AppUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        //Navigation properties
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
