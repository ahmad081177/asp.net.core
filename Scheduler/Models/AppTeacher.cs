using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Scheduler.Models
{
    public class AppTeacher
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirmed { get; set; }

        //Don't show in the UI. In order to get ModelState.IsValid=true we initiated this prop to ""
        public string PasswordHash { get; set; } = "";

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}
