using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClientWPF.ViewModels
{
    class SentViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Sent";
            }
        }
    }
}
