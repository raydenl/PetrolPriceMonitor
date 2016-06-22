using Autofac;
using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.ViewModels;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Views
{
    public class ViewLocatorPage : NavigationPage
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var viewModel = BindingContext as IViewModel;

            if (viewModel == null)
            {
                PushAsync(new ContentPage());

                return;
            }
            
            var viewFactory = App.Container.Resolve<IViewFactory>();
            
            var page = viewFactory.Resolve(viewModel);
            
            var contentPage = (ContentPage)page;

            PushAsync(contentPage);
        }
    }
}
