using FinTrack.Models;
using System.Globalization;

namespace FinTrack.Libraries.Converters
{
    public class TransactionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Transaction t) return string.Empty;
            string prefix = t.Type == TransactionType.Income ? "+ " : "- ";
            return prefix + t.Value.ToString("C");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class TransactionValueColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Transaction t) return Color.FromArgb("#E8EAF0");
            return t.Type == TransactionType.Income ? Color.FromArgb("#00E5A0") : Color.FromArgb("#FF5C6A");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}