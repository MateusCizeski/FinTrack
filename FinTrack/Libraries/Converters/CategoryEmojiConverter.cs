using FinTrack.Models;
using System.Globalization;

namespace FinTrack.Libraries.Converters
{
    public class CategoryEmojiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is TransactionCategory cat ? cat.ToEmoji() : "📦";
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class CategoryBadgeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is TransactionCategory cat ? cat.ToBadgeColor() : Colors.Transparent;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class CategoryBadgeTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is TransactionCategory cat ? cat.ToBadgeTextColor() : Color.FromArgb("#E8EAF0");
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    public class BalanceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool isPositive && isPositive
                ? Color.FromArgb("#00E5A0")
                : Color.FromArgb("#FF5C6A");
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}