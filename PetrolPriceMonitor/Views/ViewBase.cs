using PetrolPriceMonitor.Constants;
using PetrolPriceMonitor.Models;
using PetrolPriceMonitor.ViewModels;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Views
{
    public class ViewBase : ContentPage
    {
        public ViewBase() { }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as ViewModelBase;

            if (viewModel != null)
            {
                await viewModel.ViewAppearing();
            }

            SubscribeDisplayAlert();
        }

        async protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var viewModel = BindingContext as ViewModelBase;

            if (viewModel != null)
            {
                await viewModel.ViewDisappearing();
            }

            UnsubscribeDisplayAlert();
        }

        private void SubscribeDisplayAlert()
        {
            MessagingCenter.Subscribe<Application, DisplayAlertMessage>(this, MessageCenterMessage.ShowAlert, async (sender, message) =>
            {
                var result = true;

                if (!string.IsNullOrEmpty(message.Accept))
                    result = await DisplayAlert(message.Title, message.Message, message.Accept, message.Cancel);
                else
                    await DisplayAlert(message.Title, message.Message, message.Cancel);

                message.OnCompleted?.Invoke(result);

            }, Application.Current);
        }

        private void UnsubscribeDisplayAlert()
        {
            MessagingCenter.Unsubscribe<Application, DisplayAlertMessage>(this, MessageCenterMessage.ShowAlert);
        }
    }
}
