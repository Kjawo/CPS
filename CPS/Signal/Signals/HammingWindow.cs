using System;
using System.Collections.Generic;

namespace CPS.Signal.Signals
{
    public class HammingWindow : DiscreteSignal
    {
        public HammingWindow(DiscreteSignal signal, int m)
        {
            Values = GenerateValues(signal, m);
        }

        private List<Tuple<double, double>> GenerateValues(DiscreteSignal signal, int m)
        {
            var values = new List<Tuple<double, double>>();
            double w;

            for (int i = 0; i < signal.Values.Count; i++)
            {
                w =  0.53836 - (0.46164 * Math.Cos(2 * Math.PI * i / m));
                values.Add(Tuple.Create(signal.Values[i].Item1, w * signal.Values[i].Item2));
            }

            return values;
        }
    }
}