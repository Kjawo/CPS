using System;
using System.Collections.Generic;

namespace CPS.Signal.Signals
{
    class BandPassImpulseResponse : LowPassImpulseResponse
    {
        public BandPassImpulseResponse(int k, int m) : base(k, m)
        {
            Values = BandPassValues(Values);
        }

        private List<Value> BandPassValues(List<Value> values)
        {
            var updatedValues = new List<Value>();
            for (int n = 0; n < Values.Count; n++)
            {
                var newValue = Values[n].Y * 2.0 * Math.Sin(Math.PI * n / 2.0);
                updatedValues.Add(new Value { X = Values[n].X, Y = newValue });
            }
            return updatedValues;
        }
    }
}
