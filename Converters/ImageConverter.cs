using Avalonia.Data.Converters;
using Avalonia.Platform;
using System;
using System.Drawing;
using System.Globalization;

namespace DemoEkzZachet.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (value == null || value == "") 
                ? new Bitmap(AssetLoader.Open(new Uri("avares://DemoEkzZachet/Assets/picture.jpg")))
                : new Bitmap(AssetLoader.Open(new Uri($"avares://DemoEkzZachet/Assets/{value}")));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
