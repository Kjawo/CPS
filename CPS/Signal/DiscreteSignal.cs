using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    [Serializable]
    public class DiscreteSignal
    {
        public SignalType Type { get; protected set; }
        public string Name { get; protected set; }
        public double Frequency { get; private set; }
        public List<Tuple<double, double>> Values { get; protected set; } = new List<Tuple<double, double>>();

        public static DiscreteSignal ForParameters(string name, SignalType type, double frequency,
            List<Tuple<double, double>> values)
        {
            DiscreteSignal signal = new DiscreteSignal();
            signal.Type = type;
            signal.Frequency = frequency;
            signal.Values.AddRange(values);
            signal.Name = name;
            return signal;
        }

        public DiscreteSignal()
        {
        }
    }
}
