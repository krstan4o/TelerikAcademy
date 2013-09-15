using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CountryTownSystem
{
    public class CountryViewModel
    {
        private string europeDocumentPath;

        public string Name { get; set; }

        public string Language { get; set; }

        public string FlagPath { get; set; }

        public IEnumerable<TownViewModel> Towns { get; set; }

        public CountryViewModel()
        {
            this.Towns = new List<TownViewModel>();
            europeDocumentPath = "..\\..\\..\\CountryTownSystem\\europe.xml";
        }
    }
}
