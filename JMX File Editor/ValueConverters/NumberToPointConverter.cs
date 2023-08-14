using System;
using System.Globalization;
using System.Windows;

namespace JMXFileEditor
{
    /// <summary>
    /// A converter that creates a point from the given number
    /// </summary>
    public class NumberToPointConverter : BaseValueConverter<NumberToPointConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double _double)
            {
                return parameter != null && parameter.ToString() == "y" ? new Point() { Y = _double } : new Point() { X = _double };
            }
            else if (value is float _float)
            {
                return parameter != null && parameter.ToString() == "y" ? new Point() { Y = _float } : new Point() { X = _float };
            }
            else if (value is int _int)
            {
                return parameter != null && parameter.ToString() == "y" ? new Point() { Y = _int } : new Point() { X = _int };
            }
            return new Point();
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
