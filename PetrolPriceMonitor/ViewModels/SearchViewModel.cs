using PetrolPriceMonitor.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private INavigate _navigator;

        public ICommand SearchCommand { protected set; get; }

        public string Location { get; set; }
        public bool UseCurrentLocation { get; set; }
        public Company SelectedCompany { get; set; }
        public FuelOption SelectedFuelOption { get; set; }

        public ObservableCollection<Company> Companies { protected set; get; }
        public ObservableCollection<FuelOption> FuelOptions { protected set; get; }

        public SearchViewModel(INavigate navigator)
        {
            Title = "Search";

            _navigator = navigator;
            
            SearchCommand = new Command(Search, CanSearch);

            Initialise();
        }

        private void Initialise()
        {
            var companies = new List<Company>
            {
                new Company { CompanyName = "Challenge", OrderIndex = 2 },
                new Company { CompanyName = "BP", OrderIndex = 1 }
            }.OrderBy(c => c.OrderIndex);
            
            Companies = new ObservableCollection<Company>(companies);

            var fuelOptions = new List<FuelOption>
            {
                new FuelOption { FuelOptionName = "91", OrderIndex = 1 },
                new FuelOption { FuelOptionName = "95", OrderIndex = 2 },
                new FuelOption { FuelOptionName = "98", OrderIndex = 3 },
                new FuelOption { FuelOptionName = "Diesel", OrderIndex = 4 }
            }.OrderBy(f => f.OrderIndex);
            
            FuelOptions = new ObservableCollection<FuelOption>(fuelOptions);
        }
        
        private void Search()
        {
            _navigator.PushAsync<SearchResultsViewModel>(tabbedPage: true);
        }

        private bool CanSearch() => true;
    }
}
