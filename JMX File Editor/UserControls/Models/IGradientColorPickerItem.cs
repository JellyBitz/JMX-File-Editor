using System.Windows.Media;

namespace JMXFileEditor.UserControls.Models
{
    public interface IGradientColorPickerItem
    {
        Color Color { get; set; }
        double Offset { get; set; }
    }
}
