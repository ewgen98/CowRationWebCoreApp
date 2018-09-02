using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CowRationWebApplication.Controllers
{
    public class EconomyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}