using PetrolPriceMonitor.Constants;
using PetrolPriceMonitor.Models;
using PetrolPriceMonitor.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class ForgotPasswordViewModel : ViewModelBase
    {
        private INavigate _navigate;
        private IAuthenticate _authenticate;
        private IDisplayProgress _progress;

        private bool _canSend = true;

        public string Email { get; set; }

        public ICommand SendCommand { protected set; get; }

        public ForgotPasswordViewModel(INavigate navigate, IAuthenticate authenticate, IDisplayProgress progress)
        {
            Title = "Password";

            _navigate = navigate;
            _authenticate = authenticate;
            _progress = progress;
            
            SendCommand = new Command(() => Send(), () => _canSend);
        }

        async private void Send()
        {
            CanSend(false);

            if (string.IsNullOrWhiteSpace(Email))
            {
                var message = new DisplayAlertMessage(ErrorMessageTitle.Invalid, "You must enter an email address.");
                
                MessagingCenter.Send(Application.Current, MessageCenterMessage.ShowAlert, message);

                CanSend(true);

                return;
            }

            var response = await _authenticate.ForgotPassword(Email);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {

            }
            else
            {
                var message = new DisplayAlertMessage(ErrorMessageTitle.Error, response.FriendlyErrorMessage);

                MessagingCenter.Send(Application.Current, MessageCenterMessage.ShowAlert, message);
            }

            CanSend(true);
        }
        
        private void CanSend(bool value)
        {
            _canSend = value;

            ((Command)SendCommand).ChangeCanExecute();
        }
    }
}
