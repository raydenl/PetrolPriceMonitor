using Autofac;
using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.Repositories;
using PetrolPriceMonitor.Services;
using PetrolPriceMonitor.ViewModels;
using PetrolPriceMonitor.Views;
using System;
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

            builder.RegisterType<NavigatorService>()
                .As<INavigate>()
                .SingleInstance();

            builder.RegisterType<LocationService>()
                .As<ILocate>()
                .SingleInstance();

            builder.RegisterType<RestService>()
                .As<IConsume>()
                .SingleInstance();

            // navigation registration
            builder.Register(context =>
                Application.Current.MainPage.Navigation
            ).SingleInstance();

            RegisterPlatformSpecificObjects(builder);
            RegisterRepositories(builder);
            RegisterViewModels(builder);
            RegisterViews(builder);
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<StationRepository>()
                .As<IStationRepository>();
        }

        private void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<LogInViewModel>().SingleInstance();
            builder.RegisterType<SignUpViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<TabViewModel>().SingleInstance();
            builder.RegisterType<SearchViewModel>().SingleInstance();
            builder.RegisterType<SearchResultsViewModel>().SingleInstance();
            builder.RegisterType<ProfileViewModel>().SingleInstance();
            builder.RegisterType<ForgotPasswordViewModel>().SingleInstance();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<LogInView>().SingleInstance();
            builder.RegisterType<SignUpView>().SingleInstance();
            builder.RegisterType<HomeView>().SingleInstance();
            builder.RegisterType<TabView>().SingleInstance();
            builder.RegisterType<SearchView>().SingleInstance();
            builder.RegisterType<SearchResultsView>().SingleInstance();
            builder.RegisterType<ProfileView>().SingleInstance();
            builder.RegisterType<ForgotPasswordView>().SingleInstance();
        }

        private void RegisterPlatformSpecificObjects(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetImplementation<IAuthenticate>()).AsImplementedInterfaces();
            builder.RegisterInstance(GetImplementation<IDisplayProgress>()).AsImplementedInterfaces();
        }
        
        private static T GetImplementation<T>() where T: class
        {
            var implementation = DependencyService.Get<T>();

            if (implementation == null)
            {
                throw new InvalidOperationException($"Missing '{typeof(T).FullName}' implementation. Implementation is required.");
            }

            return implementation;
        }
    }
}
