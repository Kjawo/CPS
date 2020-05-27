using System;
using System.Collections.Generic;
using System.Numerics;

namespace CPS.Signal.Signals
{
    public class HammingWindow : DiscreteSignal
    {
        public HammingWindow(DiscreteSignal signal, int m)
        {
            Values = GenerateValues(signal, m);
        }

        private List<Value> GenerateValues(DiscreteSignal signal, int m)
        {
            var values = new List<Value>();
            double w;

            for (int i = 0; i < signal.Values.Count; i++)
            {
                w = 0.53836 - (0.46164 * Math.Cos(2 * Math.PI * i / m));
                values.Add(new Value { X = signal.Values[i].X, Y = w * signal.Values[i].Y });
            }

            return values;
        }
    }
}