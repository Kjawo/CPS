using System;
using System.Collections.Generic;
using System.Numerics;

namespace CPS.Signal.Signals
{
    class LowPassImpulseResponse : DiscreteSignal
    {
        public LowPassImpulseResponse(int k, int m)
        {
            Values = GenerateValues(k, m);
        }

        private List<Value> GenerateValues(int k, int m)
        {
            var values = new List<Value>();
            for (int n = 0; n < m; n++)
            {
                Complex val = 0;
                if (n == (m - 1) / 2)
                    val = 2.0 / k;
                else
                    val = Math.Sin(2 * Math.PI * (n - ((m - 1) / 2)) / k) / (Math.PI * (n - ((m - 1) / 2)));
                values.Add(new Value { X = n, Y = val });
            }
            return values;
        }
    }
}
