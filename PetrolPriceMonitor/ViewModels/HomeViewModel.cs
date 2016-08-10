﻿using PetrolPriceMonitor.Repositories;
using PetrolPriceMonitor.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private INavigate _navigate;
        private IAuthenticate _authenticate;
        private IStationRepository _stationRepository;

        public ICommand LogOutCommand { protected set; get; }
        public string Heading { protected set; get; }
        private ObservableCollection<Station> _favourites;
        public ObservableCollection<Station> Favourites
        {
            set
            {
                _favourites = value;
            }
            get
            {
                return _favourites;
            }
        }

        public HomeViewModel(INavigate navigate, IAuthenticate authenticate, IStationRepository stationRepository)
        {
            Title = "Home";
            Heading = "Favourites";

            _navigate = navigate;
            _authenticate = authenticate;
            _stationRepository = stationRepository;

            LogOutCommand = new Command(LogOut, CanLogOut);
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

            SetProperty(ref _favourites, new ObservableCollection<Station>(favouritesVMs), () => Favourites);
        }

        private void LogOut()
        {
            _authenticate.LogOut();

            _navigate.PopModalAsync();
        }

        private bool CanLogOut() => true;
    }
}
