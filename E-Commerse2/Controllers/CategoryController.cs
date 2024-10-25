using E_Commerse2.Data;
using E_Commerse2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse2.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var categoryes=context.categories.Include(e=>e.Products).ToList();
            return View(model:categoryes);
           
        }

        public IActionResult Create()
        {
            Category category= new Category();
            return View(category);
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                context.categories.Add(category);
                context.SaveChanges();


                //TempData["success"] = "Add New Category Success";
                CookieOptions options = new CookieOptions();
                options.Secure = true;
                options.Expires = DateTimeOffset.Now.AddSeconds(4);
                Response.Cookies.Append("Success", "Add New Category Success", options);
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }

        public IActionResult Edit( int categoryId)
        {
            var category = context.categories.Find(categoryId);
            if (category != null) {
                return View(model: category);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
           
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                context.categories.Update(category);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }

        public IActionResult Delete(int categoryId)
        {
            Category category=new Category() { Id = categoryId };
            context.categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
