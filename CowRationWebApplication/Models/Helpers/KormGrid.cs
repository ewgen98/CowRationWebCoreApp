﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowRationWebApplication
{
    public class KormGrid
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
        public string KormName { get; set; }
        public decimal? Price { get; set; }
    }
}
