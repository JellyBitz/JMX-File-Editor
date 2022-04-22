using JMXFileEditor.Silkroad.Mathematics;
using System.ComponentModel;
using System.Windows.Media;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    public class Color4VM : JMXStructure
    {
        #region Public Properties
        public SolidColorBrush DemoBrush { get; private set; }
        #endregion

        #region Constructor
        public Color4VM(string Name, Color4 Color) : base(Name, true)
        {
            var red = new JMXAttribute("Red", Color.Red);
            var green = new JMXAttribute("Green", Color.Green);
            var blue = new JMXAttribute("Blue", Color.Blue);
            // Add nodes
            Childs.Add(red);
            Childs.Add(green);
            Childs.Add(blue);
            Childs.Add(new JMXAttribute("Alpha", Color.Alpha));
            // Add handlers to update color brush
            void OnColorChange(object s, PropertyChangedEventArgs e)
            {
                DemoBrush = new SolidColorBrush(new Color()
                {
                    R = (byte)((float)red.Value * 255),
                    G = (byte)((float)green.Value * 255),
                    B = (byte)((float)blue.Value * 255),
                    A = 255
                });
                ;
                OnPropertyChanged(nameof(DemoBrush));
            };
            red.PropertyChanged += OnColorChange;
            green.PropertyChanged += OnColorChange;
            blue.PropertyChanged += OnColorChange;
            // Update it
            OnColorChange(null, null);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            return new Color4()
            {
                Red = (float)((JMXAttribute)Structure.Childs[0]).Value,
                Green = (float)((JMXAttribute)Structure.Childs[1]).Value,
                Blue = (float)((JMXAttribute)Structure.Childs[2]).Value,
                Alpha = (float)((JMXAttribute)Structure.Childs[3]).Value
            };
        }
        #endregion
    }
}