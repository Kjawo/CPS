using System;
using System.Collections.Generic;
using System.Numerics;

namespace CPS.Signal.Operations
{
    public class DirectCorrelation : SignalOperation
    {
        protected override string Name => "DirectCorrelation";
        protected override List<Value> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            var values = new List<Value>();

            for (int i = b.Values.Count - 1; i >= (-1) * a.Values.Count; i--)
            {
                Complex sum = 0;
                for (int j = 0; j < a.Values.Count; j++)
                {
                    if (i + j < 0 || i + j >= b.Values.Count)
                        continue;

                    sum += a.Values[j].Y * b.Values[i + j].Y;
                }
                values.Add(new Value { X = i, Y = sum });
            }

            return values;
        }
    }
}