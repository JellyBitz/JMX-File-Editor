using System.ComponentModel;
using System.Windows.Media;
using System;

using JMXFileEditor.Silkroad.Mathematics;

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
            var alpha = new JMXAttribute("Alpha", Color.Alpha);
            // Add nodes
            Childs.Add(red);
            Childs.Add(green);
            Childs.Add(blue);
            Childs.Add(alpha);
            // Add handlers to update color brush
            void OnColorChange(object s, PropertyChangedEventArgs e)
            {
                DemoBrush = new SolidColorBrush(new Color()
                {
                    R = (byte)Math.Round((float)red.Value * 255),
                    G = (byte)Math.Round((float)green.Value * 255),
                    B = (byte)Math.Round((float)blue.Value * 255),
                    A = (byte)Math.Round((float)alpha.Value * 255),
                });
                ;
                OnPropertyChanged(nameof(DemoBrush));
            };
            red.PropertyChanged += OnColorChange;
            green.PropertyChanged += OnColorChange;
            blue.PropertyChanged += OnColorChange;
            alpha.PropertyChanged += OnColorChange;
            // Update it
            OnColorChange(null, null);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure, int i)
        {
            return new Color4()
            {
                Red = (float)((JMXAttribute)Structure.Childs[i++]).Value,
                Green = (float)((JMXAttribute)Structure.Childs[i++]).Value,
                Blue = (float)((JMXAttribute)Structure.Childs[i++]).Value,
                Alpha = (float)((JMXAttribute)Structure.Childs[i++]).Value
            };
        }
        #endregion
    }
}