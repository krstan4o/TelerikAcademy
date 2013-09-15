using System;
using System.Linq;
using System.Windows;
using CountryTownSystem;

namespace _01CoutryManagementSystem
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

        private void OnPreviousCountryClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as ContinentViewModel;
            dataContext.PrevCountry();
            
        }

        private void OnNextCountryClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as ContinentViewModel;
            dataContext.NextCountry();
        }

        private void OnPreviousTownClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as ContinentViewModel;
            dataContext.PrevTown();
        }

        private void OnNextTownClick(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext as ContinentViewModel;
            dataContext.NextTown();
        }
    }
}
