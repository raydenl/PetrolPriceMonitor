using BigTed;
using PetrolPriceMonitor.iOS;
using PetrolPriceMonitor.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgressIndicatorService))]
namespace PetrolPriceMonitor.iOS
{
    public class ProgressIndicatorService : IDisplayProgress
    {
        public ProgressIndicatorService() { }
        
        public void Show(string message)
        {
            BTProgressHUD.Show(status: message, maskType: ProgressHUD.MaskType.Black);
        }

        public void Hide()
        {
            BTProgressHUD.Dismiss();
        }
    }
}
