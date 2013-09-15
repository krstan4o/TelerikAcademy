using PhonesStore.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace PhonesStore.ViewModels
{
    public class StoresViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<StoreViewModel> storesViewModels;

        public string StoresDocumentPath { get; set; }

        private ICommand addNewCommand;
        private ICommand removeCommand;
        private ICommand editCommand;

        private string successMessage;

        private string errorMessage;
        private StoreViewModel newStoreViewModel;

        public StoresViewModel()
        {
            this.StoresDocumentPath = "..\\..\\phones.xml";
            this.newStoreViewModel = new StoreViewModel();
        }

        public ICommand AddNew
        {
            get
            {
                if (this.addNewCommand == null)
                {
                    this.addNewCommand = new RelayCommand(this.HandleAddNewCommand);
                }
                return this.addNewCommand;
            }
        }

        public void ChangeSelection(object store)
        {
            this.SelectedStore = store as StoreViewModel;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private StoreViewModel selectedStore;

        public StoreViewModel SelectedStore
        {
            get
            {
                return this.selectedStore;
            }
            set
            {
                this.selectedStore = value;
                OnPropertyChanged("SelectedStore");
            }
        }

        public ICommand Remove
        {
            get
            {
                if (this.removeCommand == null)
                {
                    this.removeCommand = new RelayCommand(this.HandleRemoveCommand);
                }
                return this.removeCommand;
            }
        }

        public ICommand Edit
        {
            get
            {
                if (this.editCommand == null)
                {
                    this.editCommand = new RelayCommand(this.HandleEditCommand);
                }
                return this.editCommand;
            }
        }

        public StoreViewModel NewStore
        {
            get
            {
                return this.newStoreViewModel;
            }
            set
            {
                this.newStoreViewModel = value;
                this.OnPropertyChanged("NewStore");
            }
        }

        public string SuccessMessage
        {
            get
            {
                return this.successMessage;
            }
            set
            {
                this.successMessage = value;
                this.OnPropertyChanged("SuccessMessage");
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
                this.OnPropertyChanged("ErrorMessage");
            }
        }

        private void HandleAddNewCommand(object obj)
        {
            try
            {
                DataPersister.AddStore(this.StoresDocumentPath, this.NewStore);
                this.Stores = DataPersister.GetStores(this.StoresDocumentPath);
                this.SetSuccessMessage(string.Format("{0} successfully added!", this.NewStore.Name));
                this.NewStore = new StoreViewModel();
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(string.Format("Adding {0} failed with exception {1} ", this.NewStore.Name,  ex.Message));
            }
        }


        private void HandleRemoveCommand(object obj)
        {
            try
            {
                if (this.selectedStore==null)
                {
                    return;
                }
                DataPersister.RemoveStore(this.StoresDocumentPath, this.SelectedStore);
                this.Stores = DataPersister.GetStores(this.StoresDocumentPath);
                this.SetSuccessMessage(string.Format("{0} successfully removed!", this.NewStore.Name));
                this.SelectedStore = null;
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(string.Format("Removing {0} failed with exception {1} ", this.NewStore.Name, ex.Message));
            }
        }


        private void HandleEditCommand(object obj)
        {
            try
            {
                if (this.selectedStore == null)
                {
                    return;
                }
                DataPersister.SaveStores(this.StoresDocumentPath, this.Stores);
                this.Stores = DataPersister.GetStores(this.StoresDocumentPath);
                this.SetSuccessMessage(string.Format("Changes successfully saved!"));
                this.SelectedStore = null;
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(string.Format("Removing {0} failed with exception {1} ", this.NewStore.Name, ex.Message));
            }
        }

        private void SetSuccessMessage(string text)
        {
            this.SuccessMessage = text;
            var timer = new DispatcherTimer();
            timer.Tick += (snd, args) =>
            {
                this.SuccessMessage = "";
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
        }

        private void SetErrorMessage(string text)
        {
            this.SuccessMessage = text;
            var timer = new DispatcherTimer();
            timer.Tick += (snd, args) =>
            {
                this.SuccessMessage = "";
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
        }


        public IEnumerable<StoreViewModel> Stores
        {
            get
            {
                if (this.storesViewModels == null)
                {
                    this.Stores = DataPersister.GetStores(StoresDocumentPath);
                }
                return storesViewModels;
            }
            set
            {
                if (this.storesViewModels == null)
                {
                    this.storesViewModels = new ObservableCollection<StoreViewModel>();
                }
                this.storesViewModels.Clear();
                foreach (var item in value)
                {
                    this.storesViewModels.Add(item);
                }
            }
        }
    }
}
