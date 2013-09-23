using CountriesSystem.ViewModels;
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

namespace Countries_System_Simple
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnPrevCountryClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.GetDataContext();
            dataContext.PreviousCountry();
        }

        private void Button_OnNextCountryClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as CountriesSystemViewModel;
            dataContext.NextCountry();
        }

        private CountriesSystemViewModel GetDataContext()
        {
            var dataContext = this.DataContext;

            return dataContext as CountriesSystemViewModel;
        }

        private void Button_OnPrevTownClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.GetDataContext();
            dataContext.PreviousTown();
        }

        private void Button_OnNextTownClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.GetDataContext();
            dataContext.NextTown();
        }
    }
}
