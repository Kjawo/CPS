using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal
{
    public class QuantizedSignal : DiscreteSignal
    {
        private double quantizationStep = 1;

        public QuantizedSignal(DiscreteSignal singal, double quantizationStep)
        {
            this.quantizationStep = quantizationStep;
            Values = Values
                .Select(Quantize)
                .ToList();
        }

        private Tuple<double, double> Quantize(Tuple<double, double> value)
        {
            double low = 0;
            double high = quantizationStep;
            while (value.Item2 < low)
            {
                low = high;
                high += quantizationStep;
            }
            if (value.Item2 < ((high - low) / 2))
                return Tuple.Create(value.Item1, low);
            return Tuple.Create(value.Item1, high);
        }
    }
}
