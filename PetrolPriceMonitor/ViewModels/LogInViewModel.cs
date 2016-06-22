using PetrolPriceMonitor.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        private INavigator _navigator;

        public ICommand SignUpCommand { protected set; get; }
        public ICommand LogInCommand { protected set; get; }
        public ICommand FacebookLogInCommand { protected set; get; }
        public ICommand GuestLogInCommand { protected set; get; }
        
        public LogInViewModel(INavigator navigator)
        {
            Title = "Log In";

            _navigator = navigator;

            SignUpCommand = new Command(SignUp, CanSignUp);
            LogInCommand = new Command(LogIn, CanLogIn);
            FacebookLogInCommand = new Command(FacebookLogIn, CanFacebookLogIn);
            GuestLogInCommand = new Command(GuestLogIn, CanGuestLogIn);
        }
        
        private void SignUp()
        {
            _navigator.PushAsync<SignUpViewModel>();
        }

        private void LogIn()
        {

        }

        private void FacebookLogIn()
        {

        }

        private void GuestLogIn()
        {
            _navigator.SetRoot<RootViewModel>();
        }

        private bool CanSignUp() => true;
        private bool CanLogIn() => true;
        private bool CanFacebookLogIn() => true;
        private bool CanGuestLogIn() => true;
    }
}
