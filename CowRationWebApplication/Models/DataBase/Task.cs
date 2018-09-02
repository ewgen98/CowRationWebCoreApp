using System;
using System.Collections.Generic;

namespace CowRationWebApplication
{
    public partial class Task
    {
        public Task()
        {
            FnutritionalCharacteristics = new HashSet<FnutritionalCharacteristics>();
            Ration = new HashSet<Ration>();
        }

        public int IdTask { get; set; }
        public string Task1 { get; set; }

        public ICollection<FnutritionalCharacteristics> FnutritionalCharacteristics { get; set; }
        public ICollection<Ration> Ration { get; set; }
    }
}
