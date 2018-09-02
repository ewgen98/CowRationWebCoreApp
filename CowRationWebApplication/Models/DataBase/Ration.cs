using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class Ration
    {
        public int IdRation { get; set; }
        public int IdTask { get; set; }
        public int IdKorm { get; set; }
        public decimal? Heft { get; set; }

        public Korm IdKormNavigation { get; set; }
        public Task IdTaskNavigation { get; set; }
    }
}
