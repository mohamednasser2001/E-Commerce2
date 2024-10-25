﻿using E_Commerse2.Data;
using E_Commerse2.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerse2.Controllers
{
    public class CompanyController : Controller
    {
        ApplicationDbContext context=new ApplicationDbContext();
        public IActionResult Index()
        {
            var companiess=context.companies.ToList();
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
                context.companies.Add(Company);
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
            var company = context.companies.Find(companyId);
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
                context.companies.Update(company);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(company);

        }
    }
}