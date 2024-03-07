using ITStepShop.Data;
using ITStepShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ITStepShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cart) ?? new List<CartItem>();

            foreach (var item in cartItems)
            {
                var product = _db.Product.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    item.ProductName = product.Name;
                }
            }

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var cart = HttpContext.Session.GetString("Cart");
            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cart) ?? new List<CartItem>();

            var existingItem = cartItems.Find(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem { ProductId = productId, Quantity = 1 });
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));

            return RedirectToAction("Index", "Home");
        }
    }
}
