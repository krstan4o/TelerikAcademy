using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CountriesSystem.ViewModels
{
    public class DataPersister
    {
        public static IEnumerable<CountryViewModel> GetAll(string countriesSystemDocumentPath)
        {
            var countriesDocumentRoot = XDocument.Load(countriesSystemDocumentPath).Root;

            var countriesVMs = from countryElement in countriesDocumentRoot.Elements("country")
                               select new CountryViewModel()
                               {
                                   Name = countryElement.Element("name").Value,
                                   Language = countryElement.Element("language").Value,
                                   FlagPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), countryElement.Element("flag").Value),
                                   Towns = from townElement in countryElement.Elements("town")
                                           select new TownViewModel()
                                           {
                                               Name = townElement.Element("name").Value,
                                               Population = int.Parse(townElement.Element("population").Value)
                                           }
                               };

            return countriesVMs;
        }
    }
}
