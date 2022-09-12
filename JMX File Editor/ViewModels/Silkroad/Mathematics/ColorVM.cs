using JMXFileEditor.Silkroad.Mathematics;
using ColorPicker.Models;

using System;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels.Silkroad.Mathematics
{
    public class ColorVM : JMXStructure
    {
        #region Private Properties
        private ColorState m_Color;
        private bool m_IsEditing;
        #endregion

        #region Public Properties
        /// <summary>
        /// Contains the current color
        /// </summary>
        public ColorState Color {
            get => m_Color;
            set
            {
                m_Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        /// <summary>
        /// Check if color is on editing mode
        /// </summary>
        public bool IsEditing
        {
            get => m_IsEditing;
            set
            {
                m_IsEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        /// <summary>
        /// Check if the alpha channel can be edited
        /// </summary>
        public bool ShowAlpha { get; }
        #endregion

        #region Commands
        /// <summary>
        /// Start/stop editing the color
        /// </summary>
        public ICommand CommandEditColor { get; }
        #endregion

        #region Constructor
        public ColorVM(string Name, Color32 color, bool showAlpha = true) : base(Name, true)
        {
            this.Color = new ColorState()
            {
                RGB_R = color.Red / 255f,
                RGB_G = color.Green / 255f,
                RGB_B = color.Blue / 255f,
                A = color.Alpha / 255f,
            };
            this.ShowAlpha = showAlpha;

            // Set commands
            CommandEditColor = new RelayCommand(() =>
            {
                IsEditing = !IsEditing;
            });
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Color32()
            {
                Red = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Green = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Blue = (byte)((JMXAttribute)s.Childs[i++]).Value,
                Alpha = (byte)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        public static Color32 GetColor32(Color4 color)
        {
            var result = new Color32();
            // Check limits
            if (color.Red > 0)
                result.Red = color.Red > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Red * 255);
            if (color.Green > 0)
                result.Green = color.Green > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Green * 255);
            if (color.Blue > 0)
                result.Blue = color.Blue > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Blue * 255);
            if (color.Alpha > 0)
                result.Alpha = color.Alpha > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Alpha * 255);
            return result;
        }
        public static Color32 GetColor32(Color3 color)
        {
            var result = new Color32();
            // Check limits
            if (color.Red > 0)
                result.Red = color.Red > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Red * 255);
            if (color.Green > 0)
                result.Green = color.Green > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Green * 255);
            if (color.Blue > 0)
                result.Blue = color.Blue > byte.MaxValue ? byte.MaxValue : (byte)Math.Round(color.Blue * 255);
            return result;
        }
        public static Color4 GetColor4(Color32 color)
        {
            return new Color4()
            {
                Red = color.Red / 255f,
                Green = color.Green / 255f,
                Blue = color.Blue / 255f,
                Alpha = color.Alpha / 255f,
            };
        }
        public static Color3 GetColor3(Color32 color)
        {
            return new Color3()
            {
                Red = color.Red / 255f,
                Green = color.Green / 255f,
                Blue = color.Blue / 255f,
            };
        }
        #endregion
    }
}