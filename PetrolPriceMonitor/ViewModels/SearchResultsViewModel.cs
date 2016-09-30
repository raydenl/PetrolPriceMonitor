using PetrolPriceMonitor.Enums;
using PetrolPriceMonitor.Repositories;
using PetrolPriceMonitor.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public class SearchResultsViewModel : ViewModelBase
    {
        private INavigate _navigate;
        private IAuthenticate _authenticate;
        private IStationRepository _stationRepository;

        public string Heading { protected set; get; }

        public Location SelectedLocation { set; get; }
        public Company SelectedCompany { set; get; }
        public FuelOption SelectedFuelOption { set; get; }
        public string SelectedPlaceId { get; set; }

        private ObservableCollection<Station> _results;
        public ObservableCollection<Station> Results
        {
            set
            {
                _results = value;
            }
            get
            {
                return _results;
            }
        }

        public SearchResultsViewModel(INavigate navigate, IAuthenticate authenticate, IStationRepository stationRepository)
        {
            Title = "Search";
            Heading = "Results";

            _navigate = navigate;
            _authenticate = authenticate;
            _stationRepository = stationRepository;
        }

        async public override Task ViewAppearing()
        {
            await Initialise();
        }

        async private Task Initialise()
        {
            var favouriteStationIds = new List<string>
            {
                "BB873D34-94A4-48E2-94FF-7713C2050339"
            };

            var favourites = _stationRepository.GetStationsByFuelType(
                FuelType.Octane91,
                favouriteStationIds.ToArray());

            var favouritesVMs = (await favourites).Select(f =>
            {
                var fuelOption = f.FuelOptions.FirstOrDefault();

                return new Station
                {
                    StationName = f.Name,
                    LogoFilename = string.Format("{0}.png", f.Type.ToString()),
                    Price = fuelOption != null ? fuelOption.Price : (decimal?)null
                };
            });

            SetProperty(ref _results, new ObservableCollection<Station>(favouritesVMs), () => Results);
        }
    }
}
