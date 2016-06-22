using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        public List<IViewModel> ViewModels { get; protected set; }

        public RootViewModel(IComponentContext container)
        {
            ViewModels = new List<IViewModel>();

            ViewModels.Add(container.Resolve<HomeViewModel>());
            ViewModels.Add(container.Resolve<SearchViewModel>());
        }
    }
}
