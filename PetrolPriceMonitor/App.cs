using Autofac;
using PetrolPriceMonitor.Bootstrapping;
using Xamarin.Forms;

namespace PetrolPriceMonitor
{
    public class App : Application
    {
        public static IContainer Container { protected set; get; }

        public App()
        {
            var bootstrapper = new Bootstrapper(this);

            Container = bootstrapper.Run();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
