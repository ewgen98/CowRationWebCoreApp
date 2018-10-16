using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CowRationWebApplication.Models.Helpers;
using CowRationWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CowRationWebApplication.Extensions;

namespace CowRationWebApplication.Controllers
{
    public class LogicController : Controller
    {
        private CowRationContext db;
        public LogicController(CowRationContext db)
        {
            this.db = db;
        }
        public IActionResult KormLagerraum()=>View(GetSelectedKorm());

        [HttpPost]
        public IActionResult Result(int day)
        {
            var result=new LogicViewModel
            {
                LogicGrid=Calculate(day)
            };
            return View(result);
        }

        private List<LogicGrid> Calculate(int day)
        {
            var rations = HttpContext.Session.Get<List<RationGrid>>("Ration");


            var list = new List<LogicGrid>();
            var numbers = new List<int>();
            foreach (var item in GetSelectedKorm())
            {
                var col = rations.Where(r => r.Name == item.Name).FirstOrDefault().Value;
                int d = (int) ((item.Value * 1000 - (col * 360 * day)) / col);
                list.Add(new LogicGrid
                {
                    Name = item.Name,
                    Day = (int) ((item.Value - (col * day)) / col)
                });


                numbers.Add((int) (item.Value / col));


            }

            day = numbers.Min();
            return list;
        }

        private List<LogicGrid> GetSelectedKorm()
        {
            var rations =HttpContext.Session.Get<List<RationGrid>>("Ration");

            var list=new List<LogicGrid>();
            foreach (var item in db.Storage.Include("Korm").ToList())
            {
                var korm=rations.Where(k=>k.Name==item.Korm.NameKorm).FirstOrDefault();
                if (korm!=null)
                {
                    list.Add(new LogicGrid 
                    {
                         Name=item.Korm.NameKorm,
                         Value=item.Quantity
                    });
                }
            }
            return list;
        }
    }
}