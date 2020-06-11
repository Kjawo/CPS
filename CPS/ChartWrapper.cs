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
        public SeriesCollection SeriesCollectionReal { get; } = new SeriesCollection();
        public SeriesCollection SeriesCollectionImaginary { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();
        private Series[] seriesReal = { null, null, null };
        private Series[] seriesImaginary = { null, null, null };

        public void Clear()
        {
            SeriesCollectionReal.Clear();
            SeriesCollectionImaginary.Clear();
        }

        public void SetSignal(int n, DiscreteSignal Signal)
        {
            if (Signal == null)
            {
                seriesReal[n] = null;
                seriesImaginary[n] = null;
                return;
            }

            ChartValues<ObservablePoint> valuesReal = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> valuesImaginary = new ChartValues<ObservablePoint>();
            valuesReal.AddRange(
                Signal.Values.Select(
                    tuple => new ObservablePoint { X = tuple.X.Real, Y = tuple.Y.Real }
                )
            );
            valuesImaginary.AddRange(
                Signal.Values.Select(
                    tuple => new ObservablePoint { X = tuple.X.Real, Y = tuple.Y.Imaginary }
                )
            );

            seriesReal[n] = CreateSeries(n, Signal, valuesReal);
            seriesImaginary[n] = CreateSeries(n, Signal, valuesImaginary);
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

                case SignalType.FOURIER:
                    return new ColumnSeries
                    {
                        Title = signal.Name,
                        Values = values,
                        MaxColumnWidth = 5.0,
                        MinHeight = 50.0,
                        Fill = ChartColors.List[n]
                    };

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Replot()
        {
            SeriesCollectionReal.Clear();
            SeriesCollectionImaginary.Clear();
            foreach (var serie in seriesReal)
            {
                if (serie != null)
                    SeriesCollectionReal.Add(serie);
            }
            foreach (var serie in seriesImaginary)
            {
                if (serie != null)
                    SeriesCollectionImaginary.Add(serie);
            }
        }
    }
}
