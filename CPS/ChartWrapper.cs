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
        public double Frequency { get; set; } = 100;
        private ISignal CurrentSignal;

        public ChartWrapper()
        {
            CurrentSignal = new SinusoidalSignal();
        }

        public void UpdateSignal(Params Params)
        {
            CurrentSignal.SetParams(Params);
            Generate();
        }

        private void Generate()
        {
            SeriesCollection.Clear();
            ChartValues<ObservablePoint> values = new ChartValues<ObservablePoint>();

            double from = CurrentSignal.Params().t1;
            double to = from + CurrentSignal.Params().d;
            double step = CurrentSignal.Params().d / Frequency;
            for (double x = from; x <= to; x += step)
            {
                values.Add(
                    new ObservablePoint
                    {
                        X = x,
                        Y = CurrentSignal.y(x)
                    }
                );
            }

            LineSeries ls = new LineSeries
            {
                Title = CurrentSignal.Name(),
                Values = values,
                
            };

            SeriesCollection.Add(ls);
        }

    }
}
