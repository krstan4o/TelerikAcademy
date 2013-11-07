using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

using CinemaReserve.Client.Data;
using CinemaReserve.ResponseModels;
using CinemaReserve.Client.Behavior;

namespace CinemaReserve.Client.ViewModels
{
    using CinemaReserve.Client.Helpers;

    public class CinemasViewModel : ViewModelBase
    {
        private ObservableCollection<CinemaModel> cinemas = new ObservableCollection<CinemaModel>();
        public ObservableCollection<CinemaModel> Cinemas
        {
            get
            {
                var allCinemas = DataPersister.GetCinemas();
                cinemas.UpdateWith(allCinemas);
                return cinemas;
            }
            private set
            {
                cinemas.UpdateWith(value);
            }
        }

        private ObservableCollection<MovieModel> cinemaMovies = new ObservableCollection<MovieModel>();
        public ObservableCollection<MovieModel> CinemaMovies
        {
            get
            {
                var cinema = GetDefaultCinemasView().CurrentItem as CinemaModel;
                if (cinema == null)
                {
                    cinemaMovies.Clear();
                    return cinemaMovies;
                }
                var result = DataPersister.GetCinemaMovies(cinema.Id);
                cinemaMovies.UpdateWith(result);
                return cinemaMovies;
            }
            set
            {
                cinemaMovies.UpdateWith(value);
            }
        }

        public MovieDetailsModel CinemaMovie
        {
            get
            {
                var movie = GetDefaultCinemaMoviesView().CurrentItem as MovieModel;

                if (movie == null)
                {
                    return null;
                }

                var result = DataPersister.GetMovieDetails(movie.Id);
                return result;
            }
        }

        private ObservableCollection<ProjectionModel> cinemaMovieProjections = new ObservableCollection<ProjectionModel>();

        public ObservableCollection<ProjectionModel> CinemaMovieProjections
        {
            get
            {
                var cinema = GetDefaultCinemasView().CurrentItem as CinemaModel;
                var movie = GetDefaultCinemaMoviesView().CurrentItem as MovieModel;

                if (cinema == null || movie == null)
                {
                    cinemaMovieProjections.Clear();
                    return cinemaMovieProjections;
                }

                var result = DataPersister.GetCinemaMovieProjections(cinema.Id, movie.Id);
                cinemaMovieProjections.UpdateWith(result);
                return cinemaMovieProjections;
            }
            set
            {
                cinemaMovieProjections.UpdateWith(value);
            }
        }

        public ProjectionDetailsModel CinemaMovieProjectionsProjection
        {
            get
            {
                var projection = GetDefaultCinemaMovieProjectionsView().CurrentItem as ProjectionModel;

                if (projection == null)
                {
                    return null;
                }

                var result = DataPersister.GetProjectionDetails(projection.Id);
                return result;
            }
        }

        public string CreateReservationEmail { get; set; }

        public string RemoveReservationEmail { get; set; }
        public string RemoveReservationUserCode { get; set; }

        public ICommand CreateReservation { get; private set; }
        public ICommand RemoveReservation { get; private set; }

        public CinemasViewModel()
        {
            GetDefaultCinemasView().CurrentChanged += (sender, e) =>
            {
                OnPropertyChanged("CinemaMovies");
            };

            GetDefaultCinemaMoviesView().CurrentChanged += (sender, e) =>
            {
                OnPropertyChanged("CinemaMovie");
                OnPropertyChanged("CinemaMovieProjections");
            };

            GetDefaultCinemaMovieProjectionsView().CurrentChanged += (sender, e) =>
            {
                OnPropertyChanged("CinemaMovieProjectionsProjection");
            };

            CreateReservationEmail = "doncho1@minkov.it";
            RemoveReservationEmail = "doncho2@minkov.it";
            RemoveReservationUserCode = "12345";

            CreateReservation = new RelayCommand(HandleCreateReservationCommand);
            RemoveReservation = new RelayCommand(HandleRemoveReservationCommand);
        }

        private ICollectionView GetDefaultCinemasView()
        {
            var view = CollectionViewSource.GetDefaultView(this.cinemas);
            return view;
        }

        private ICollectionView GetDefaultCinemaMoviesView()
        {
            var view = CollectionViewSource.GetDefaultView(this.cinemaMovies);
            return view;
        }

        private ICollectionView GetDefaultCinemaMovieProjectionsView()
        {
            var view = CollectionViewSource.GetDefaultView(this.cinemaMovieProjections);
            return view;
        }

        private ICollectionView GetDefaultCinemaMovieProjectionsProjectionSeatsView()
        {
            var view = CollectionViewSource.GetDefaultView(this.CinemaMovieProjectionsProjection.Seats);
            return view;
        }

        private void HandleCreateReservationCommand(object parameter)
        {
            var projection = GetDefaultCinemaMovieProjectionsView().CurrentItem as ProjectionModel;

            var reservation = new CreateReservationModel();

            reservation.Email = CreateReservationEmail;

            // TODO: Get selected seats
            reservation.Seats = new[] { new SeatModel() { Row = new Random().Next(5), Column = new Random().Next(5) } };

            var response = DataPersister.CreateReservationForProjection(projection.Id, reservation);

            if (response == null)
            {
                MessageBox.Show("Error creating reservation!");
                return;
            }

            MessageBox.Show("User code: " + response.UserCode);
        }

        private void HandleRemoveReservationCommand(object parameter)
        {
            var projection = GetDefaultCinemaMovieProjectionsView().CurrentItem as ProjectionModel;

            var reservation = new RejectReservationModel()
            {
                Email = RemoveReservationEmail,
                UserCode = RemoveReservationUserCode
            };

            var success = DataPersister.RemoveReservationForProjection(projection.Id, reservation);

            if (!success)
            {
                MessageBox.Show("Error removing reservation!");
                return;
            }

            MessageBox.Show("Reservation successfully removed!");
        }
    }
}
