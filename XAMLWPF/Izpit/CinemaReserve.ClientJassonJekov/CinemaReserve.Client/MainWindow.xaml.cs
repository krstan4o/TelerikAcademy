using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaReserve.Client
{
    using CinemaReserve.Client.Data;
    using CinemaReserve.ResponseModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //DataPersister.GetCinemas();
            //DataPersister.GetCinemaMovies(-1);
            //DataPersister.GetCinemaMovieProjections(1, 1);
            //DataPersister.GetMovies();
            //DataPersister.GetMovieDetails(1);
            //DataPersister.SearchMoviesByTitleOrDescription("9 in cinema");
            //DataPersister.GetProjectionDetails(1);
            //DataPersister.CreateReservationForProjection(
            //    1,
            //    new CreateReservationModel()
            //    {
            //        Email = "nakov@nakov.com",
            //        Seats = new[] { new SeatModel() { Column = 1, Row = 1 } }
            //    });
            //DataPersister.RemoveReservationForProjection(
            //    1,
            //    new RejectReservationModel() { Email = "nakov@nakov.com", UserCode = "n8m24" });
        }

        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.C)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
