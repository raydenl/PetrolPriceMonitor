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
    public class SearchViewModel : ViewModelBase
    {
        private INavigate _navigator;

        public ICommand SearchCommand { protected set; get; }

        public SearchViewModel(INavigate navigator)
        {
            Title = "Search";

            _navigator = navigator;
            
            SearchCommand = new Command(Search, CanSearch);
        }
        
        private void Search()
        {
            _navigator.PushAsync<SearchResultsViewModel>(tabbedPage: true);
        }

        private bool CanSearch() => true;
    }
}
