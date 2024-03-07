using ITStepShop.Data;
using ITStepShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ITStepShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Category;
            return View(categoryList);
        }

        //GET Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //GET EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var category = _db.Category.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET Delete
        public IActionResult Delete(int? id)
        {
            if(id==null||id==0) return NotFound();
            var category = _db.Category.Find(id);
            if(category == null) return NotFound();
            return View(category);
        }
        //POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _db.Category.Find(id);
            if (category == null) return NotFound();
            else 
            {
                _db.Category.Remove(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
