using System;
using System.Globalization;

namespace JMXFileEditor
{
    /// <summary>
    /// A converter that takes an object to return his <see cref="Type"/> as string
    /// </summary>
    public class ObjectToTypeConverter : BaseValueConverter<ObjectToTypeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : value.GetType().Name;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
