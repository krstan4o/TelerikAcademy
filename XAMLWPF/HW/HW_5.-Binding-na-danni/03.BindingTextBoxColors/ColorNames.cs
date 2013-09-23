using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.BindingTextBoxColors
{
    

    public class ColorNames : ObservableCollection<string>
    {
        public ColorNames()
        {
            Add("Red");
            Add("Blue");
            Add("Purple");
            Add("Black");
            Add("Silver");            
        }
    }
}
