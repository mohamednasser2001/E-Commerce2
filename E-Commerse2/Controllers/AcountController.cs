using E_Commerse2.Migrations;
using E_Commerse2.Utility;
using E_Commerse2.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Permissions;

namespace E_Commerse2.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AcountController(UserManager<ApplicationUser>userManager,
            SignInManager<ApplicationUser>signInManager,RoleManager<IdentityRole>roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Register()
        {
            if (roleManager.Roles.IsNullOrEmpty())
            {
                await roleManager.CreateAsync(new(SD.AdminRole));
                await roleManager.CreateAsync(new(SD.CustomerRole));
                await roleManager.CreateAsync(new(SD.CommpanyRole));
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUserVM userVM)
        {
            if (ModelState.IsValid) {

                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = userVM.Name,
                    Email = userVM.Email,
                    Address = userVM.Address
                };
                  var result=await userManager.CreateAsync(applicationUser,userVM.Password);
                if (result.Succeeded) {

                    var role=await userManager.AddToRoleAsync(applicationUser, SD.CustomerRole);
                    await signInManager.SignInAsync(applicationUser, false);
                    return  RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "invalid Password");
                }

            }
            return View(userVM);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                var userDb = await userManager.FindByNameAsync(userVM.User);
                if (userDb !=null)
                {
                    var finalResult=await userManager.CheckPasswordAsync(userDb,userVM.Password);
                    if (finalResult)
                    {
                        await signInManager.SignInAsync(userDb, userVM.Remembere);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "InValid Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("User", "inValid User Name");
                }
          
            }
            return View(userVM);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }
    }


}
