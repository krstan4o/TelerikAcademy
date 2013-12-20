using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeAndTravel
{
    class Weapon:Item
    {
        public Weapon(string name, Location location)
            :base(name, 10, ItemType.Weapon, location)
        {
            
        }

    }
}
