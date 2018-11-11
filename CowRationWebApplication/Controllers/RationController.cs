using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CowRationWebApplication.Extensions;

namespace CowRationWebApplication.Controllers
{
    public class RationController : Controller
    {
        private CowRationContext db;
        private List<RationGrid> selectKorms=new List<RationGrid> ();
        public RationController(CowRationContext context)
        {
            db=context;
        }

        public IActionResult KormSelect()
        {
            var korms=db.Korm.Select(k=>new KormGrid{Id=k.IdKorm, Selected=true, KormName=k.NameKorm, Price=k.PriceKorm }).ToList();
            return View(korms);
        }

         [HttpPost]
        public IActionResult KormCategory(int [] selectedKorms, int cowCount)
        {
            var kormsSelections=new List<RationGrid>();
            foreach (var id in selectedKorms)
            {
                var korm=db.Korm.Where(k=>k.IdKorm==id).FirstOrDefault();
                kormsSelections.Add(new RationGrid
                {
                    Name=korm.NameKorm,
                    Id=korm.IdKorm
                });
                selectKorms.Add(new RationGrid{
                    Name=korm.NameKorm,
                    Id=korm.IdKorm
                });
            }
            HttpContext.Session.Set("SelectKorms",kormsSelections);
            HttpContext.Session.Set<int>("CowCount",cowCount);
            return View();
        }
        
         [HttpPost]
        public IActionResult KormLaboratory(int countMilk)
        {
            HttpContext.Session.Set<int>("CountMilk",countMilk);
            var laboratoryResult = new List<LaboratoryGrid>();
            var kormsSelections=HttpContext.Session.Get<List<RationGrid>>("SelectKorms");
            foreach (var item in kormsSelections)
            {

                laboratoryResult.Add(new LaboratoryGrid
                {
                    Name = item.Name,
                    Sv = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.Id).Where(f => f.IdIndex == 3).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0,
                    Sp = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.Id).Where(f => f.IdIndex == 4).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0,
                    Sg = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.Id).Where(f => f.IdIndex == 5).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0,
                    Sk = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.Id).Where(f => f.IdIndex == 6).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0
                });
            }
            return View(laboratoryResult);
        }

        [HttpPost]
        public IActionResult Result()
        {
            var kormsSelections=HttpContext.Session.Get<List<RationGrid>>("SelectKorms");
            var countMilk=HttpContext.Session.Get<int>("CountMilk");
            foreach (var item in db.RationCow.Include("Milk").Include("Korm").Where(r => r.Milk.MilkQuantity == countMilk).ToList())
            {
                RationGrid date = kormsSelections.Where(r => r.Name == item.Korm.NameKorm).FirstOrDefault();
                if (date != null)
                {
                    kormsSelections.Where(r => r.Name == item.Korm.NameKorm).FirstOrDefault().Value = item.Value;
                    kormsSelections.Where(r => r.Name == item.Korm.NameKorm).FirstOrDefault().Cost = item.Value * (double) item.Korm.PriceKorm;

                }
            }

            HttpContext.Session.Set("Ration",kormsSelections);

            return View(kormsSelections);
        }
    }
}