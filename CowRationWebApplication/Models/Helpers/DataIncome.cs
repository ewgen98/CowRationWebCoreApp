using System.Collections.Generic;

namespace CowRationWebApplication.Models.Helpers
{
    public class DataIncome
    {
        public List<ExpensesGrid> Expenses { get; set; }
        public double ExpensesMilk { get; set; }
        public double ExpensesDay { get; set; }

        public double Receipts { get; set; }
        public double Income { get; set; }
        public double Profitability { get; set; }
        public double UnitProfitability { get; set; }
    }
}
