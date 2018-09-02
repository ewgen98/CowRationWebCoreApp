using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class RationCow
    {
        public int Id { get; set; }
        public int MilkId { get; set; }
        public int KormId { get; set; }
        public double Value { get; set; }

        public Korm Korm { get; set; }
        public Milk Milk { get; set; }
    }
}
