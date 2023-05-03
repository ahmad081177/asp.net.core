namespace MyCal.Models
{
    public class AppTeacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public List<int> UserIds{ get; set; }
        public virtual List<AppUser> Users { get; set; }
    }

}
