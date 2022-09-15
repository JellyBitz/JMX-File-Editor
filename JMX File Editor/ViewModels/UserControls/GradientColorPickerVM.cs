using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.UserControls.Models;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace JMXFileEditor.ViewModels
{
    public class GradientColorPickerVM : JMXProperty
    {
        #region Private Members
        private float m_Begin = 0;
        private float m_End = 1;
        #endregion

        #region Public Properties
        public ObservableCollection<GradientValue> GradientValues { get; } = new ObservableCollection<GradientValue>();
        public float Begin
        {
            get => m_Begin;
            set
            {
                m_Begin = value;
                OnPropertyChanged(nameof(Begin));
            }
        }
        public float End
        {
            get => m_End;
            set
            {
                m_End = value;
                OnPropertyChanged(nameof(End));
            }
        }
        #endregion

        #region Commands
        public ICommand CommandAddColor { get; }
        public ICommand CommandRemoveColor { get; }
        #endregion

        #region Constructor
        public GradientColorPickerVM(string name, float begin, float end, EEBlend<Color32, DiffuseBlend> values) : base(name, true)
        {
            // Make sure values are right
            if (begin > end)
            {
                var temp = end;
                end = begin;
                begin = end;
            }
            Begin = FloatClamp(begin, 0f, 1f);
            End = FloatClamp(end, 0f, 1f);
            foreach (var v in values)
                GradientValues.Add(new GradientValue() { Color = ColorVM.GetColor(v.Value), Offset = v.Time });

            // Make default gradient
            if (values.Points.Count == 0)
            {
                GradientValues.Add(new GradientValue() { Color = new Color() { A = 255 }, Offset = 0 });
                GradientValues.Add(new GradientValue() { Color = new Color() { A = 255, R = 255, G = 255, B = 255 }, Offset = 1 });
            }

            #region Commands
            CommandAddColor = new RelayCommand(() =>
            {
                GradientValues.Add(new GradientValue()
                {
                    Color = new Color() { A = 255 },
                    Offset = End
                });
            });
            CommandRemoveColor = new RelayParameterizedCommand((selectedItem) =>
            {
                if (!(selectedItem is GradientValue item))
                    return;

                GradientValues.Remove(item);
            });
            #endregion
        }
        #endregion

        #region Private Helpers
        private float FloatClamp(float value, float min, float max) => value < min ? min : value > max ? max : value;
        #endregion

        #region Internal Classes
        public class GradientValue : BaseViewModel, IGradientColorPickerItem
        {
            #region Private Members
            private Color m_Color;
            private double m_Offset;
            #endregion

            #region Public Properties
            public Color Color
            {
                get => m_Color;
                set
                {
                    m_Color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
            public double Offset
            {
                get => m_Offset;
                set
                {
                    m_Offset = value;
                    OnPropertyChanged(nameof(Offset));
                }
            }
            #endregion
        }
        #endregion
    }
}
