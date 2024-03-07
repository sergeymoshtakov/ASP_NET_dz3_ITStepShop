using Microsoft.AspNetCore.Mvc;

namespace ITStepShop.Controllers
{
    public class UploadCKEDITORController : Controller
    {
        private readonly IWebHostEnvironment _webHost;
        public UploadCKEDITORController(IWebHostEnvironment webHostEnvironment)
        {
            _webHost = webHostEnvironment;
        }

        public string Index()
        {
            return "Hello i`m aploader :)";
        }

        [HttpPost]
        public JsonResult Upload(IFormFile upload) 
        {
            if (upload != null && upload.Length > 0) { 
                var filename = upload.FileName;
                var path = Path.Combine(_webHost.WebRootPath, "uploads" ,filename);
                var str = new FileStream(path, FileMode.Create);
                upload.CopyToAsync(str);
                var url = $"{"/uploads/"}{filename}";
                return Json(new { uploaded = true, url});
            }
            return Json(new { path = "/uploads/" });
        }

        [HttpGet]
        public async Task<IActionResult> FileBrowser(IFormFile upload)
        { 
            var dir = new DirectoryInfo(Path.Combine(_webHost.WebRootPath, "uploads"));
            ViewBag.FilesUploads = dir.GetFiles();
            return View("FileBrowser");
        }
    }
}
