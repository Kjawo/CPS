using System;
using System.Collections.Generic;

namespace CPS.Signal.Operations
{
    public class DirectCorrelation : SignalOperation
    {
        protected override string Name => "DirectCorrelation";
        protected override List<Tuple<double, double>> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            List<Tuple<double, double>> values = new List<Tuple<double, double>>();

            for (int i = b.Values.Count - 1; i >= (-1) * a.Values.Count; i--)
            {
                double sum = 0;
                for (int j = 0; j < a.Values.Count; j++)
                {
                    if (i + j < 0 || i + j >= b.Values.Count)
                        continue;

                    sum += a.Values[j].Item2 * b.Values[i + j].Item2;
                }
                values.Add(Tuple.Create((double) i, sum));
            }

            return values;
        }
    }
}