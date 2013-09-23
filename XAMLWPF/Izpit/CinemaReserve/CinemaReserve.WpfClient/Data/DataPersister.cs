using System;
using System.Collections.Generic;
using System.Linq;
using CinemaReserve.WpfClient.ViewModels;
using CinemaReserve.ResponseModels;
using System.Collections.ObjectModel;
namespace CinemaReserve.WpfClient.Data
{
    public static class DataPersister
    {
        private const string BaseServicesUrl = "http://localhost:50971/api/";
      
        internal static IEnumerable<ViewModels.CinemaViewModel> GetCinemaList()
        {
            var cinemaList =
                 HttpRequester.Get<IEnumerable<CinemaModel>>(BaseServicesUrl + "cinemas");

            return cinemaList.AsQueryable()
                .Select(model => new CinemaViewModel()
                {
                    Id = model.Id,
                    CinemaName = model.Name
                });
        }



        internal static IEnumerable<MovieViewModel> GetMoviesByCinemaId(int cinemaId)
        {
            var moviesByCinema = HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "cinemas/" + cinemaId);
            return moviesByCinema.AsQueryable().Select(model => new MovieViewModel()
            {
                Id = model.Id,
                Title = model.Title
            });
        }

        internal static IEnumerable<MovieViewModel> GetAllMovies()
        {
           var moviesList = 
                 HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "movies");

           return moviesList.AsQueryable().Select(model => new MovieViewModel()
           {
               Id = model.Id,
               Title = model.Title
           });
        }

        internal static DetailedMovieViewModel GetMovieInformation(int movieId)
        {
            var movieDetails = HttpRequester.Get<MovieDetailsModel>(BaseServicesUrl + "movies/" + movieId);
            DetailedMovieViewModel detailedMovieViewModel = new DetailedMovieViewModel();
            detailedMovieViewModel.Id=movieDetails.Id;
            detailedMovieViewModel.Title = movieDetails.Title;
            detailedMovieViewModel.Description = movieDetails.Description;
            detailedMovieViewModel.Actors = movieDetails.Actors as ObservableCollection<string>;
            detailedMovieViewModel.Projections = movieDetails.Projections.AsQueryable().Select(x => new ProjectionViewModel() 
            {
                 Id = x.Id,
                 Movie = x.Movie,
                 Cinema=x.Cinema,
                 Time=x.Time
            });
            return detailedMovieViewModel;
        }
    }
}
