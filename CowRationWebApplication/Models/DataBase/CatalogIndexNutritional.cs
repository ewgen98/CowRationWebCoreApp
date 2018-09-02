using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class CatalogIndexNutritional
    {
        public CatalogIndexNutritional()
        {
            FnutritionalCharacteristics = new HashSet<FnutritionalCharacteristics>();
        }

        public int IdIndex { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public ICollection<FnutritionalCharacteristics> FnutritionalCharacteristics { get; set; }
    }
}
