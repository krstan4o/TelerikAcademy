using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class MoviesViewModel:ViewModelBase,IPageViewModel
    {

        private ObservableCollection<MovieViewModel> moviesList;
      
        public string Name
        {
            get { return "Movies View"; }
        }

    


        public IEnumerable<MovieViewModel> MoviesList
        {
            get
            {
                if (this.moviesList == null)
                {
                    this.MoviesList = DataPersister.GetAllMovies();
                }
                return this.moviesList;
            }
            set
            {
                if (this.moviesList == null)
                {
                    this.moviesList = new ObservableCollection<MovieViewModel>();
                }
                this.moviesList.Clear();
                foreach (var item in value)
                {
                    this.moviesList.Add(item);
                }
            }
        }

        public MoviesViewModel()
        {

        }
    }
}
