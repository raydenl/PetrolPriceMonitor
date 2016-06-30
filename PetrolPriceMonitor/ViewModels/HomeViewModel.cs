using PetrolPriceMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private INavigate _navigate;
        private IAuthenticate _authenticate;

        public ICommand LogOutCommand { protected set; get; }

        public HomeViewModel(INavigate navigate, IAuthenticate authenticate)
        {
            Title = "Home";

            _navigate = navigate;
            _authenticate = authenticate;

            LogOutCommand = new Command(LogOut, CanLogOut);
        }

        private void LogOut()
        {
            _authenticate.LogOut();

            _navigate.SetNavigationRoot<LogInViewModel>();
        }

        private bool CanLogOut() => true;
    }
}
