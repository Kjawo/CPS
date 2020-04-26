using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal
{
    public class DigitalizedSignal : DiscreteSignal
    {
        private double quantizationStep = 1;
        public double  frequency { get; private set; } = 1;

        public SignalType OriginalType { get; private set; }

        public DigitalizedSignal(DiscreteSignal signal, double quantizationStep, double frequency)
        {
            this.Name = signal.Name;
            this.Type = SignalType.DISCRETE;
            this.OriginalType = signal.Type;
            this.quantizationStep = quantizationStep;
            this.frequency = frequency;
            Values = Sample(signal, frequency)
                .Select(Quantize)
                .ToList();
        }

        private List<Tuple<double, double>> Sample(DiscreteSignal signal, double frequency)
        {
            var values = new List<Tuple<double, double>>();
            double samplingStep = 1 / frequency;
            double nextValidTime = signal.Values[0].Item1;
            foreach (var value in signal.Values)
            {
                if (value.Item1 >= nextValidTime)
                {
                    values.Add(value);
                    nextValidTime += samplingStep;
                }
            }
            Console.WriteLine(values.Count);
            Console.WriteLine(signal.Values.Count);
            return values;
        }

        private Tuple<double, double> Quantize(Tuple<double, double> value)
        {
            double sign = Math.Sign(value.Item2);
            double low = 0;
            double high = quantizationStep;
            while (value.Item2 * sign > low)
            {
                low = high;
                high += quantizationStep;
            }
            if (value.Item2 < ((high - low) / 2))
                return Tuple.Create(value.Item1, sign * low);
            return Tuple.Create(value.Item1, sign * high);
        }
    }
}
