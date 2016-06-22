using Autofac;
using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.Services;
using PetrolPriceMonitor.ViewModels;
using PetrolPriceMonitor.Views;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Bootstrapping
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // service registration
            builder.RegisterType<ViewFactory>()
                .As<IViewFactory>()
                .SingleInstance();

            builder.RegisterType<Navigator>()
                .As<INavigator>()
                .SingleInstance();

            // navigation registration
            builder.Register(context =>
                Application.Current.MainPage.Navigation
            ).SingleInstance();
            
            RegisterViewModels(builder);
            RegisterViews(builder);
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<LogInViewModel>().SingleInstance();
            builder.RegisterType<SignUpViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<RootViewModel>().SingleInstance();
            builder.RegisterType<SearchViewModel>().SingleInstance();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<LogInView>().SingleInstance();
            builder.RegisterType<SignUpView>().SingleInstance();
            builder.RegisterType<HomeView>().SingleInstance();
            builder.RegisterType<RootView>().SingleInstance();
            builder.RegisterType<SearchView>().SingleInstance();
        }
    }
}
