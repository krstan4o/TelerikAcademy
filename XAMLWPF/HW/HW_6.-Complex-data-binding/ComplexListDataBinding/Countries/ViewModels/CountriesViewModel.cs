using Countries.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Countries.Extensions;
using System.Windows.Input;

namespace Countries.ViewModels
{
    public class CountriesViewModel : ViewModelBase
    {
        private CountryViewModel currentCountry;

        public CountriesViewModel()
        {
            var usaTowns = new List<TownViewModel>();
            usaTowns.Add(new TownViewModel("NYC", 17000000));
            usaTowns.Add(new TownViewModel("Chicago Town", 19000000));
            usaTowns.Add(new TownViewModel("LA", 14000000));

            var australiaTowns = new List<TownViewModel>();
            australiaTowns.Add(new TownViewModel("Sydney", 12500000));
            australiaTowns.Add(new TownViewModel("Brisbane", 780000));

            var countries = new List<CountryViewModel>();
            countries.Add(new CountryViewModel("USA", "English", "/url", usaTowns));
            countries.Add(new CountryViewModel("Australia", "English", "/url", australiaTowns));

            foreach (var country in countries)
            {
                country.SelectedCountry += this.SetCurrentCountry;
            }

            this.Countries = countries;
            this.CurrentCountry = countries.First();
            this.Next = new RelayCommand(this.NextCountry);
            this.Previous = new RelayCommand(this.PreviousCountry);
        }

        public ICommand Next { get; set; }

        public ICommand Previous { get; set; }

        public CountryViewModel CurrentCountry
        {
            get
            {
                return this.currentCountry;
            }

            set
            {
                this.currentCountry = value;
                this.RaisePropertyChanged();
            }
        }

        public IEnumerable<CountryViewModel> Countries { get; set; }

        public void SetCurrentCountry(object sender, string countryName)
        {
            var selectedCountry = this.Countries.First(c => c.Name == countryName);
            this.CurrentCountry = selectedCountry;
        }

        public void NextCountry()
        {
            var collection = this.Countries.GetDefaultView();
            collection.MoveCurrentToNext();
            if (collection.IsCurrentAfterLast)
            {
                collection.MoveCurrentToFirst();
            }

            var next = collection.CurrentItem as CountryViewModel;
            this.CurrentCountry = next;
        }

        public void PreviousCountry()
        {
            var collection = this.Countries.GetDefaultView();
            collection.MoveCurrentToPrevious();
            if (collection.IsCurrentBeforeFirst)
            {
                collection.MoveCurrentToLast();
            }

            var previous = collection.CurrentItem as CountryViewModel;
            this.CurrentCountry = previous;
        }
    }
}