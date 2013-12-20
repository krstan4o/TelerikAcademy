using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeAndTravel
{
    public class Wood:Item
    {
        public Wood(string name, Location location)
            :base(name, 2, ItemType.Wood, location)
        {

        }
        public override void UpdateWithInteraction(string interaction)
        {
            if (interaction == "drop" && this.Value > 0)
            {
                this.Value -= 1;
            }
            base.UpdateWithInteraction(interaction);
        }
    }
}
