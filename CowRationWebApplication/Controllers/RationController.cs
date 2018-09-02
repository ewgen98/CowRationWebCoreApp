using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CowRationWebApplication.Controllers
{
    public class RationController : Controller
    {
        private CowRationContext db;
        private List<Korm> kormsSelections;
        public RationController(CowRationContext context)
        {
            db=context;
        }
        public IActionResult Index()
        {
            var model=new RationViewModel
            {
                Korms = db.Korm.Select(k=>new KormGrid{Selected=true, KormName=k.NameKorm, Price=k.PriceKorm }).ToList(),
                LaboratoryResult = new List<LaboratoryGrid>()
            };
            foreach (var item in db.Korm.ToList())
            {

                model.LaboratoryResult.Add(new LaboratoryGrid
                {
                    Name = item.NameKorm,
                    Sv = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.IdKorm).Where(f => f.IdIndex == 3).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0,
                    Sp = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.IdKorm).Where(f => f.IdIndex == 4).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0,
                    Sg = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.IdKorm).Where(f => f.IdIndex == 5).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0,
                    Sk = (db.FnutritionalCharacteristics.Where(k => k.IdKorm == item.IdKorm).Where(f => f.IdIndex == 6).FirstOrDefault() ?? new FnutritionalCharacteristics()).Fvalue??0
                });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Result(int countMilk)
        {
            var result = new ResultRationViewModel();
            result.Rations = db.Korm.ToList().Select(k => new RationGrid { Name = k.NameKorm }).ToList();
            foreach (var item in db.RationCow.Include("Milk").Include("Korm").Where(r => r.Milk.MilkQuantity == countMilk).ToList())
            {
                RationGrid date = result.Rations.Where(r => r.Name == item.Korm.NameKorm).FirstOrDefault();
                if (date != null)
                {
                    result.Rations.Where(r => r.Name == item.Korm.NameKorm).FirstOrDefault().Value = item.Value;
                    result.Rations.Where(r => r.Name == item.Korm.NameKorm).FirstOrDefault().Cost = item.Value * (double) item.Korm.PriceKorm;

                }
            }
            return View(result.Rations);
        }
    }
}