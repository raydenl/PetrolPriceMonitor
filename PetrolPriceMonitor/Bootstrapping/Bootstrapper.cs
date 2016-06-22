using Autofac;
using PetrolPriceMonitor.Factories;
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

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterModule<AutofacModule>();
        }

        protected override void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<LogInViewModel, LogInView>();
            viewFactory.Register<SignUpViewModel, SignUpView>();
            viewFactory.Register<HomeViewModel, HomeView>();
            viewFactory.Register<RootViewModel, RootView>();
            viewFactory.Register<SearchViewModel, SearchView>();
        }

        protected override void ConfigureApplication(IContainer container)
        {
            // set main page
            var viewFactory = container.Resolve<IViewFactory>();
            var mainPage = viewFactory.Resolve<LogInViewModel>();
            var navigationPage = new NavigationPage(mainPage);

            _application.MainPage = navigationPage;
        }
    }

}
