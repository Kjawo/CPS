using System;
using System.Collections.Generic;

namespace CPS.Signal.Operations
{
    class Convolution : SignalOperation
    {
        protected override string Name => "convoluted";

        protected override List<Tuple<double, double>> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            var values = new List<Tuple<double, double>>();
            int p = a.Values.Count;
            int o = b.Values.Count;
            for (int n = 0; n < p + o - 1; n++)
            {
                double value = 0;
                for (int k = 0; k < a.Values.Count; k++)
                {
                    double firstVal = a.Values[k].Item2;
                    double secondVal = (n - k < 0 || n - k >= o) ? 0 : b.Values[n - k].Item2;
                    value += firstVal * secondVal;
                }
                values.Add(Tuple.Create(n * 1.0, value));
            }
            return values;
        }
    }
}
