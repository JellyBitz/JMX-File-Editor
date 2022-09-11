using System;
using System.ComponentModel;
using System.Windows.Media;

using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    public class Color3VM : JMXStructure
    {
        #region Public Properties
        public SolidColorBrush DemoBrush { get; private set; }
        #endregion

        #region Constructor
        public Color3VM(string Name, Color3 Color) : base(Name, true)
        {
            var red = new JMXAttribute("Red", Color.Red);
            var green = new JMXAttribute("Green", Color.Green);
            var blue = new JMXAttribute("Blue", Color.Blue);
            // Add nodes
            Childs.Add(red);
            Childs.Add(green);
            Childs.Add(blue);
            // Add handlers to update color brush
            void OnColorChange(object s, PropertyChangedEventArgs e)
            {
                DemoBrush = new SolidColorBrush(new Color()
                {
                    R = (byte)Math.Round((float)red.Value * 255),
                    G = (byte)Math.Round((float)green.Value * 255),
                    B = (byte)Math.Round((float)blue.Value * 255),
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
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Color3()
            {
                Red = (float)((JMXAttribute)s.Childs[i++]).Value,
                Green = (float)((JMXAttribute)s.Childs[i++]).Value,
                Blue = (float)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}