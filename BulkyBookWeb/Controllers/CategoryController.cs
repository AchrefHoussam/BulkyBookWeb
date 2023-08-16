using BulkyBook.Data;

using BulkyBook.Models;

using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess;

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

        public IActionResult Create()

        {
            return View();
        }
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
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if (id == null || id == 0)
            { return NotFound(); }
            //var categoryFromDB = _context.Categories.Find(id);
            var categoryFromDB = _context.Categories.FirstOrDefault(u => u.ID == id);
                return View(categoryFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)

        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name" , "the display num cant match the name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            } 
            var obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
                

            return View(obj);
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)

        {
            var obj = _context.Categories.Find(id);
            if (obj == null)
                return NotFound();

                _context.Categories.Remove(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
               
         
        }
       
    


    }
}
