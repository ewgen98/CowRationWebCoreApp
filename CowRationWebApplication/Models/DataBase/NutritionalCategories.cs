using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class NutritionalCategories
    {
        public NutritionalCategories()
        {
            FnutritionalCharacteristics = new HashSet<FnutritionalCharacteristics>();
        }

        public int IdCategory { get; set; }
        public int IdFaza { get; set; }
        public int Weight { get; set; }
        public int? VolumeOfMilk { get; set; }
        public decimal? FatContent { get; set; }
        public decimal? Protein { get; set; }

        public Faza IdFazaNavigation { get; set; }
        public ICollection<FnutritionalCharacteristics> FnutritionalCharacteristics { get; set; }
    }
}
