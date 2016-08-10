using System;
using System.Globalization;
using Xamarin.Forms;

namespace PetrolPriceMonitor.Converters
{
    public class UrlToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            return ImageSource.FromFile(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
