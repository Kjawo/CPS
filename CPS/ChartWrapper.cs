using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using CPS.Signal;

namespace CPS
{

    class ChartWrapper
    {
        public SeriesCollection SeriesCollection { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();

        public void Clear()
        {
            SeriesCollection.Clear();
        }

        public void AddSeries(DiscreteSignal Signal)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            values.AddRange(
                Signal.GetValues().Select(
                    tuple => new ObservablePoint { X = tuple.Item1, Y = tuple.Item2 }
                )
            );

            if (Signal.Name.Equals("unitImpulse") || Signal.Name.Equals("impulseNoise"))
            {
                ScatterSeries ls = new ScatterSeries
                {
                    Title = Signal.Name,
                    Values = values,
                    MaxPointShapeDiameter = 6
                };
                SeriesCollection.Add(ls);
            }
            else
            {
                LineSeries ls = new LineSeries
                {
                    Title = Signal.Name,
                    Values = values,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null
                };
                SeriesCollection.Add(ls);
            }

        }
    }
}
