using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class Milk
    {
        public Milk()
        {
            RationCow = new HashSet<RationCow>();
        }

        public int Id { get; set; }
        public double MilkQuantity { get; set; }

        public ICollection<RationCow> RationCow { get; set; }
    }
}
