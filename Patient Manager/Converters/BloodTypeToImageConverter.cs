using System.Globalization;
using System.Windows.Data;

namespace Patient_Manager.Converters;

// Converts BloodType from Patient into string for Image path
internal class BloodTypeToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return $"Images/{value}.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}