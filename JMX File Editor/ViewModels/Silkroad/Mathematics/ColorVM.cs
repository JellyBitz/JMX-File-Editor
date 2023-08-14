using JMXFileEditor.Silkroad.Mathematics;

using System;
using System.Windows.Media;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    public class ColorVM : JMXStructure
    {
        #region Private Properties
        private Color m_Color;
        #endregion

        #region Public Properties
        /// <summary>
        /// Contains the current color
        /// </summary>
        public Color Color
        {
            get => m_Color;
            set
            {
                m_Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        /// <summary>
        /// Check if the alpha channel can be edited
        /// </summary>
        public bool ShowAlpha { get; }
        #endregion

        #region Constructor
        public ColorVM(string Name, Color color, bool showAlpha = true) : base(Name, true)
        {
            Color = color;
            ShowAlpha = showAlpha;
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s) => ((ColorVM)s).Color;
        public Color4 GetColor4()
        {
            return new Color4()
            {
                Red = Color.R / 255f,
                Green = Color.G / 255f,
                Blue = Color.B / 255f,
                Alpha = Color.A / 255f,
            };
        }
        public Color3 GetColor3() => GetColor3(Color);
        public Color32 GetColor32() => new Color32(Color.R, Color.G, Color.B, Color.A);
        #endregion

        #region Public Static Helpers
        public static Color GetColor(Color32 c)
        {
            return new Color()
            {
                R = c.Red,
                G = c.Green,
                B = c.Blue,
                A = c.Alpha,
            };
        }
        public static Color GetColor(Color4 c)
        {
            var result = new Color();
            // Check limits
            if (c.Red > 0)
                result.R = c.Red > 1 ? byte.MaxValue : (byte)Math.Round(c.Red * 255);
            if (c.Green > 0)
                result.G = c.Green > 1 ? byte.MaxValue : (byte)Math.Round(c.Green * 255);
            if (c.Blue > 0)
                result.B = c.Blue > 1 ? byte.MaxValue : (byte)Math.Round(c.Blue * 255);
            if (c.Alpha > 0)
                result.A = c.Alpha > 1 ? byte.MaxValue : (byte)Math.Round(c.Alpha * 255);
            return result;
        }
        public static Color GetColor(Color3 c)
        {
            var result = new Color() { A = 255 };
            // Check limits
            if (c.Red > 0)
                result.R = c.Red > 1 ? byte.MaxValue : (byte)Math.Round(c.Red * 255);
            if (c.Green > 0)
                result.G = c.Green > 1 ? byte.MaxValue : (byte)Math.Round(c.Green * 255);
            if (c.Blue > 0)
                result.B = c.Blue > 1 ? byte.MaxValue : (byte)Math.Round(c.Blue * 255);
            return result;
        }
        public static Color3 GetColor3(Color c)
        {
            return new Color3()
            {
                Red = c.R / 255f,
                Green = c.G / 255f,
                Blue = c.B / 255f,
            };
        }
        #endregion
    }
}