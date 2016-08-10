using Autofac;
using PetrolPriceMonitor.Factories;
using PetrolPriceMonitor.ViewModels;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Views
{
    public class ViewLocatorPage : ViewBase
    {
        public static readonly BindableProperty ViewModelProperty =
        BindableProperty.Create("ViewModel", typeof(IViewModel), typeof(ViewLocatorPage), null);

        public IViewModel ViewModel
        {
            get { return (IViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
            if (ViewModel == null)
            {
                Content = null;

                return;
            }
            
            var viewFactory = App.Container.Resolve<IViewFactory>();
            
            var page = viewFactory.Resolve(ViewModel);

            var contentPage = (ContentPage)page;
            
            Title = ViewModel.Title;
            BindingContext = ViewModel;
            foreach(var toolbarItem in contentPage.ToolbarItems)
            {
                ToolbarItems.Add(toolbarItem);
            }
            Content = contentPage.Content;
        }
    }
}
