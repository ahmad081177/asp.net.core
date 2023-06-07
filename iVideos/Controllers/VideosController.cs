using iVideos.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Drawing;
using System.Drawing.Imaging;

namespace iVideos.Controllers
{
    public class VideosController : Controller
    {
       
        [HttpPost]
        public void UpdateVidoeInfo(int vid, double duration, double currentTime)
        {
            //save stuff to DB
            Console.WriteLine(vid);
        }
        public IActionResult GotoVideo(int id)
        {
            //check if the video exists
            var v = VideosDB.Instance.Videos.Where(u=>u.Id==id).FirstOrDefault();
            if(v!=null)
            {
                return View(v);
            }
            return View();
        }
        public IActionResult Index() {
            var viedoes = VideosDB.Instance.Videos;
            List<VideoViewModel2> vms = new List<VideoViewModel2>();
            foreach (var v in viedoes)
            {
                vms.Add(new VideoViewModel2()
                {
                    Description = v.Description,
                    Id = v.Id,
                    Subject = v.Subject,
                    Title = v.Title,
                    ImagePath = v.Path + ".jpg"
                });
            }
            return View(vms); 
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Create(VideoViewModel model)
        {
            //check if model is valid
            if (ModelState.IsValid)
            {
                //save video under wwwroor/videos
                string fname = Path.Combine("videos",model.Video.FileName);
                string fullname = Path.Combine(Environment.CurrentDirectory,"wwwroot", fname);
                //make sure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(fullname));
                //create the file
                MemoryStream stream = new MemoryStream();
                model.Video.CopyTo(stream);
                System.IO.File.WriteAllBytes(fullname, stream.ToArray());
                try
                {
                    //create thumbnail
                    VideosUtils.SaveVideoThumbnail(fullname);
                }
                catch { }
                //save data into "dummy db"
                Video v = new Video();
                v.Subject = model.Subject;
                v.Title = model.Title;
                v.Description = model.Description;
                v.Id = Random.Shared.Next();
                v.Path = fname;

                VideosDB.Instance.Add(v);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
