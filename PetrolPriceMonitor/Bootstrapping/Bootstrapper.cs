using Autofac;
using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.Services;
using PetrolPriceMonitor.ViewModels;
using PetrolPriceMonitor.Views;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Bootstrapping
{
    public class Bootstrapper : AutofacBootstrapper
    {
        private readonly App _application;

        public Bootstrapper(App application)
        {
            _application = application;
        }
        
        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<LogInViewModel, LogInView>();
            viewFactory.Register<SignUpViewModel, SignUpView>();
            viewFactory.Register<HomeViewModel, HomeView>();
            viewFactory.Register<RootViewModel, RootView>();
            viewFactory.Register<SearchViewModel, SearchView>();
            viewFactory.Register<ProfileViewModel, ProfileView>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            var navigate = container.Resolve<INavigate>();

            navigate.SetNavigationRoot<LogInViewModel>();
        }
    }

}
