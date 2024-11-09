using E_Commerse2.Data;
using E_Commerse2.Models;
using E_Commerse2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerse2.Controllers
{
    [Authorize(Roles = $"{SD.AdminRole},{SD.CommpanyRole}")]
    public class CompanyController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var companiess = context.Companies.ToList();
            return View(companiess);
        }

        public IActionResult Create()
        {
            //Company Company = new Company();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company Company)
        {
            if (ModelState.IsValid)
            {
                context.Companies.Add(Company);
                context.SaveChanges();


                //TempData["success"] = "Add New Company Success";
                CookieOptions options = new CookieOptions();
                options.Secure = true;
                options.Expires = DateTimeOffset.Now.AddSeconds(4);
                Response.Cookies.Append("Success", "Add New Company Success", options);
                return RedirectToAction(nameof(Index));
            }
            return View(Company);

        }

        public IActionResult Edit(int companyId)
        {
            var company = context.Companies.Find(companyId);
            if (company != null)
            {
                return View(model: company);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }

        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                context.Companies.Update(company);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(company);

        }
    }
}
