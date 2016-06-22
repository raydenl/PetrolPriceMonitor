using PetrolPriceMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private INavigator _navigator;
        
        public SearchViewModel(INavigator navigator)
        {
            Title = "Search";

            _navigator = navigator;
        }
    }
}
