using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using CinemaReserve.ResponseModels;

namespace CinemaReserve.Client.Data
{
    // TODO: Refactor
    internal static class DataPersister
    {
        private const string BaseServicesUrl = "http://localhost:50971/api/";

        #region Cinemas

        public static IEnumerable<CinemaModel> GetCinemas()
        {
            var result = HttpRequester.Get<IEnumerable<CinemaModel>>(BaseServicesUrl + "cinemas");
            return result;
        }

        public static IEnumerable<MovieModel> GetCinemaMovies(int cinemaId)
        {
            if (cinemaId <= 0)
            {
                return null;
            }

            var result = HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "cinemas/" + cinemaId);
            return result;
        }

        public static IEnumerable<ProjectionModel> GetCinemaMovieProjections(int cinemaId, int movieId)
        {
            if (cinemaId <= 0)
            {
                return null;
            }

            if (movieId <= 0)
            {
                return null;
            }

            var result =
                HttpRequester.Get<IEnumerable<ProjectionModel>>(
                    BaseServicesUrl + "cinemas/" + cinemaId + "/projections/" + movieId);
            return result;
        }

        #endregion

        #region Movies

        public static IEnumerable<MovieModel> GetMovies()
        {
            var result = HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "movies");
            return result;
        }

        public static MovieDetailsModel GetMovieDetails(int movieId)
        {
            if (movieId <= 0)
            {
                return null;
            }

            var result = HttpRequester.Get<MovieDetailsModel>(BaseServicesUrl + "movies/" + movieId);
            return result;
        }

        public static IEnumerable<MovieModel> SearchMoviesByTitleOrDescription(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            var escapedQuery = HttpUtility.UrlEncode(query);
            var result = HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "movies/?keyword=" + escapedQuery);
            return result;
        }

        #endregion

        #region Projections

        public static ProjectionDetailsModel GetProjectionDetails(int projectionId)
        {
            if (projectionId <= 0)
            {
                return null;
            }

            var result = HttpRequester.Get<ProjectionDetailsModel>(BaseServicesUrl + "projections/" + projectionId);
            return result;
        }

        public static ReservationModel CreateReservationForProjection(int projectionId, CreateReservationModel reservation)
        {
            if (projectionId <= 0)
            {
                return null;
            }

            if (reservation == null ||
                string.IsNullOrEmpty(reservation.Email) ||
                reservation.Seats == null || !reservation.Seats.Any())
            {
                return null;
            }

            try
            {
                var result = HttpRequester.Post<ReservationModel>(BaseServicesUrl + "projections/" + projectionId, reservation);
                return result;
            }
            catch (WebException ex)
            {
                if (ex.Message == "The remote server returned an error: (400) Bad Request.")
                {
                    return null;
                }

                throw;
            }
        }

        public static bool RemoveReservationForProjection(int projectionId, RejectReservationModel reservation)
        {
            if (projectionId <= 0)
            {
                return false;
            }

            if (reservation == null ||
                string.IsNullOrEmpty(reservation.Email) ||
                string.IsNullOrEmpty(reservation.UserCode))
            {
                return false;
            }

            var result = HttpRequester.Put(BaseServicesUrl + "projections/" + projectionId, reservation);
            return result;
        }

        #endregion
    }
}