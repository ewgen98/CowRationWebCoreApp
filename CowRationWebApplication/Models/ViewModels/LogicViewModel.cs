using CowRationWebApplication.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowRationWebApplication.Models.ViewModels
{
    public class LogicViewModel
    {
        public List<LogicGrid> LogicGrid { get; set; }
        public int MaxDay{get;set;}
    }
}
