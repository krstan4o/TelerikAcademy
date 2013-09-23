using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CountriesSystem.ViewModels
{
    public class CountriesSystemViewModel
    {
        private IEnumerable<CountryViewModel> countriesViewModels;
        private IEnumerable<TownViewModel> currentTowns;
        public string CountriesSystemDocumentPath { get; set; }

        public CountriesSystemViewModel()
        {
            this.CountriesSystemDocumentPath = "..\\..\\..\\CountriesSystem.ViewModels\\countries.xml";
        }

        public IEnumerable<CountryViewModel> Countries
        {
            get
            {
                if (this.countriesViewModels == null)
	            {
                    this.countriesViewModels = DataPersister.GetAll(CountriesSystemDocumentPath);
	            }

                return countriesViewModels;
            }
        }

        public void NextCountry()
        {
            var countriesCollectionView = this.GetDefaultView(this.countriesViewModels);
            countriesCollectionView.MoveCurrentToNext();

            if (countriesCollectionView.IsCurrentAfterLast)
            {
                countriesCollectionView.MoveCurrentToFirst();
            }
        }

        public void PreviousCountry()
        {
            var countriesCollectionView = this.GetDefaultView(this.countriesViewModels);
            countriesCollectionView.MoveCurrentToPrevious();

            if (countriesCollectionView.IsCurrentBeforeFirst)
            {
                countriesCollectionView.MoveCurrentToLast();
            }
        }

        public void NextTown()
        {
            var countriesCollectionView = this.GetDefaultView(this.countriesViewModels);
            var currentCountry = countriesCollectionView.CurrentItem as CountryViewModel;

            this.currentTowns = currentCountry.Towns;
            var townsCollectionView = this.GetDefaultView(this.currentTowns);
            townsCollectionView.MoveCurrentToNext();

            if (townsCollectionView.IsCurrentAfterLast)
            {
                townsCollectionView.MoveCurrentToFirst();
            }
        }

        public void PreviousTown()
        {
            var countriesCollectionView = this.GetDefaultView(this.countriesViewModels);
            var currentCountry = countriesCollectionView.CurrentItem as CountryViewModel;

            this.currentTowns = currentCountry.Towns;
            var townsCollectionView = this.GetDefaultView(this.currentTowns);
            townsCollectionView.MoveCurrentToPrevious();

            if (townsCollectionView.IsCurrentBeforeFirst)
            {
                townsCollectionView.MoveCurrentToLast();
            }
        }

        private ICollectionView GetDefaultView<T>(IEnumerable<T> collection)
        {
            return CollectionViewSource.GetDefaultView(collection);
        }
    }
}
