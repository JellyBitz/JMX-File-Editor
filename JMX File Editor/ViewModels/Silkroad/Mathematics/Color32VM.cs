using System.ComponentModel;
using System.Windows.Media;

using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    public class Color32VM : JMXStructure
    {
        #region Public Properties
        public SolidColorBrush DemoBrush { get; private set; }
        #endregion

        #region Constructor
        public Color32VM(string Name, Color32 Color) : base(Name, true)
        {
            var r = new JMXAttribute("Red", Color.Red);
            var g = new JMXAttribute("Green", Color.Green);
            var b = new JMXAttribute("Blue", Color.Blue);
            var a = new JMXAttribute("Alpha", Color.Alpha);
            // Add nodes
            Childs.Add(r);
            Childs.Add(g);
            Childs.Add(b);
            Childs.Add(a);
            // Add handlers to update color brush
            void OnColorChange(object s, PropertyChangedEventArgs e)
            {
                DemoBrush = new SolidColorBrush(new Color()
                {
                    R = (byte)r.Value,
                    G = (byte)g.Value,
                    B = (byte)b.Value,
                    A = (byte)a.Value,
                });
                ;
                OnPropertyChanged(nameof(DemoBrush));
            };
            r.PropertyChanged += OnColorChange;
            g.PropertyChanged += OnColorChange;
            b.PropertyChanged += OnColorChange;
            a.PropertyChanged += OnColorChange;
            // Update it
            OnColorChange(null, null);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure, int i)
        {
            return new Color32()
            {
                Red = (byte)((JMXAttribute)Structure.Childs[i++]).Value,
                Green = (byte)((JMXAttribute)Structure.Childs[i++]).Value,
                Blue = (byte)((JMXAttribute)Structure.Childs[i++]).Value,
                Alpha = (byte)((JMXAttribute)Structure.Childs[i++]).Value
            };
        }
        #endregion
    }
}