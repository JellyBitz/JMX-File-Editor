using System.Windows.Input;
using System.Windows.Media;

namespace JMXFileEditor.UserControls
{
    public class GradientColorPickerVM : BaseViewModel
    {
        #region Private Members
        private double m_Minimum = 0;
        private double m_Maximum = 1;
        #endregion

        #region Public Properties
        public ItemChangeObservableCollection<GradientColorData> GradientItemsSource { get; } = new ItemChangeObservableCollection<GradientColorData>()
        {
        	// Black ~ White by default
               new GradientColorData(){ Color = new Color(){ A = 255}, Offset = 0 },
               new GradientColorData(){ Color = new Color(){ A = 255, R = 255, G = 255, B = 255 }, Offset = 1 },
        };
        public double Minimum
        {
            get => m_Minimum;
            set
            {
                m_Minimum = value;
                OnPropertyChanged(nameof(Minimum));
            }
        }
        public double Maximum
        {
            get => m_Maximum;
            set
            {
                m_Maximum = value;
                OnPropertyChanged(nameof(Maximum));
            }
        }
        #endregion

        #region Commands
        public ICommand CommandAddColor { get; }
        public ICommand CommandRemoveColor { get; }
        #endregion

        #region Constructor
        public GradientColorPickerVM()
        {

            CommandAddColor = new RelayCommand(() =>
            {
                GradientItemsSource.Add(new GradientColorData()
                {
                    Color = new Color() { A = 255 },
                    Offset = Maximum
                });
            });
            CommandRemoveColor = new RelayParameterizedCommand((selectedItem) =>
            {
                if (!(selectedItem is GradientColorData item))
                    return;

                GradientItemsSource.Remove(item);
            });
        }
        #endregion

        #region Internal Classes
        public class GradientColorData : BaseViewModel
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
