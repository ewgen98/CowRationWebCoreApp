using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CowRationWebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CowRationWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(model.EmailOrLogin, model.Password, false, false);
                if (result.Result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.EmailOrLogin);
                    var userRoles = (await userManager.GetRolesAsync(user)).FirstOrDefault(u => u == "admin");
                    if (userRoles != null)
                    {
                        return RedirectToAction("Users", "Admin");
                    }
                    return RedirectToAction("KormSelect", "Ration");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Created()=>View();

        [HttpPost]
        public async Task<IActionResult> Created(CreatedViewModel model)
        {
            User user=new User {Email=model.Email, UserName=model.Email, OrganizationName=model.OrganizationName };

            var result=await userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user,"user");
                return RedirectToAction("Users","Admin");
            }
            return View(user);
        }
             
    }
}