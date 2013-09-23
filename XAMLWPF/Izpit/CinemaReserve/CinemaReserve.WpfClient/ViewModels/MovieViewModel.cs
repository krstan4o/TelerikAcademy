using CinemaReserve.WpfClient.Behavior;
using CinemaReserve.WpfClient.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaReserve.WpfClient.ViewModels
{
    public class MovieViewModel:ViewModelBase
    {

        public int Id { get; set; }

        public string Title { get; set; }

        private ICommand getDetailedMovie { get; set; }

        private DetailedMovieViewModel detailedMovieInfo;

        public DetailedMovieViewModel DetailedMovieInfo
        {
            get
            {
                return this.detailedMovieInfo;
            }
            set
            {
                if (this.detailedMovieInfo != value)
                {
                    this.detailedMovieInfo = value;
                    this.OnPropertyChanged("DetailedMovieInfo");
                }
            }
        }

        public ICommand GetDetailedMovie
        {
            get
            {
                if (this.getDetailedMovie == null)
                {
                    this.getDetailedMovie = new RelayCommand(this.HandleGetMoviesInCinema);
                }
                return this.getDetailedMovie;
            }
        }

        private void HandleGetMoviesInCinema(object parameter)
        {
            this.DetailedMovieInfo = DataPersister.GetMovieInformation(this.Id);
        }
        public MovieViewModel()
        {

        }
    }
}
