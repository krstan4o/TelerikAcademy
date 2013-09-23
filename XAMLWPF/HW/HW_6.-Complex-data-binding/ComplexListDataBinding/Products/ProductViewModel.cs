using System.Windows.Input;

namespace Products
{
    public class ProductViewModel : ViewModelBase
    {
        private int id;
        private string number;
        private decimal cost;
        private string name;
        private string description;

        public ProductViewModel()
        {
            this.Id = 1;
            this.GetProduct = new RelayCommand(this.GetProductHandler);
            this.GetProductHandler();
        }

        public ICommand GetProduct { get; set; }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.RaisePropertyChanged();
            }
        }

        public string Number
        {
            get
            {
                return this.number;
            }

            set
            {
                this.number = value;
                this.RaisePropertyChanged();
            }
        }

        public decimal Cost
        {
            get
            {
                return this.cost;
            }

            set
            {
                this.cost = value;
                this.RaisePropertyChanged();
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
                this.RaisePropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.RaisePropertyChanged();
            }
        }

        private void GetProductHandler()
        {
            var product = DataPersister.GetProductById(this.Id);
            if (product != null)
            {
                this.Id = product.Id;
                this.Name = product.ModelName;
                this.Number = product.ModelNumber;
                this.Cost = product.UnitCost;
                this.Description = product.Description;
            }
            else
            {
                this.Name = null;
                this.Number = null;
                this.Cost = 0;
                this.Description = null;
            }
        }
    }
}