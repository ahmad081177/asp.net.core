using iVideos.Models;
using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit;

namespace iVideos
{
    public class VideosUtils
    {
        public static void SaveVideoThumbnail(string fname)
        {
            var video = new MediaFile
            {
                Filename = fname
            };
            var image = new MediaFile { Filename = fname + ".jpg" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(video);
                engine.GetThumbnail(video, image,
                    new ConversionOptions { Seek = TimeSpan.FromSeconds(video.Metadata.Duration.TotalSeconds / 2) });
            }
            //var thumbnail = File.ReadAllBytes(image.Filename);
            //File.Delete(video.Filename);
            //File.Delete(image.Filename);
            //return thumbnail;
        }
    }
    public class VideosDB
    {
        private static VideosDB the_;
        public List<Video> Videos { get; private set; }
        private VideosDB()
        {
            //In real project - this to be read from DB
            //check for videos
            string dir = Path.Combine(Environment.CurrentDirectory, @"wwwroot\videos");
            //list of dir
            string []files = Directory.GetFiles(dir, "*.mp4");
            Videos = new List<Video>();
            foreach (string file in files)
            {
                Video v = new Video()
                {
                    Id = Random.Shared.Next(),
                    Description = "bla",
                    OwnerId = Random.Shared.Next(),
                    Subject = "as",
                    Title = "title",
                    Path = Path.GetRelativePath(Path.Combine(Environment.CurrentDirectory, @"wwwroot\"), file)
                };
                Videos.Add(v);
            }

        }
        public static VideosDB Instance
        {
            get
            {
                the_ ??= new VideosDB();
                return the_;
            }
        }
        public void Add(Video v)
        {
            Videos.Add(v);
        }
        public void Remove(Video v)
        {
            Videos.Remove(v);
        }
        public void Clear()
        {
            Videos.Clear();
        }
    }
}
