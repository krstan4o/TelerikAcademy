using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace CinemaReserve.WpfClient.ViewModels
{
    public class CinemasViewModel:ViewModelBase,IPageViewModel
    {
        private string cinemaName;
    
        private ObservableCollection<CinemaViewModel> cinemaList;



        public int Id { get; set; }

        public string Name
        {
            get { return "Cinemas View"; }
        }

        public string CinemaName
        {
            get
            {
                return this.cinemaName;
            }
            set
            {
                if (this.cinemaName != value)
                {
                    this.cinemaName = value;
                    this.OnPropertyChanged("CinemaName");
                }
            }
        }

        public IEnumerable<CinemaViewModel> CinemaList
        {
            get
            {
                if (this.cinemaList == null)
                {
                    this.CinemaList = DataPersister.GetCinemaList();
                }
                return this.cinemaList;
            }
            set
            {
                if (this.cinemaList == null)
                {
                    this.cinemaList = new ObservableCollection<CinemaViewModel>();
                }
                this.cinemaList.Clear();
                foreach (var item in value)
                {
                    this.cinemaList.Add(item);
                }
            }
        }

      
      

        public CinemasViewModel()
        {
                      
        }
     
    }
}
