using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using CPS.Signal;
using System.Collections.Generic;

namespace CPS
{
    public class HistogramWrapper
    {
        public SeriesCollection SeriesCollection { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();
        private Series[] series = { null, null, null };
        public int HistogramGroupsCount { get; set; } = 100;

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

            var aggregated = AggregateHistogram(Signal.Values);

            values.AddRange(
                aggregated.Select(
                    tuple => new ObservablePoint { X = tuple.Item1, Y = (double) tuple.Item2 }
                )
            );

            series[n] = new ColumnSeries
            {
                Title = Signal.Name,
                Values = values,
                MaxColumnWidth = 5.0,
                Fill = ChartColors.List[n]
            };
        }

        private List<Tuple<double,int>> AggregateHistogram(List<Tuple<double, double>> values)
        {
            double min = values.Min(point => point.Item2);
            double max = values.Max(point => point.Item2);
            double step = (max - min) / HistogramGroupsCount;

            return values.Select(tuple => Math.Floor((tuple.Item2 - min) / step))
                .GroupBy(value => value)
                .Select(group => Tuple.Create(min + step * group.Key, group.Count()))
                .ToList();
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
