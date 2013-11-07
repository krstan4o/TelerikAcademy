using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PrimesApp.Helpers;
using Windows.Networking.BackgroundTransfer;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PrimesApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async void CalculatePrimesClick_1(object sender, RoutedEventArgs e)
        {
            int startNumber = int.Parse(StartNumberBox.Text);
            int endNumber = int.Parse(EndNumberBox.Text);

            var primes = await PrimesFunctions.CalculatePrimesRangeAsync(startNumber, endNumber);
            var primesWithPartners = await PrimesFunctions.GetPrimesBeginsWithDiggitAsync(primes);

            var result = new List<string>();

            if (listPrimes.IsOn)
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, true);
            }
            else
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, false);
            }

            string concat = await result.JoinAsStringAsync(", ");

            Primes.Text = concat;
        }
        private async void CalculatePrimesClick_2(object sender, RoutedEventArgs e)
        {
            int startNumber = int.Parse(StartNumberBox2.Text);
            int endNumber = int.Parse(EndNumberBox2.Text);

            var primes = await PrimesFunctions.CalculatePrimesRangeAsync(startNumber, endNumber);
            var primesWithPartners = await PrimesFunctions.GetPrimesBeginsWithDiggitAsync(primes);


            var result = new List<string>();

            if (listPrimes2.IsOn)
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, true);
            }
            else
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, false);
            }

            string concat = await result.JoinAsStringAsync(", ");

            Primes2.Text = concat;


        }
        private async void CalculatePrimesClick_3(object sender, RoutedEventArgs e) 
        {

            int startNumber = int.Parse(StartNumberBox3.Text);
            int endNumber = int.Parse(EndNumberBox3.Text);

            var primes = await PrimesFunctions.CalculatePrimesRangeAsync(startNumber, endNumber);
            var primesWithPartners = await PrimesFunctions.GetPrimesBeginsWithDiggitAsync(primes);


            var result = new List<string>();

            if (listPrimes3.IsOn)
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, true);
            }
            else
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, false);
            }

            string concat = await result.JoinAsStringAsync(", ");

            Primes3.Text = concat;
        }
        private async void CalculatePrimesClick_4(object sender, RoutedEventArgs e)
        {
            int startNumber = int.Parse(StartNumberBox4.Text);
            int endNumber = int.Parse(EndNumberBox4.Text);

            var primes = await PrimesFunctions.CalculatePrimesRangeAsync(startNumber, endNumber);
            var primesWithPartners = await PrimesFunctions.GetPrimesBeginsWithDiggitAsync(primes);


            var result = new List<string>();

            if (listPrimes4.IsOn)
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, true);
            }
            else
            {
                result = await PrimesFunctions.GetPrimesOrNoPrimesResult(primesWithPartners, false);
            }

            string concat = await result.JoinAsStringAsync(", ");

            Primes4.Text = concat;


        }
    }
}
