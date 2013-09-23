using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesSystem.ViewModels
{
    public class CountryViewModel
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string FlagPath { get; set; }
        public IEnumerable<TownViewModel> Towns { get; set; }
    }
}
