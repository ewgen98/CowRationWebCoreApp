using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class Faza
    {
        public Faza()
        {
            NutritionalCategories = new HashSet<NutritionalCategories>();
        }

        public int IdFaza { get; set; }
        public string Faza1 { get; set; }

        public ICollection<NutritionalCategories> NutritionalCategories { get; set; }
    }
}
