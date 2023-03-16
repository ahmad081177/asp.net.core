using AuthWithRolesWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthWithRolesWebApp.ViewModels
{
    public class EditUserViewModel
    {
        [NotMapped]
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

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirmed { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
    }
}
