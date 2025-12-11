using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var categories = _manager.CategoryService.GetAllCategories(false);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _manager.CategoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Update(int id)
        {
            var category = _manager.CategoryService.GetCategoryById(id, false);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _manager.CategoryService.UpdateCategory(category.CategoryId, category, true);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _manager.CategoryService.GetCategoryById(id, false);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _manager.CategoryService.DeleteCategory(id, false);
            return RedirectToAction("Index");
        }
    }
}