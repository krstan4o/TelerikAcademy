using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReserve.Client.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows.Input;

    using CinemaReserve.Client.Behavior;
    using CinemaReserve.Client.Data;
    using CinemaReserve.Client.Helpers;
    using CinemaReserve.ResponseModels;

    public class MoviesViewModel : ViewModelBase
    {
        private ObservableCollection<MovieModel> movies = new ObservableCollection<MovieModel>();

        public ObservableCollection<MovieModel> Movies
        {
            get
            {
                return this.movies;
            }
            set
            {
                this.movies.UpdateWith(value);
            }
        }

        public MovieDetailsModel Movie
        {
            get
            {
                var movie = GetDefaultMoviesView().CurrentItem as MovieModel;
                var result = DataPersister.GetMovieDetails(movie.Id);
                return result;
            }
        }

        public string SearchQuery { get; set; }

        public ICommand SearchMoviesByTitleOrDescription { get; private set; }

        public MoviesViewModel()
        {
            var allMovies = DataPersister.GetMovies();
            Movies = new ObservableCollection<MovieModel>(allMovies);

            GetDefaultMoviesView().CurrentChanged += (sender, e) =>
            {
                OnPropertyChanged("Movie");
            };

            SearchMoviesByTitleOrDescription = new RelayCommand(HandleSearchMoviesByTitleOrDescriptionCommand);
            // parameter => !string.IsNullOrEmpty(SearchQuery)
        }

        private ICollectionView GetDefaultMoviesView()
        {
            var view = CollectionViewSource.GetDefaultView(this.Movies);
            return view;
        }

        private void HandleSearchMoviesByTitleOrDescriptionCommand(object parameter)
        {
            var foundMovies = DataPersister.SearchMoviesByTitleOrDescription(SearchQuery);

            if (foundMovies == null)
            {
                foundMovies = DataPersister.GetMovies();
            }

            this.Movies = new ObservableCollection<MovieModel>(foundMovies);

            OnPropertyChanged("Movie");
            OnPropertyChanged("Movies");
        }
    }
}
