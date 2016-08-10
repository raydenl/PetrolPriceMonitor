using Autofac;
using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.Services;
using PetrolPriceMonitor.ViewModels;
using PetrolPriceMonitor.Views;

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
            viewFactory.Register<TabViewModel, TabView>();
            viewFactory.Register<SearchViewModel, SearchView>();
            viewFactory.Register<SearchResultsViewModel, SearchResultsView>();
            viewFactory.Register<ProfileViewModel, ProfileView>();
            viewFactory.Register<ForgotPasswordViewModel, ForgotPasswordView>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            var navigate = container.Resolve<INavigate>();

            navigate.SetNavigationRoot<LogInViewModel>();
        }
    }

}
