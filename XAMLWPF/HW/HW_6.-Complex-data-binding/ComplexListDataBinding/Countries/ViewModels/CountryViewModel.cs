using Countries.Commands;
using Countries.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Countries.ViewModels
{
    public class CountryViewModel : ViewModelBase, INotifySelectedCountry
    {
        private TownViewModel currentTown;

        public CountryViewModel()
        {
            this.Towns = new List<TownViewModel>();
            this.SelectCountry = new RelayCommand<string>(this.NotifyCountrySelected);
            this.Previous = new RelayCommand(this.NextTown);
            this.Next = new RelayCommand(this.NextTown);
        }

        public CountryViewModel(string name, string language, string flagUrl, IEnumerable<TownViewModel> towns) : this()
        {
            this.Name = name;
            this.Language = language;
            this.FlagUrl = flagUrl;
            this.Towns = towns;
            this.CurrentTown = towns.FirstOrDefault();
        }

        public TownViewModel CurrentTown
        {
            get
            {
                return this.currentTown;
            }

            set
            {
                this.currentTown = value;
                this.RaisePropertyChanged();
            }
        }


        public ICommand SelectCountry { get; set; }

        public ICommand Next { get; set; }

        public ICommand Previous { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public string FlagUrl { get; set; }

        public IEnumerable<TownViewModel> Towns { get; set; }

        public event EventHandler<string> SelectedCountry;

        private void NotifyCountrySelected(string country)
        {
            var handler = this.SelectedCountry;
            if (handler != null)
            {
                handler(this, country);
            }
        }

        public void NextTown()
        {
            var collection = this.Towns.GetDefaultView();
            collection.MoveCurrentToNext();
            if (collection.IsCurrentAfterLast)
            {
                collection.MoveCurrentToFirst();
            }

            var next = collection.CurrentItem as TownViewModel;
            this.CurrentTown = next;
        }

        public void PreviousTown()
        {
            var collection = this.Towns.GetDefaultView();
            collection.MoveCurrentToPrevious();
            if (collection.IsCurrentBeforeFirst)
            {
                collection.MoveCurrentToLast();
            }

            var previous = collection.CurrentItem as TownViewModel;
            this.CurrentTown = previous;
        }
    }
}