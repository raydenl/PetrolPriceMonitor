using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PetrolPriceMonitor.Constants;
using PetrolPriceMonitor.Models;
using PetrolPriceMonitor.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
        private IConsume _consumer;

        private Location _location;
        private string _placeId;

        public ICommand SearchCommand { protected set; get; }
        public ICommand AddressSearchCommand { protected set; get; }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                if (!Equals(_address, value))
                {
                    _address = value;

                    OnPropertyChanged(() => Address);

                    Togglable(() => PopulateAddresses());
                }
            }
        }
        private ObservableCollection<Address> _addresses;
        public ObservableCollection<Address> Addresses
        {
            get { return _addresses; }
            set
            {
                if (!Equals(_addresses, value))
                {
                    _addresses = value;

                    OnPropertyChanged(() => Addresses);
                }
            }
        }
        private string _addressPlaceholder;
        public string AddressPlaceholder
        {
            get { return _addressPlaceholder; }
            set
            {
                if (!Equals(_addressPlaceholder, value))
                {
                    _addressPlaceholder = value;

                    OnPropertyChanged(() => AddressPlaceholder);
                }
            }
        }
        private bool _useCurrentLocation;
        public bool UseCurrentLocation
        {
            get { return _useCurrentLocation; }
            set
            {
                if (!Equals(_useCurrentLocation, value))
                {
                    _useCurrentLocation = value;

                    OnPropertyChanged(() => UseCurrentLocation);

                    if (value)
                        SetCurrentLocation();
                    else
                        ClearCurrentLocation();
                }
            }
        }

        private bool _isSearchBarFocused;
        public bool IsSearchBarFocused
        {
            get { return _isSearchBarFocused; }
            set
            {
                if (!Equals(_isSearchBarFocused, value))
                {
                    _isSearchBarFocused = value;

                    OnPropertyChanged(() => IsSearchBarFocused);
                }
            }
        }

        public Company SelectedCompany { get; set; }
        public FuelOption SelectedFuelOption { get; set; }

        private Address _selectedAddress;
        public Address SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                if (!Equals(_selectedAddress, value))
                {
                    _selectedAddress = value;

                    OnPropertyChanged(() => SelectedAddress);

                    SetAddress();
                }
            }
        }

        public ObservableCollection<Company> Companies { protected set; get; }
        public ObservableCollection<FuelOption> FuelOptions { protected set; get; }

        public SearchViewModel(INavigate navigator, ILocate locator, IDisplayProgress progress, IConsume consumer)
        {
            Title = "Search";

            _navigator = navigator;
            _locator = locator;
            _progress = progress;
            _consumer = consumer;
            
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

            Addresses = new ObservableCollection<Address>(new List<Address>());

            IsSearchBarFocused = true;
        }
        
        private void Search()
        {
            _navigator.PushAsync<SearchResultsViewModel>(tabbedPage: true, setStateAction: vm => {
                vm.SelectedLocation = _location;
                vm.SelectedCompany = SelectedCompany;
                vm.SelectedFuelOption = SelectedFuelOption;
                vm.SelectedPlaceId = _placeId;
            });
        }

        private void AddressSearch()
        {
            PopulateAddresses();
        }

        private void SetAddress()
        {
            _placeId = SelectedAddress.PlaceId;
            
            Addresses = new ObservableCollection<Address>(new List<Address>());

            ToggleOff(() => Address = SelectedAddress.DisplayName);

            IsSearchBarFocused = false;
        }
        
        async private void SetCurrentLocation()
        {
            _progress.Show(ProgressMessage.Working);

            Address = null;
            AddressPlaceholder = "Locating...";

            var location = await _locator.GetCurrentLocation();
            
            AddressPlaceholder = "Current Location";
            
            _progress.Hide();

            _location = new Location
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }

        async private void PopulateAddresses()
        {
            var settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;

            var results = await _consumer.GetAsync<AddressPrediction>(RestUrl.GooglePlaceAutocomplete(Address, "address", "en-AU", "country:nz", Authentication.GoogleApiKey), settings);

            var predictions = results.Predictions;

            var addresses = new List<Address>();

            foreach (var prediction in predictions)
            {
                addresses.Add(new Address
                {
                    DisplayName = prediction.Description,
                    PlaceId = prediction.PlaceId
                });
            }

            Addresses = new ObservableCollection<Address>(addresses);
        }

        private void ClearCurrentLocation()
        {
            AddressPlaceholder = "Enter an address";
        }
        
        private bool CanSearch() => true;
        private bool CanAddressSearch() => true;
    }
}
