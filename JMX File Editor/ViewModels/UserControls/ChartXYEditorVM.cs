using JMXFileEditor.Silkroad.Data.JMXVENVI;
using JMXFileEditor.Silkroad.Mathematics;
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
        public double MinValue { get; private set; } = 0;
        public double MaxValue { get; private set; } = 1;
        public SeriesCollection Series { get; } = new SeriesCollection();
        public double Step { get; } = 0.25;
        public Func<double, string> Formatter => value => value.ToString("N2");
        #endregion

        #region Commands
        public ICommand CommandAddValue { get; private set; }
        public ICommand CommandRemoveValue { get; private set; }
        #endregion

        #region Constructors
        public ChartXYEditorVM(string name, List<EnvironmentFloatBlend> blends) : base(name, true)
        {
            // Default values
            if (blends.Count == 0)
            {
                blends.Add(new EnvironmentFloatBlend() { Time = 0, Value = 1});
                blends.Add(new EnvironmentFloatBlend() { Time = 1, Value = 1 });
            }
            // Cast values into ChartValues
            var values = new ChartValues<ObservablePoint>();
            foreach (var v in blends)
                values.Add(new ObservablePoint(v.Time, v.Value));
            Initialize(values);
        }
        public ChartXYEditorVM(string name,List<Vector2> graph) : base(name, true)
        {
            // Default values
            if(graph.Count == 0)
            {
                graph.Add(new Vector2(0,0));
                graph.Add(new Vector2(1,1));
            }
            // Cast values into ChartValues
            var values = new ChartValues<ObservablePoint>();
            foreach (var v in graph)
                values.Add(new ObservablePoint(v.X, v.Y));
            Initialize(values);
        }
        #endregion

        #region Public Methods
        public List<EnvironmentFloatBlend> GetEnvironmentFloatBlends()
        {
            var result = new List<EnvironmentFloatBlend>();
            foreach (var v in Series[0].Values.Cast<ObservablePoint>())
                result.Add(new EnvironmentFloatBlend() { Time = (float)v.X, Value = (float)v.Y });
            return result;
        }
        public List<Vector2> GetVector2()
        {
            var result = new List<Vector2>();
            foreach (var v in Series[0].Values.Cast<ObservablePoint>())
                result.Add(new Vector2((float)v.X, (float)v.Y));
            return result;
        }
        #endregion

        #region Private Helpers
        private void Initialize(ChartValues<ObservablePoint> values)
        {
            // Create Line
            Series.Add(new LineSeries()
            {
                Values = values
            });
            // Initialize Commands
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
