using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    [Serializable]
    public class DiscreteSignal
    {
        public string Name { get; private set; }
        public double Frequency { get; private set; }
        public List<Tuple<double, double>> Values { get; protected set; } = new List<Tuple<double, double>>();

        public static DiscreteSignal ForParameters(string name, double frequency,
            List<Tuple<double, double>> values)
        {
            DiscreteSignal Signal = new DiscreteSignal();
            Signal.Frequency = frequency;
            Signal.Values.AddRange(values);
            Signal.Name = name;
            return Signal;
        }

        public DiscreteSignal()
        {
        }
    }
}
