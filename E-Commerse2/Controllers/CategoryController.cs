﻿using E_Commerse2.Data;
using E_Commerse2.Models;
using E_Commerse2.Repository;
using E_Commerse2.Repository.IRepository;
using E_Commerse2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse2.Controllers
{
    [Authorize(Roles =$"{SD.AdminRole},{SD.CommpanyRole}")]
    public class CategoryController : Controller
    {
        //ApplicationDbContext context = new ApplicationDbContext();
        //CategoryRepository categoryRepository=new CategoryRepository();
        ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [AllowAnonymous]//if you went tastasny this of el Authoriz

        public IActionResult Index()
        {
            var categoryes = categoryRepository.GetAll("Products");
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
                //context.categories.Add(category);
                //context.SaveChanges();
                categoryRepository.Create(category);
                categoryRepository.Commit();



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
            var category = categoryRepository.GetAll(e=>e.Id==categoryId);
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
                //context.categories.Update(category);
                //context.SaveChanges();

                categoryRepository.Edit(category);
                categoryRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }

        public IActionResult Delete(int categoryId)
        {
            var category=categoryRepository.GetAll(e=>e.Id==categoryId);
            //context.categories.Remove(category);
            //context.SaveChanges();
            categoryRepository.Delete(category);
            categoryRepository.Commit();
            return RedirectToAction(nameof(Index));

        }
    }
}
