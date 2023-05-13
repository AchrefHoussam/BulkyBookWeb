using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
            
        {
            IEnumerable<Category> categoryList = _context.Categories ;
            return View(categoryList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)

        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("customError" , "the name and the display order should be different");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
           
        }
        public IActionResult Create()

        {            
            return View();
        }
    }
}
