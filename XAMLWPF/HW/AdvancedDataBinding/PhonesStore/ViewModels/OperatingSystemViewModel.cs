using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonesStore.ViewModels
{
    public class OperatingSystemViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Manufacturer { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 23 + ((Name != null) ? this.Name.GetHashCode() : 0);
                result = result * 23 + ((Version != null) ? this.Version.GetHashCode() : 0);
                result = result * 23 + ((Manufacturer != null) ? this.Manufacturer.GetHashCode() : 0);
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as OperatingSystemViewModel;
            if (other == null)
            {
                return false;
            }
            return this.Name.ToLower() == other.Name.ToLower() &&
                   this.Version.ToLower() == other.Version.ToLower() &&
                   this.Manufacturer.ToLower() == other.Manufacturer.ToLower();
        }
    }
}
