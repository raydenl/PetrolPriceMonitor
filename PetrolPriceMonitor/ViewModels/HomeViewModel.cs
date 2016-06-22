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
        private INavigator _navigator;

        public ICommand Blah { protected set; get; }

        public HomeViewModel(INavigator navigator)
        {
            Title = "Home";

            _navigator = navigator;

            Blah = new Command(() =>
            {

            });
        }
    }
}
