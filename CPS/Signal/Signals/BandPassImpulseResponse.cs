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

        private List<Tuple<double, double>> BandPassValues(List<Tuple<double, double>> values)
        {
            var updatedValues = new List<Tuple<double, double>>();
            for (int n = 0; n < Values.Count; n++)
            {
                var newValue = Values[n].Item2 * 2.0 * Math.Sin(Math.PI * n / 2.0);
                updatedValues.Add(Tuple.Create(Values[n].Item1, newValue));
            }
            return updatedValues;
        }
    }
}
