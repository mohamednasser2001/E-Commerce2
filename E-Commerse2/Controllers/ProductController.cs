using E_Commerse2.Data;
using E_Commerse2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse2.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var products=context.products.Include(e=>e.Category).ToList();
            return View(model:products);

        }

        public IActionResult Create()
        {
            var categorys =context.categories.ToList();

            return View(model:categorys);
        }

        [HttpPost]
        public IActionResult Create(Product product,IFormFile ImgUrl)
        {
            if (ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                product.ImgUrl = fileName;
            }


            context.products.Add(product);
            context.SaveChanges();
            return RedirectToAction("Index");



        }

        public IActionResult Delete(int productId)
        {
            Product product=new Product() { Id=productId};
             context.products.Remove(product);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int productId)
        {
            var product = context.products.Find(productId);
            var categores=context.categories.ToList();
            //ViewData["AllCategory"] = categores;
            ViewBag.AllCategory = categores;
            if (product != null)
            {
                return View(model: product);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }

        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile ImgUrl)
        {
            var oldproduct = context.products.AsNoTracking().FirstOrDefault(e => e.Id == product.Id);
            if (ImgUrl !=null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString()+Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                //var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }
             
                product.ImgUrl = fileName;
            }
            else
            {
                product.ImgUrl = oldproduct.ImgUrl;
            }


            context.products.Update(product);
            context.SaveChanges();
            return RedirectToAction("Index");



        }
    }
}
