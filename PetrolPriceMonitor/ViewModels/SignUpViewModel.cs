using PetrolPriceMonitor.Services;
using System;
using System.ComponentModel;

namespace PetrolPriceMonitor.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private INavigator _navigator;

        public SignUpViewModel(INavigator navigator)
        {
            Title = "Sign Up";

            _navigator = navigator;
        }
    }
}
