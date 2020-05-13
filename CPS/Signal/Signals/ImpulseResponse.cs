using System;
using System.Collections.Generic;

namespace CPS.Signal.Signals
{
    class ImpulseResponse : DiscreteSignal
    {
        public ImpulseResponse(int k, int m)
        {
            Values = GenerateValues(k, m);
        }

        private List<Tuple<double, double>> GenerateValues(int k, int m)
        {
            var values = new List<Tuple<double, double>>();
            for (int n = 0; n < m; n++)
            {
                double val = 0;
                if (n == (m - 1) / 2)
                    val = 2.0 / k;
                else
                    val = Math.Sin(2 * Math.PI * (n - ((m - 1) / 2)) / k) / (Math.PI * (n - ((m - 1) / 2)));
                values.Add(Tuple.Create((double)n, val));
            }
            return values;
        }
    }
}
