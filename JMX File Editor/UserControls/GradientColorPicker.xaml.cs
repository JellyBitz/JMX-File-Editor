using JMXFileEditor.UserControls.Models;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace JMXFileEditor.UserControls
{
    /// <summary>
    /// Lógica de interacción para GradientColorPicker.xaml
    /// </summary>
    public partial class GradientColorPicker : UserControl
    {
        #region Dependecy Properties
        /// <summary>
        /// Exposes minimum value from Gradient display
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        public static DependencyProperty MinimumProperty =
           DependencyProperty.Register("Minimum", typeof(double), typeof(GradientColorPicker), new PropertyMetadata(0d));
        /// <summary>
        /// Exposes maximum value from Gradient display
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        public static DependencyProperty MaximumProperty =
           DependencyProperty.Register("Maximum", typeof(double), typeof(GradientColorPicker), new PropertyMetadata(1d));
        /// <summary>
        /// Exposes the alpha value modification from items
        /// </summary>
        public bool ShowAlpha
        {
            get => (bool)GetValue(ShowAlphaProperty);
            set => SetValue(ShowAlphaProperty, value);
        }
        public static DependencyProperty ShowAlphaProperty =
           DependencyProperty.Register("ShowAlpha", typeof(bool), typeof(GradientColorPicker), new PropertyMetadata(true));
        /// <summary>
        /// Exposes the flag indicating if the items count can be modified
        /// </summary>
        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }
        public static DependencyProperty IsEditableProperty =
           DependencyProperty.Register("IsEditable", typeof(bool), typeof(GradientColorPicker), new PropertyMetadata(true));
        /// <summary>
        /// Exposes the flag indicating if the items count can be modified
        /// </summary>
        public bool IsSizeable
        {
            get => (bool)GetValue(IsSizeableProperty);
            set => SetValue(IsSizeableProperty, value);
        }
        public static DependencyProperty IsSizeableProperty =
           DependencyProperty.Register("IsSizeable", typeof(bool), typeof(GradientColorPicker), new PropertyMetadata(true));
        /// <summary>
        /// Gradient items being handled
        /// </summary>
        public IEnumerable<IGradientColorPickerItem> ItemsSource
        {
            get => (IEnumerable<IGradientColorPickerItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<IGradientColorPickerItem>), typeof(GradientColorPicker),
            new PropertyMetadata(new PropertyChangedCallback(OnItemsSourcePropertyChanged)));
        /// <summary>
        /// Command executed on adding action
        /// </summary>
        public ICommand CommandAddItem
        {
            get => (ICommand)GetValue(CommandAddItemProperty);
            set => SetValue(CommandAddItemProperty, value);
        }
        public static DependencyProperty CommandAddItemProperty =
            DependencyProperty.Register("CommandAddItem", typeof(ICommand), typeof(GradientColorPicker));
        /// <summary>
        /// Command executed on removing action
        /// </summary>
        public ICommand CommandRemoveItem
        {
            get => (ICommand)GetValue(CommandRemoveItemProperty);
            set => SetValue(CommandRemoveItemProperty, value);
        }
        public static DependencyProperty CommandRemoveItemProperty =
           DependencyProperty.Register("CommandRemoveItem", typeof(ICommand), typeof(GradientColorPicker));
        #endregion

        #region Constructor
        public GradientColorPicker()
        {
            InitializeComponent();
            Minimum = 0;
            Maximum = 1;
        }
        #endregion

        #region Private Helpers
        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;
            var _this = d as GradientColorPicker;
            _this?.OnItemsSourceChanged((IEnumerable<IGradientColorPickerItem>)e.OldValue, (IEnumerable<IGradientColorPickerItem>)e.NewValue);
        }
        private void OnItemsSourceChanged(IEnumerable<IGradientColorPickerItem> oldValue, IEnumerable<IGradientColorPickerItem> newValue)
        {
            if (oldValue is INotifyCollectionChanged oldINotifyCollectionChanged)
            {
                oldINotifyCollectionChanged.CollectionChanged -= OnItemsSourceCollectionChanged;
                foreach (var item in oldValue)
                {
                    if (item is INotifyPropertyChanged itemINotifyPropertyChanged)
                        itemINotifyPropertyChanged.PropertyChanged -= OnItemsSourceValuePropertyChanged;
                }
            }
            if (newValue is INotifyCollectionChanged newINotifyCollectionChanged)
            {
                newINotifyCollectionChanged.CollectionChanged += OnItemsSourceCollectionChanged;
                foreach (var item in newValue)
                {
                    if (item is INotifyPropertyChanged itemINotifyPropertyChanged)
                        itemINotifyPropertyChanged.PropertyChanged += OnItemsSourceValuePropertyChanged;
                }
            }
            UpdateGradientStopCollection();
        }
        private void OnItemsSourceValuePropertyChanged(object sender, PropertyChangedEventArgs e) => UpdateGradientStopCollection();
        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => UpdateGradientStopCollection();
        private void UpdateGradientStopCollection()
        {
            xGradientDisplay.GradientStops?.Clear();
            if (ItemsSource != null)
                xGradientDisplay.GradientStops = new GradientStopCollection(ItemsSource.Select(x => new GradientStop(x.Color, x.Offset)));
        }
        #endregion
    }
}
