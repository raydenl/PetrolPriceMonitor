using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged
    {
        string Title { get; set; }

        void SetState<T>(Action<T> action) where T : class, IViewModel;
    }
}
