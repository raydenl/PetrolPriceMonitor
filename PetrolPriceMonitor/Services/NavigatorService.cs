using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Services
{
    public class NavigatorService : INavigate
    {
        private readonly Lazy<INavigation> _navigation;
        private readonly IViewFactory _viewFactory;

        public NavigatorService(Lazy<INavigation> navigation, IViewFactory viewFactory)
        {
            _navigation = navigation;
            _viewFactory = viewFactory;
        }

        private INavigation Navigation
        {
            get { return _navigation.Value; }
        }

        public async Task<IViewModel> PopAsync()
        {
            Page view = await Navigation.PopAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            Page view = await Navigation.PopModalAsync();
            return view.BindingContext as IViewModel;
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction = null, bool tabbedPage = false)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel, setStateAction);

            await PushAsync(view, tabbedPage);
            
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel, bool tabbedPage = false)
            where TViewModel : class, IViewModel
        {
            var view = _viewFactory.Resolve(viewModel);

            await PushAsync(view, tabbedPage);

            return viewModel;
        }

        private async Task PushAsync(Page view, bool tabbedPage)
        {
            if (!tabbedPage)
            {
                await Navigation.PushAsync(view);
            }
            else
            {
                var tab = _viewFactory.Resolve<TabViewModel>() as TabbedPage;

                await ((NavigationPage)tab.CurrentPage).PushAsync(view);
            }
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel, setStateAction);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var view = _viewFactory.Resolve(viewModel);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public TViewModel SetRoot<TViewModel>()
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel);
            Application.Current.MainPage = view;

            return viewModel;
        }

        public TViewModel SetNavigationRoot<TViewModel>()
            where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            var view = _viewFactory.Resolve(out viewModel);
            Application.Current.MainPage = new NavigationPage(view);

            return viewModel;
        }
    }
}
