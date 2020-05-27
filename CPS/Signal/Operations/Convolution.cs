using System;
using System.Collections.Generic;
using System.Numerics;

namespace CPS.Signal.Operations
{
    class Convolution : SignalOperation
    {
        protected override string Name => "convoluted";

        protected override List<Value> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            var values = new List<Value>();
            int p = a.Values.Count;
            int o = b.Values.Count;
            for (int n = 0; n < p + o - 1; n++)
            {
                Complex value = 0;
                for (int k = 0; k < a.Values.Count; k++)
                {
                    Complex firstVal = a.Values[k].Y;
                    Complex secondVal = (n - k < 0 || n - k >= o) ? 0 : b.Values[n - k].Y;
                    value += firstVal * secondVal;
                }
                values.Add(new Value { X = n, Y = value });
            }
            return values;
        }
    }
}
