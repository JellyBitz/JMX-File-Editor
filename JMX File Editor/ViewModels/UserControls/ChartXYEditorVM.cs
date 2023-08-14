using JMXFileEditor.Silkroad.Data.JMXVENVI;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels
{
    public class ChartXYEditorVM : JMXProperty
    {
        #region Public Properties
        public double MinValue { get; }
        public double MaxValue { get; }
        public SeriesCollection Series { get; } = new SeriesCollection();
        public double Step { get; } = 0.2;
        public Func<double, string> Formatter => value => value.ToString("N2");
        #endregion

        #region Commands
        public ICommand CommandAddValue { get; private set; }
        public ICommand CommandRemoveValue { get; private set; }
        #endregion

        #region Constructors
        public ChartXYEditorVM(string name, List<EnvironmentFloatBlend> blends) : base(name, true)
        {
            // Cast values into ChartValues
            var values = new ChartValues<ObservablePoint>();
            foreach (var v in blends)
            {
                values.Add(new ObservablePoint(v.Time, v.Value));
                // Set up min/max
                if (MaxValue <= v.Time)
                    MaxValue = v.Time;
                if (MinValue > v.Time)
                    MinValue = v.Time;
            }
            // Create Line
            Series.Add(new LineSeries() {
                Values = values
            });

            InitializeCommands();
        }
        #endregion

        #region Public Methods
        public List<EnvironmentFloatBlend> GetEnvironmentFloatBlends()
        {
            List<EnvironmentFloatBlend> result = new List<EnvironmentFloatBlend>();
            foreach (var v in Series[0].Values.Cast<ObservablePoint>())
                result.Add(new EnvironmentFloatBlend() { Time = (float)v.X, Value = (float)v.Y });
            return result;
        }
        #endregion

        #region Private Helpers
        private void InitializeCommands()
        {
            CommandAddValue = new RelayCommand(() =>
            {
                Series[0].Values.Add(new ObservablePoint() { X = 1, Y = 1});
            });
            CommandRemoveValue = new RelayParameterizedCommand((parameter) =>
            {
                if (parameter is ObservablePoint value)
                    Series[0].Values.Remove(value);
            });
        }
        #endregion
    }
}
