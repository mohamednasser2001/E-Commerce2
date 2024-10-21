using E_Commerse2.Data;
using E_Commerse2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerse2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        ApplicationDbContext context = new ApplicationDbContext(); 

        public IActionResult Index()
        {
            var product=context.products.ToList();
            return View(model: product);
        }

        public IActionResult Details( int productId)
        {
            var product = context.products.Find(productId);
            if (product != null)
            {
                return View(model: product);
            }
            else
            {
                return RedirectToAction(nameof(NotFound));
            }
           
        }

        public IActionResult NotFound()
        {
           
            return View();
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
    }
}
