using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

        protected virtual bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> propertyExpression)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyExpression);

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

        async public virtual Task ViewAppearing()
        {
            await Task.Delay(0);
        }

        async public  virtual Task ViewDisappearing()
        {
            await Task.Delay(0);
        }
    }
}
