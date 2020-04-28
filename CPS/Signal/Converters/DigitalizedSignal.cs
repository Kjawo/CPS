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
            if (value.Item2 >= 0) return Tuple.Create(value.Item1, PositiveQuant(value.Item2));
            return Tuple.Create(value.Item1, NegativeQuant(value.Item2));
        }

        private double PositiveQuant(double value)
        {
            double low = 0;
            double high = quantizationStep;
            while (value > high)
            {
                low = high;
                high += quantizationStep;
            }
            if (value < (high - low) / 2) return low;
            return high;
        }

        private double NegativeQuant(double value)
        {
            double low = -quantizationStep;
            double high = 0;
            while (value < low)
            {
                high = low;
                low -= quantizationStep;
            }
            if (value < (high - low) / 2) return low;
            return high;
        }
    }
}
