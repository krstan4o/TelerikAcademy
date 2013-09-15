using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CountryTownSystem
{
    public class DataPersister
    {
        public static IEnumerable<CountryViewModel> GetAll(string europePath)
        {
            var countryDocumentRoot = XDocument.Load(europePath).Root;

            var countries =
                           from country in countryDocumentRoot.Elements("country")
                           select new CountryViewModel()
                           {
                               Name = country.Element("name").Value,
                               Language = country.Element("language").Value,
                               FlagPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), country.Element("flag").Value),
                               Towns = from town in country.Element("towns").Elements("town")
                                       select new TownViewModel() 
                                       {
                                           Name=town.Element("name").Value,
                                           Population=int.Parse(town.Element("population").Value)
                                       }
                           };
            return countries;
        }
    }
}
