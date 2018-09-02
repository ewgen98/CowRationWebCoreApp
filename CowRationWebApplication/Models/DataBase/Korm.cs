using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class Korm
    {
        public Korm()
        {
            FnutritionalCharacteristics = new HashSet<FnutritionalCharacteristics>();
            Ration = new HashSet<Ration>();
            RationCow = new HashSet<RationCow>();
            Storage = new HashSet<Storage>();
        }

        public int KormCategory { get; set; }
        public int IdKorm { get; set; }
        public string NameKorm { get; set; }
        public decimal? PriceKorm { get; set; }
        public string Unit { get; set; }
        public string Voluminousness { get; set; }

        public ICollection<FnutritionalCharacteristics> FnutritionalCharacteristics { get; set; }
        public ICollection<Ration> Ration { get; set; }
        public ICollection<RationCow> RationCow { get; set; }
        public ICollection<Storage> Storage { get; set; }
    }
}
