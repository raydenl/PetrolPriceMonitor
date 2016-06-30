using PetrolPriceMonitor.Services;
using System;
using System.ComponentModel;

namespace PetrolPriceMonitor.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private INavigate _navigator;

        public SignUpViewModel(INavigate navigator)
        {
            Title = "Sign Up";

            _navigator = navigator;
        }
    }
}
