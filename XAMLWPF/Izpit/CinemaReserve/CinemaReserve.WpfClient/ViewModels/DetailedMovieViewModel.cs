using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class DetailedMovieViewModel:ViewModelBase
    {
        public int Id{ get; set; }
        public string Title{ get; set; }

        private string description;
        private ObservableCollection<string> actors;

        public ObservableCollection<string> Actors 
        {
            get
            {
                return this.actors;
            }
            set
            {
                if (this.actors != value)
                {
                    this.actors = value;
                    this.OnPropertyChanged("Actors");
                }
            }
        }

        public IQueryable<ProjectionViewModel> Projections { get; set; }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnPropertyChanged("Description");
                }
            }
        }
    }
}
