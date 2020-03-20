﻿using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using CPS.Signal;

namespace CPS
{
    class HistogramWrapper
    {
        public SeriesCollection SeriesCollection { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();
        public int HistogramGroupsCount { get; set; } = 100;
        double MinSignalAmplitude { get; set; } = -1;
        double MaxSignalAmplitude { get; set; } = 1;

        public void Clear()
        {
            SeriesCollection.Clear();
        }

        public void AddSeries(DiscreteSignal Signal)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            MinSignalAmplitude = Signal.GetValues().Min(tuple => tuple.Item2);
            MaxSignalAmplitude = Signal.GetValues().Max(tuple => tuple.Item2);

            var aggregated = Signal.GetValues()
                                   .GroupBy(QuantizedGrouping)
                                   .Select(SelectSumOfOccurences);

            values.AddRange(
                aggregated.Select(
                    tuple => new ObservablePoint { X = tuple.Item1, Y = (double) tuple.Item2 }
                )
            );

            ColumnSeries ls = new ColumnSeries
            {
                Title = Signal.Name,
                Values = values,
                MaxColumnWidth = 5.0
            };

            SeriesCollection.Add(ls);
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