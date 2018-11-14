using CowRationWebApplication.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using CowRationWebApplication.Extensions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace CowRationWebApplication.Controllers
{
    [Authorize(Roles ="user")]
    public class EconomyController : Controller
    {
        private CowRationContext db;
        public EconomyController(CowRationContext context)
        {
            db=context;
        }
        public IActionResult Initialdata()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Characteristics(DataMilk model)
        {
            HttpContext.Session.Set("InitialData",model);
            return View();
        }

        [HttpPost]
        public IActionResult Expenses(DataMilk model)
        {
            var data=HttpContext.Session.Get<DataMilk>("InitialData");
            model.PreviewQuantityMilk=data.PreviewQuantityMilk;
            model.PreviewAverage=data.PreviewAverage;
            model.FatMilkF=data.FatMilkF;

            HttpContext.Session.Set("DataMilk",model);

            var expenses=new List<ExpensesGrid>();
            foreach (var item in db.Expenses.ToList())
            {
                expenses.Add(new ExpensesGrid
                {
                    Article=item.Article,
                    Price=item.Value,
                    Percenent=0
                });
            }
            HttpContext.Session.Set("Expenses",expenses);
            return View(expenses);
        }

        [HttpPost]
        public IActionResult Result(List<ExpensesGrid> expenses)
        {
            int cowCount=HttpContext.Session.Get<int>("CowCount");
            int milkCount=HttpContext.Session.Get<int>("CountMilk");
            var dataMilk=HttpContext.Session.Get<DataMilk>("DataMilk");
            var exp=HttpContext.Session.Get<List<ExpensesGrid>>("Expenses");
            var model=new DataIncome();

            model.ExpensesDay=exp.Sum(p=>p.Price);
            model.ExpensesMilk=cowCount*milkCount*dataMilk.FatMilkF/dataMilk.FatMilk;

            model.Expenses=new List<ExpensesGrid> ();
            foreach (var item in exp)
            {
                item.Percenent=item.Price/model.ExpensesDay*100;
                model.Expenses.Add(item);
            }

            model.Receipts=milkCount*cowCount*
                ((dataMilk.ExtraPrice*dataMilk.ExtraPercent/100)
                +(dataMilk.HigherPrice*dataMilk.HigherPercent/100)
                +(dataMilk.ExtraPrice*dataMilk.FirstPercent/100));
            model.Income=model.Receipts-model.ExpensesDay;
            model.Profitability=(model.Receipts-model.ExpensesDay)/model.ExpensesDay;
            model.UnitProfitability=((model.Receipts-model.ExpensesDay)/model.ExpensesDay)/model.ExpensesMilk;

            return View(model);
        }
    }
}