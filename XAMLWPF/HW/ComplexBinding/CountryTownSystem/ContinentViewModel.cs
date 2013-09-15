using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;

namespace CountryTownSystem
{
    public class ContinentViewModel
    {
        private string europeDocumentPath;

        private IEnumerable<TownViewModel> currentTowns;
        public string Name { get; set; }

        private IEnumerable<CountryViewModel> countries { get; set; }

        public IEnumerable<CountryViewModel> Countries
        {
            get
            {
                if (this.countries == null)
                {
                    this.countries = DataPersister.GetAll(europeDocumentPath);
                }
                return countries;
            }
        }

        public ContinentViewModel()
        {
            this.europeDocumentPath = "..\\..\\..\\CountryTownSystem\\europe.xml";
        }

        public void NextCountry()
        {
            var countriesCollectionView = this.GetDefaultView(this.countries);
            var currentCountry = countriesCollectionView.CurrentItem as CountryViewModel;
            var towns = currentCountry.Towns;
            countriesCollectionView.MoveCurrentToNext();
            if (countriesCollectionView.IsCurrentAfterLast)
            {
                countriesCollectionView.MoveCurrentToFirst();
            }
        }

        public void PrevCountry()
        {
            var countriesCollectionView = this.GetDefaultView(this.countries);
            countriesCollectionView.MoveCurrentToPrevious();
            if (countriesCollectionView.IsCurrentBeforeFirst)
            {
                countriesCollectionView.MoveCurrentToLast();
            }
        }

        public void NextTown()
        {
            var countriesCollectionView = this.GetDefaultView(this.countries);
            var currentCountry = countriesCollectionView.CurrentItem as CountryViewModel;
            this.currentTowns = currentCountry.Towns;
            var townsCollectionView = this.GetDefaultView(this.currentTowns);
            townsCollectionView.MoveCurrentToNext();
            if (townsCollectionView.IsCurrentAfterLast)
            {
                townsCollectionView.MoveCurrentToFirst();
            }
        }

        public void PrevTown()
        {
            var countriesCollectionView = this.GetDefaultView(this.countries);
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
