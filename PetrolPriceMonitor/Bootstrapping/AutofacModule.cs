using Autofac;
using PetrolPriceMonitor.Factories;
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

            // navigation registration
            builder.Register(context =>
                Application.Current.MainPage.Navigation
            ).SingleInstance();

            RegisterPlatformSpecificObjects(builder);
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
            builder.RegisterType<ProfileViewModel>().SingleInstance();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<LogInView>().SingleInstance();
            builder.RegisterType<SignUpView>().SingleInstance();
            builder.RegisterType<HomeView>().SingleInstance();
            builder.RegisterType<RootView>().SingleInstance();
            builder.RegisterType<SearchView>().SingleInstance();
            builder.RegisterType<ProfileView>().SingleInstance();
        }

        private void RegisterPlatformSpecificObjects(ContainerBuilder builder)
        {
            builder.RegisterInstance(GetAuthenticateImplementation()).AsImplementedInterfaces();
        }

        private static IAuthenticate GetAuthenticateImplementation()
        {
            var implementation = DependencyService.Get<IAuthenticate>();

            if (implementation == null)
            {
                throw new InvalidOperationException($"Missing '{typeof(IAuthenticate).FullName}' implementation. Implementation is required.");
            }

            return implementation;
        }
    }
}
