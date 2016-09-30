﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace PetrolPriceMonitor.TriggerActions
{
    public class FocusTriggerAction : TriggerAction<SearchBar>
    {
        public bool Focused { get; set; }

        protected override async void Invoke(SearchBar searchBar)
        {
            await Task.Delay(1000);

            if (Focused)
            {
                searchBar.Focus();
            }
            else
            {
                searchBar.Unfocus();
            }
        }
    }
}
