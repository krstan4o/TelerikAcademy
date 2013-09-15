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
    public class StoreViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Name 
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }
        
        private ObservableCollection<PhoneViewModel> phonesViewModels;

        private ObservableCollection<OperatingSystemViewModel> operationalSystems;

        private ICommand addNewCommand;
        private ICommand editCommand;
        private ICommand removeCommand;

        private string successMessage;

        private string errorMessage;
        private PhoneViewModel newPhoneViewModel;

        public string PhonesStoreDocumentPath { get; set; }

        public StoreViewModel()
        {
            this.PhonesStoreDocumentPath = "..\\..\\phones.xml";
            this.newPhoneViewModel = new PhoneViewModel();
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

        private void HandleRemoveCommand(object obj)
        {
            try
            {
                if (this.selectedStore == null)
                {
                    return;
                }
                DataPersister.RemovePhone(this.PhonesStoreDocumentPath, this.SelectedPhone, this.Name);
                this.Phones = DataPersister.GetPhones(this.PhonesStoreDocumentPath, this.Name);
                this.SetSuccessMessage(string.Format("{0} successfully removed!", this.SelectedPhone.Vendor));
                this.SelectedPhone = null;
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(string.Format("Removing {0} failed with exception {1} ", this.SelectedPhone.Vendor, ex.Message));
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
                DataPersister.SavePhones(this.PhonesStoreDocumentPath, this.Phones, this.Name);
                this.Phones = DataPersister.GetPhones(this.PhonesStoreDocumentPath, this.Name);
                this.SetSuccessMessage(string.Format("Changes successfully saved!"));
                this.SelectedPhone = null;
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(string.Format("Removing {0} failed with exception {1} ", this.SelectedPhone.Vendor, ex.Message));
            }
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

        public PhoneViewModel NewPhone
        {
            get
            {
                return this.newPhoneViewModel;
            }
            set
            {
                this.newPhoneViewModel = value;
                this.OnPropertyChanged("NewPhone");
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

        public void ChangeSelection(object store)
        {
            this.SelectedPhone = store as PhoneViewModel;
        }

        private PhoneViewModel selectedStore;

        public PhoneViewModel SelectedPhone
        {
            get
            {
                return this.selectedStore;
            }
            set
            {
                this.selectedStore = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public IEnumerable<PhoneViewModel> Phones
        {
            get
            {
                return phonesViewModels;
            }
            set
            {
                if (this.phonesViewModels == null)
                {
                    this.phonesViewModels = new ObservableCollection<PhoneViewModel>();
                }
                this.phonesViewModels.Clear();
                foreach (var item in value)
                {
                    this.phonesViewModels.Add(item);
                }
            }
        }

        public IEnumerable<OperatingSystemViewModel> OperationalSystems
        {
            get
            {
                if (this.operationalSystems == null)
                {
                    this.OperationalSystems = DataPersister.GetAllOperationalSystems(this.PhonesStoreDocumentPath);
                }
                return this.operationalSystems;
            }
            set
            {
                if (this.operationalSystems == null)
                {
                    this.operationalSystems = new ObservableCollection<OperatingSystemViewModel>();
                }
                this.operationalSystems.Clear();
                foreach (var item in value)
                {
                    this.operationalSystems.Add(item);
                }
            }
        }

        private void HandleAddNewCommand(object obj)
        {
            try
            {
                DataPersister.AddPhone(this.PhonesStoreDocumentPath, this.NewPhone, this.Name);
                this.Phones = DataPersister.GetPhones(this.PhonesStoreDocumentPath, this.Name);
                this.SetSuccessMessage(string.Format("{0} {1} successfully added!", this.NewPhone.Vendor, this.NewPhone.Model));
                this.NewPhone = new PhoneViewModel();
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(string.Format("Adding {0} {1} failed with exception {2} ", this.NewPhone.Vendor, this.NewPhone.Model, ex.Message));
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
    }
}
