using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Data.JMXVENVI;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.UserControls.Models;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

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
        public ItemChangeObservableCollection<GradientValue> GradientValues { get; } = new ItemChangeObservableCollection<GradientValue>();
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
        public bool ShowAlpha { get; private set; }
        #endregion

        #region Commands
        public ICommand CommandAddColor { get; private set; }
        public ICommand CommandRemoveColor { get; private set; }
        #endregion

        #region Constructor
        public GradientColorPickerVM(string name, float begin, float end, EEBlend<Color32, DiffuseBlend> values, bool ShowAlpha = true) : base(name, true)
        {
            foreach (var v in values)
                GradientValues.Add(new GradientValue() { Color = ColorVM.GetColor(v.Value), Offset = v.Time });
            InitializeValues(begin, end, ShowAlpha);
        }
        public GradientColorPickerVM(string name, float begin, float end, EnvironmentCurve<Color3, EnvironmentColorBlend> values) : base(name, true)
        {
            foreach (var v in values.Blends)
                GradientValues.Add(new GradientValue() { Color = ColorVM.GetColor(v.Value), Offset = v.Time });
            InitializeValues(begin, end, ShowAlpha);
        }
        #endregion

        #region Public Methods
        public EEBlend<Color32, DiffuseBlend> GetDiffuseBlend()
        {
            var result = new EEBlend<Color32, DiffuseBlend>()
            {
                Begin = Begin,
                End = End,
            };
            foreach (var v in GradientValues)
                result.Points.Add(new DiffuseBlend() { 
                    Value = new Color32(v.Color.R, v.Color.G, v.Color.B, v.Color.A),
                    Time = (float)v.Offset 
                });
            return result;
        }
        public EnvironmentCurve<Color3, EnvironmentColorBlend> GetEnvironmentColorBlend()
        {
            var result = new EnvironmentCurve<Color3, EnvironmentColorBlend>();
            foreach (var v in GradientValues)
                result.Blends.Add(new EnvironmentColorBlend()
                {
                    Value = ColorVM.GetColor3(v.Color),
                    Time = (float)v.Offset
                });
            return result;
        }
        #endregion

        #region Private Helpers
        private void InitializeValues(float begin, float end, bool showAlpha)
        {
            ShowAlpha = showAlpha;
            Begin = FloatClamp(begin < end ? begin : end, 0f, 1f);
            End = FloatClamp(begin < end ? end : begin, 0f, 1f);
            // Make default gradient
            if (GradientValues.Count == 0)
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
