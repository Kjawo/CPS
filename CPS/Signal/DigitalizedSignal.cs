using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CPS.Signal
{
    public class DigitalizedSignal : DiscreteSignal
    {
        private double quantizationStep = 1;
        public double frequency { get; private set; } = 1;
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

        private List<Value> Sample(DiscreteSignal signal, double frequency)
        {
            var values = new List<Value>();
            double samplingStep = 1 / frequency;
            double nextValidTime = signal.Values[0].X.Real;
            foreach (var value in signal.Values)
            {
                if (value.X.Real >= nextValidTime)
                {
                    values.Add(value);
                    nextValidTime += samplingStep;
                }
            }
            Console.WriteLine(values.Count);
            Console.WriteLine(signal.Values.Count);
            return values;
        }

        private Value Quantize(Value value)
        {
            double newValue = Math.Round(value.X.Real / quantizationStep) * quantizationStep;
            return new Value { X = value.X, Y = new Complex(newValue, 0) };
        }
    }
}
