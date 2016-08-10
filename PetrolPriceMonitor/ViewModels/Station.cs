using System.Windows.Input;
using Xamarin.Forms;

namespace PetrolPriceMonitor.ViewModels
{
    public class Station : ViewModelBase
    {
        public ICommand ConfirmCommand { protected set; get; }

        public ICommand RejectCommand { protected set; get; }

        public string CompanyName { set; get; }

        public string StationName { set; get; }
            
        public decimal Price { set; get; }

        public string LogoFilename
        {
            get { return string.Format("{0}.png", CompanyName.Replace(" ", "-")); }
        }

        public Station()
        {
            ConfirmCommand = new Command(Confirm, CanConfirm);
            RejectCommand = new Command(Reject, CanReject);
        }

        private void Confirm()
        {

        }

        private void Reject()
        {

        }

        private bool CanConfirm() => true;
        private bool CanReject() => true;
    }
}
