using System.ComponentModel.DataAnnotations;

namespace iVideos.Models
{
    public class VideoViewModel
    {
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Video { get; set; }
    }
    public class VideoViewModel2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }
    }
}
