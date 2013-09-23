using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class CinemaViewModel
    {
        public int Id { get; set; }

        public string CinemaName { get; set; }
        private ObservableCollection<MovieViewModel> moviesList;
        private ICommand getMoviesInCinema { get; set; }

        public ICommand GetMoviesInCinema
        {
            get
            {
                if (this.getMoviesInCinema == null)
                {
                    this.getMoviesInCinema = new RelayCommand(this.HandleGetMoviesInCinema);
                }
                return this.getMoviesInCinema;
            }
        }

        private void HandleGetMoviesInCinema(object parameter)
        {
            this.MoviesList = DataPersister.GetMoviesByCinemaId(this.Id);
        }

        public IEnumerable<MovieViewModel> MoviesList
        {
            get
            {
                if (this.moviesList == null)
                {
                     this.moviesList = new ObservableCollection<MovieViewModel>();
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


        
    }
}
