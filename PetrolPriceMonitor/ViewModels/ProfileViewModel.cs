using PetrolPriceMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private INavigate _navigate;

        public ProfileViewModel(INavigate navigate)
        {
            Title = "Profile";

            _navigate = navigate;
        }
    }
}
