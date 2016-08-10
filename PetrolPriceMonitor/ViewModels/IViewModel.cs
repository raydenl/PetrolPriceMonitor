using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }

        void SetState<T>(Action<T> action) where T : class, IViewModel;

        Task ViewAppearing();

        Task ViewDisappearing();
    }
}
