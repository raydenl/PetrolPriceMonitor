using PetrolPriceMonitor.Constants;
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
        private ILocate _locator;
        private IDisplayProgress _progress;

        private Location _location;

        public ICommand SearchCommand { protected set; get; }
        public ICommand AddressSearchCommand { protected set; get; }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }
        private string _addressPlaceholder;
        public string AddressPlaceholder
        {
            get { return _addressPlaceholder; }
            set
            {
                _addressPlaceholder = value;
            }
        }
        private bool _useCurrentLocation;
        public bool UseCurrentLocation
        {
            get { return _useCurrentLocation; }
            set
            {
                _useCurrentLocation = value;

                OnPropertyChanged(() => UseCurrentLocation);

                if (value)
                    SetCurrentLocation();
                else
                    ClearCurrentLocation();
            }
        }
        public Company SelectedCompany { get; set; }
        public FuelOption SelectedFuelOption { get; set; }

        public ObservableCollection<Company> Companies { protected set; get; }
        public ObservableCollection<FuelOption> FuelOptions { protected set; get; }

        public SearchViewModel(INavigate navigator, ILocate locator, IDisplayProgress progress)
        {
            Title = "Search";

            _navigator = navigator;
            _locator = locator;
            _progress = progress;
            
            SearchCommand = new Command(Search, CanSearch);
            AddressSearchCommand = new Command(AddressSearch, CanAddressSearch);

            Initialise();
        }

        private void Initialise()
        {
            ClearCurrentLocation();

            var companies = new List<Company>
            {
                new Company { CompanyName = "All", OrderIndex = 0 },
                new Company { CompanyName = "BP", OrderIndex = 1 },
                new Company { CompanyName = "Challenge", OrderIndex = 2 },
                new Company { CompanyName = "Z Energy", OrderIndex = 3 }
            }.OrderBy(c => c.OrderIndex);
            
            Companies = new ObservableCollection<Company>(companies);

            var fuelOptions = new List<FuelOption>
            {
                new FuelOption { FuelOptionName = "All", OrderIndex = 1 },
                new FuelOption { FuelOptionName = "91", OrderIndex = 1 },
                new FuelOption { FuelOptionName = "95", OrderIndex = 2 },
                new FuelOption { FuelOptionName = "98", OrderIndex = 3 },
                new FuelOption { FuelOptionName = "Diesel", OrderIndex = 4 }
            }.OrderBy(f => f.OrderIndex);
            
            FuelOptions = new ObservableCollection<FuelOption>(fuelOptions);

            SelectedCompany = companies.First();
            SelectedFuelOption = fuelOptions.First();
        }
        
        private void Search()
        {
            _navigator.PushAsync<SearchResultsViewModel>(tabbedPage: true, setStateAction: vm => {
                vm.SelectedLocation = _location;
                vm.SelectedCompany = SelectedCompany;
                vm.SelectedFuelOption = SelectedFuelOption;
            });
        }

        private void AddressSearch()
        {
            SetLocation();
        }

        async private void SetLocation()
        {
            _progress.Show(ProgressMessage.Working);

            var location = await _locator.GetCurrentLocation();
            
            _progress.Hide();

            _location = new Location
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
        
        async private void SetCurrentLocation()
        {
            _progress.Show(ProgressMessage.Working);

            SetProperty(ref _address, null, () => Address);
            SetProperty(ref _addressPlaceholder, "Locating...", () => AddressPlaceholder);

            var location = await _locator.GetCurrentLocation();
            
            SetProperty(ref _addressPlaceholder, "Current Location", () => AddressPlaceholder);
            
            _progress.Hide();

            _location = new Location
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }

        private void ClearCurrentLocation()
        {
            SetProperty(ref _addressPlaceholder, "Enter an address", () => AddressPlaceholder);
        }
        
        private bool CanSearch() => true;
        private bool CanAddressSearch() => true;
    }
}
