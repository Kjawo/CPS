using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using CPS.Signal;

namespace CPS
{
    public class HistogramWrapper
    {
        public SeriesCollection SeriesCollection { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();
        private Series[] series = { null, null, null };
        public int HistogramGroupsCount { get; set; } = 100;
        double MinSignalAmplitude { get; set; } = -1;
        double MaxSignalAmplitude { get; set; } = 1;

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
            MinSignalAmplitude = Signal.Values.Min(tuple => tuple.Item2);
            MaxSignalAmplitude = Signal.Values.Max(tuple => tuple.Item2);

            var aggregated = Signal.Values
                .GroupBy(QuantizedGrouping)
                .Select(SelectSumOfOccurences);

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

        public void Replot()
        {
            SeriesCollection.Clear();
            foreach (var serie in series)
            {
                if (serie != null)
                    SeriesCollection.Add(serie);
            }
        }

        private double QuantizedGrouping(Tuple<double, double> tuple)
        {
            double step = (MaxSignalAmplitude - MinSignalAmplitude) / HistogramGroupsCount;
            double group = MinSignalAmplitude;
            while (group <= tuple.Item2) group += step;
            return group;
        }

        private Tuple<double, int> SelectSumOfOccurences(IGrouping<double, Tuple<double, double>> group)
        {
            return new Tuple<double, int>(group.Key, group.Sum(tuple => 1));
        }

    }
}
