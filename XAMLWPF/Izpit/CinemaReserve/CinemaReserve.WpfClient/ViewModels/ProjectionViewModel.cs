using CinemaReserve.ResponseModels;
using System;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class ProjectionViewModel:ViewModelBase
    {
        private DateTime time;
        public int Id { get; set; }
        
        public MovieModel Movie { get; set; }
        public CinemaModel Cinema { get; set; }

        public DateTime Time 
        {
            get
            {
                return this.time;
            }
            set
            {
                if (this.time != value)
                {
                    this.time = value;
                    this.OnPropertyChanged("Time");
                }
            }
        }
    }
}
