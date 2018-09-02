using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class FnutritionalCharacteristics
    {
        public int IdTask { get; set; }
        public int IdCategory { get; set; }
        public int IdKorm { get; set; }
        public int IdIndex { get; set; }
        public double? Fvalue { get; set; }

        public NutritionalCategories IdCategoryNavigation { get; set; }
        public CatalogIndexNutritional IdIndexNavigation { get; set; }
        public Korm IdKormNavigation { get; set; }
        public Task IdTaskNavigation { get; set; }
    }
}
