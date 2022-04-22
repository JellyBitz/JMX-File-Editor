using System;
using System.Globalization;
using System.Windows;

namespace JMXFileEditor
{
    /// <summary>
    /// A converter that takes in a boolean and returns a <see cref="Visibility"/>
    /// </summary>
    public class ValueToVisibilityConverter : BaseValueConverter<ValueToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check like boolean logic
            if (parameter == null)
            {
                if(value is bool _bool)
                {
                    return _bool ? Visibility.Visible : Visibility.Collapsed;
                }
                else if (value is int _int)
                {
                    return _int != 0? Visibility.Visible : Visibility.Collapsed;
                }
            }
            // If any parameter is specified, then inverse the logic
            else
            {
                if (value is bool _bool)
                {
                    return _bool ? Visibility.Collapsed : Visibility.Visible;
                }
                else if (value is int _int)
                {
                    return _int != 0 ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            // No cases found
            return Visibility.Collapsed;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
