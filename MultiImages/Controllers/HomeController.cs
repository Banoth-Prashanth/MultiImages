using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiImages.Controllers
{
    public class HomeController : Controller
    {
        NirmaankaraEntities con=new NirmaankaraEntities();
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadFiles(MultipleMediaRecord model)
        {
            try
            {
                string imagePaths = string.Empty;
                string videoPaths = string.Empty;

                // Process images
                if (model.Images != null && model.Images.Any())
                {
                    var imageDir = Server.MapPath("~/Images/");
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    foreach (var image in model.Images)
                    {
                        var imagePath = Path.Combine(imageDir, image.FileName);
                        image.SaveAs(imagePath);
                        imagePaths += imagePath + ";";
                    }
                }

                // Process videos
                if (model.Videos != null && model.Videos.Any())
                {
                    var videoDir = Server.MapPath("~/Images/");
                    if (!Directory.Exists(videoDir))
                    {
                        Directory.CreateDirectory(videoDir);
                    }

                    foreach (var video in model.Videos)
                    {
                        var videoPath = Path.Combine(videoDir, video.FileName);
                        video.SaveAs(videoPath);
                        videoPaths += videoPath + ";";
                    }
                }

                // Save paths to the model
                model.ImagePaths = imagePaths;
                model.VideoPaths = videoPaths;

                // Here you can save the model to the database if necessary
                // e.g., dbContext.MultipleMediaRecords.Add(model);
                // dbContext.SaveChanges();

                return Json(new { success = true, message = "Files uploaded successfully!" });
            }
            catch
            {
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }
        }
        public ActionResult demo()
        {
            return View();
        }
        public ActionResult demo1()
        {
            return View();
        }

    }
}