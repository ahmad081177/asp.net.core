namespace iVideos.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Path { get; set; }
        
        public int OwnerId { get; set; }
    }
}
