using E_Commerse2.Data;
using E_Commerse2.Models;
using E_Commerse2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace E_Commerse2.Controllers
{
    [Authorize(Roles = $"{SD.AdminRole},{SD.CommpanyRole}")]
    public class ProductController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index(int page=1)
        {
            if (page < 1)
            {
                page = 1;
            }
           
              IQueryable<Product> products = context.products.Include(e => e.Category);
            if(page <= products.Count() / 5)
            {
                products = products.Skip((page - 1) * 5).Take(5);
                return View(model: products.ToList());
            }
            return RedirectToAction("NotFound", "Home");
            
           

        }

        public IActionResult Create()
        {
            var categorys = context.categories.ToList()
             .Select(e => new SelectListItem()
             {
                 Text = e.Name,
                 Value = e.Id.ToString()
             });
            ViewBag.Categories = categorys;
            Product product = new Product();

            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product,IFormFile ImgUrl)
        {
            if (ModelState.IsValid)
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
            var categorys = context.categories.ToList();
            ViewBag.Categories = categorys;
            return View(product);


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
