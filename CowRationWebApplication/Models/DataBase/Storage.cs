using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class Storage
    {
        public int Id { get; set; }
        public int? KormId { get; set; }
        public double? Quantity { get; set; }

        public Korm Korm { get; set; }
    }
}
