using PetrolPriceMonitor.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        private INavigate _navigate;
        private IAuthenticate _authenticate;

        public ICommand SignUpCommand { protected set; get; }
        public ICommand LogInCommand { protected set; get; }
        public ICommand FacebookLogInCommand { protected set; get; }
        public ICommand GuestLogInCommand { protected set; get; }
        
        public LogInViewModel(INavigate navigate, IAuthenticate authenticate)
        {
            Title = "Log In";

            _navigate = navigate;
            _authenticate = authenticate;

            SignUpCommand = new Command(SignUp, CanSignUp);
            LogInCommand = new Command(LogIn, CanLogIn);
            FacebookLogInCommand = new Command(FacebookLogIn, CanFacebookLogIn);
            GuestLogInCommand = new Command(GuestLogIn, CanGuestLogIn);
        }
        
        private void SignUp()
        {
            _navigate.PushAsync<SignUpViewModel>();
        }

        private void LogIn()
        {
            _authenticate.LogIn("", "");
        }

        private void FacebookLogIn()
        {
            _authenticate.LogInUsingFacebook();
        }

        private void GuestLogIn()
        {
            _navigate.SetRoot<RootViewModel>();
        }

        private bool CanSignUp() => true;
        private bool CanLogIn() => true;
        private bool CanFacebookLogIn() => true;
        private bool CanGuestLogIn() => true;
    }
}
