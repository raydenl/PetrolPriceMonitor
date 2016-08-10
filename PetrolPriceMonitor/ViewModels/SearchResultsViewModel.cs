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
                "F032D049-6893-49C4-8B1E-44EDE85455FA"
            };

            var favourites = _stationRepository.GetFavourites(favouriteStationIds.ToArray());

            var favouritesVMs = (await favourites).Select(f => new Station
            {
                CompanyName = f.CompanyName,
                StationName = f.StationName,
                Price = f.Price
            });

            SetProperty(ref _results, new ObservableCollection<Station>(favouritesVMs), () => Results);
        }
    }
}
