using UIKit;
using PetrolPriceMonitor.iOS.Bootstrapping;

namespace PetrolPriceMonitor.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();

            bootstrapper.Run();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
