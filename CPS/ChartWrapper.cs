using System;
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
        public bool SecondSignalEnabled { get; set; } = true;
        public double Frequency { get; set; } = 100;
        private ISignal FirstSignal;
        private ISignal SecondSignal;

        public ChartWrapper()
        {
            FirstSignal = new SinusoidalSignal();
            SecondSignal = new SinusoidalSignal();
        }

        public void UpdateSignal(Params FirstParams, Params SecondParams)
        {
            FirstSignal.SetParams(FirstParams);
            SecondSignal.SetParams(SecondParams);
            Generate();
        }

        private void Generate()
        {
            SeriesCollection.Clear();
            AddSeriesForSignal(FirstSignal);
            if (SecondSignalEnabled)
            {
                AddSeriesForSignal(SecondSignal);
            }
        }

        private void AddSeriesForSignal(ISignal Signal)
        {
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            double from = Signal.Params().t1;
            double to = from + Signal.Params().d;
            double step = Signal.Params().d / Frequency;
            for (double x = from; x <= to; x += step)
            {
                values.Add(
                    new ObservablePoint
                    {
                        X = x,
                        Y = Signal.y(x)
                    }
                );
            }

            LineSeries ls = new LineSeries
            {
                Title = Signal.Name(),
                Values = values,

            };

            SeriesCollection.Add(ls);
        }
    }
}
