﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PetrolPriceMonitor.ViewModels
{
    public abstract class ViewModelBase : IViewModel
    {
        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            action(this as T);
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);

            OnPropertyChanged(propertyName);
        }
    }
}
