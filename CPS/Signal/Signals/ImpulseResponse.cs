using System;
using System.Collections.Generic;

namespace CPS.Signal.Signals
{
    class ImpulseResponse : DiscreteSignal
    {
        public ImpulseResponse()
        {
            Values = GenerateValues(8, 63);
        }

        private List<Tuple<double, double>> GenerateValues(int k, int m)
        {
            var values = new List<Tuple<double, double>>();
            for (double n = 0; n < m; n++)
            {
                double val = 0;
                if (n == (m - 1) / 2)
                    val = 2 / k;
                else
                    val = Math.Sin(2 * Math.PI * (n - ((m - 1) / 2)) / k) / (Math.PI * (n - ((m - 1) / 2)));
                values.Add(Tuple.Create(n, val));
            }
            return values;
        }
    }
}
