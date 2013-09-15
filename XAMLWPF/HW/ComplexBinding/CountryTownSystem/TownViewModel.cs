using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryTownSystem
{
    public class TownViewModel
    {
        public string Name { get; set; }

        public int Population { get; set; }

        public CountryViewModel Country { get; set; }
    }
}
