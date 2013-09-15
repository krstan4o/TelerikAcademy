using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonesStore.ViewModels
{
    public class PhoneViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public string Name
        //{
        //    get
        //    {
        //        return this.name;
        //    }
        //    set
        //    {
        //        this.name = value;
        //        OnPropertyChanged("Name");
        //    }
        //}
        private string model;
        public string Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
                OnPropertyChanged("Model");
            }
        }

        private OperatingSystemViewModel os;
        public OperatingSystemViewModel OS
        {
            get
            {
                return this.os;
            }
            set
            {
                this.os = value;
                OnPropertyChanged("OS");
            }
        }

        private string vendor;
        public string Vendor
        {
            get
            {
                return this.vendor;
            }
            set
            {
                this.vendor = value;
                OnPropertyChanged("Vendor");
            }
        }

        private int year;
        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
                OnPropertyChanged("Year");
            }
        }

        public string ImagePath { get; set; }
        
    }
}
