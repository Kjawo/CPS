using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using CPS.Signal;

namespace CPS
{
    public class ChartWrapper
    {
        public SeriesCollection SeriesCollection { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();
        private Series[] series = { null, null, null };

        public void Clear()
        {
            SeriesCollection.Clear();
        }

        public void SetSignal(int n, DiscreteSignal Signal)
        {
            if (Signal == null)
            {
                series[n] = null;
                return;
            }

            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();
            values.AddRange(
                Signal.Values.Select(
                    tuple => new ObservablePoint { X = tuple.X.Real, Y = tuple.Y.Real }
                )
            );

            series[n] = CreateSeries(n, Signal, values);
        }

        private Series CreateSeries(int n, DiscreteSignal signal, ChartValues<ObservablePoint> values)
        {
            switch (signal.Type)
            {
                case SignalType.DISCRETE:
                    return new ScatterSeries
                    {
                        Title = signal.Name,
                        Values = values,
                        MaxPointShapeDiameter = 3,
                        Fill = ChartColors.List[n],
                    };

                case SignalType.CONTINUOUS:
                    return new LineSeries
                    {
                        Title = signal.Name,
                        Values = values,
                        Fill = System.Windows.Media.Brushes.Transparent,
                        PointGeometry = null,
                        Stroke = ChartColors.List[n],
                        LineSmoothness = 0
                    };

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Replot()
        {
            SeriesCollection.Clear();
            foreach (var serie in series)
            {
                if (serie != null)
                    SeriesCollection.Add(serie);
            }
        }
    }
}
