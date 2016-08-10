using Autofac;
using PetrolPriceMonitor.Services;
using System.Collections.Generic;

namespace PetrolPriceMonitor.ViewModels
{
    public class TabViewModel : ViewModelBase
    {
        public List<IViewModel> ViewModels { get; protected set; }

        public TabViewModel(IComponentContext container, INavigate navigate)
        {
            ViewModels = new List<IViewModel>();

            ViewModels.Add(container.Resolve<HomeViewModel>());
            ViewModels.Add(container.Resolve<SearchViewModel>());
            ViewModels.Add(container.Resolve<ProfileViewModel>());
        }
    }
}
