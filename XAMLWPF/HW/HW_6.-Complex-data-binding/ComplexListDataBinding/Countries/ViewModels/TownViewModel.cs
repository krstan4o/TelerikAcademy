namespace Countries.ViewModels
{
    public class TownViewModel : ViewModelBase
    {
        public TownViewModel() { }

        public TownViewModel(string name, int population)
        {
            this.Name = name;
            this.Population = population;
        }

        public string Name { get; set; }

        public int Population { get; set; }

        public CountryViewModel Country { get; set; }
    }
}
