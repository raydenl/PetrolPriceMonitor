using BigTed;
using System;

namespace PetrolPriceMonitor.iOS
{
    public class ProgressIndicator : IDisposable
    {
        public ProgressIndicator(string message)
        {
            BTProgressHUD.Show(status: message, maskType: ProgressHUD.MaskType.Black);
        }

        public void Dispose()
        {
            BTProgressHUD.Dismiss();
        }
        
        public static void Show(string message)
        {
            BTProgressHUD.Show(status: message, maskType: ProgressHUD.MaskType.Black);
        }

        public static void Hide()
        {
            BTProgressHUD.Dismiss();
        }
    }
}
