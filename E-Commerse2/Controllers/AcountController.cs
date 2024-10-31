using E_Commerse2.Migrations;
using E_Commerse2.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerse2.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AcountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
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
                   RedirectToAction("Index", "Home");
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
            return RedirectToAction("Logn");

        }
    }


}
