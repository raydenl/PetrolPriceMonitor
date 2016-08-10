using PetrolPriceMonitor.Constants;
using PetrolPriceMonitor.Models;
using PetrolPriceMonitor.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private INavigate _navigate;
        private IAuthenticate _authenticate;

        public ICommand SignUpCommand { protected set; get; }

        private bool _canSignUp = true;

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public SignUpViewModel(INavigate navigate, IAuthenticate authenticate)
        {
            Title = "Sign Up";

            _navigate = navigate;
            _authenticate = authenticate;

            SignUpCommand = new Command(() => SignUp(), () => _canSignUp);
        }

        async private void SignUp()
        {
            CanSignUp(false);

            if (string.IsNullOrWhiteSpace(Email))
            {
                var message = new DisplayAlertMessage(ErrorMessageTitle.Invalid, "You must enter an email address.");

                MessagingCenter.Send(Application.Current, MessageCenterMessage.ShowAlert, message);

                CanSignUp(true);

                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                var message = new DisplayAlertMessage(ErrorMessageTitle.Invalid, "You must enter a password.");

                MessagingCenter.Send(Application.Current, MessageCenterMessage.ShowAlert, message);

                CanSignUp(true);

                return;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                var message = new DisplayAlertMessage(ErrorMessageTitle.Invalid, "Passwords do not match.");

                MessagingCenter.Send(Application.Current, MessageCenterMessage.ShowAlert, message);

                CanSignUp(true);

                return;
            }

            var response = await _authenticate.SignUp(Email, Password);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {

            }
            else
            {
                var message = new DisplayAlertMessage(ErrorMessageTitle.Error, response.FriendlyErrorMessage);

                MessagingCenter.Send(Application.Current, MessageCenterMessage.ShowAlert, message);
            }

            CanSignUp(true);
        }

        private void CanSignUp(bool value)
        {
            _canSignUp = value;

            ((Command)SignUpCommand).ChangeCanExecute();
        }
    }
}
