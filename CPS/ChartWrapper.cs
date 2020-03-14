using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using CPS.Signal;

namespace CPS
{

    class ChartWrapper
    {
        public SeriesCollection SeriesCollection { get; } = new SeriesCollection();
        public Func<double, string> XFormatter { get; } = value => value.ToString();
        public Func<double, string> YFormatter { get; } = value => value.ToString();
        public double Frequency { get; set; } = 1000;
        private ISignal CurrentSignal;

        public ChartWrapper()
        {
            CurrentSignal = new SinusoidalSignal();
        }

        public void Generate()
        {
            SeriesCollection.Clear();
            ChartValues<double> values = new ChartValues<double>();

            double from = CurrentSignal.Params().t1;
            double to = from + CurrentSignal.Params().d;
            double step = CurrentSignal.Params().d / Frequency;
            for (double x = from; x <= to; x += step)
            {
                values.Add(CurrentSignal.y(x));
            }

            LineSeries ls = new LineSeries
            {
                Title = CurrentSignal.Name(),
                Values = values
            };

            System.Console.WriteLine(ls.Values.Count);

            SeriesCollection.Add(ls);
        }

    }
}
