using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CowRationWebApplication.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        public AdminController(UserManager<User> userManager)
        {
            this.userManager=userManager;
        }
        public async Task<IActionResult> Users()
        {
            var users=(await userManager.GetUsersInRoleAsync("user")).ToList();
            return View(users);
        }
    }
}