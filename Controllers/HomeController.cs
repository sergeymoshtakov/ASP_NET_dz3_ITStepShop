using ITStepShop.Data;
using ITStepShop.Models;
using ITStepShop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace ITStepShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product,
                Categories = _db.Category
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            var product = _db.Product
                            .Include(x => x.Category) // Включаємо дані категорії
                            .FirstOrDefault(u => u.Id == id);

            DetailsVM detailsVM = new DetailsVM()
            {
                Product = product,
                Category = product?.Category, // Перевірка на null для продукту
                ExistsInCart = false
            };
            return View(detailsVM);
        }

        public IActionResult AddToCart(int productId)
        {
            List<int> cart = HttpContext.Session.Get<List<int>>("Cart") ?? new List<int>();
            cart.Add(productId);
            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        //CKEditor

        //[HttpPost]
        //public ActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        //{
        //    if (upload.Length <= 0) return null;
        //    //if (!upload.IsImage())
        //    //{
        //    //    var NotImageMessage = "please choose a picture";
        //    //    dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
        //    //    return Json(NotImage);
        //    //}

        //    var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

        //    Image image = Image.FromStream(upload.OpenReadStream());
        //    int width = image.Width;
        //    int height = image.Height;
        //    if ((width > 750) || (height > 500))
        //    {
        //        var DimensionErrorMessage = "Custom Message for error";
        //        dynamic stuff = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + DimensionErrorMessage + "\"}}");
        //        return Json(stuff);
        //    }

        //    if (upload.Length > 500 * 1024)
        //    {
        //        var LengthErrorMessage = "Custom Message for error";
        //        dynamic stuff = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + LengthErrorMessage + "\"}}");
        //        return Json(stuff);
        //    }

        //    var path = Path.Combine(
        //        Directory.GetCurrentDirectory(), "wwwroot/images/CKEditorImages",
        //        fileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        upload.CopyTo(stream);

        //    }

        //    var url = $"{"/images/CKEditorImages/"}{fileName}";
        //    var successMessage = "image is uploaded successfully";
        //    dynamic success = JsonConvert.DeserializeObject("{ 'uploaded': 1,'fileName': \"" + fileName + "\",'url': \"" + url + "\", 'error': { 'message': \"" + successMessage + "\"}}");
        //    return Json(success);
        //}
    }
}
