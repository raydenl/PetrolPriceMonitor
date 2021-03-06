﻿using PetrolPriceMonitor.ViewModels;
using System;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Services
{
    public interface INavigate
    {
        Task<IViewModel> PopAsync();

        Task<IViewModel> PopModalAsync();

        Task PopToRootAsync();

        Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction = null, bool tabbedPage = false)
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel, bool tabbedPage = false)
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;

        Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;

        TViewModel SetRoot<TViewModel>()
            where TViewModel : class, IViewModel;

        TViewModel SetNavigationRoot<TViewModel>()
            where TViewModel : class, IViewModel;
    }
}
