﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal
{
    [Serializable]
    public class DiscreteSignal
    {
        public string Name { get; private set; }
        private BaseSignal InputSignal;
        public double Frequency { get; private set; }
        private List<Tuple<double, double>> Values = new List<Tuple<double, double>>();

        public static DiscreteSignal ForParameters(string Name, double Frequency,
            List<Tuple<double, double>> Values)
        {
            DiscreteSignal Signal = new DiscreteSignal();
            Signal.Frequency = Frequency;
            Signal.Values = Values;
            Signal.Name = Name;
            return Signal;
        }

        private DiscreteSignal()
        {
        }

        public DiscreteSignal(double f, BaseSignal signal)
        {
            InputSignal = signal;
            Frequency = f;
            Name = signal.Name();
            BuildValues();
        }

        private void BuildValues()
        {
            double from = InputSignal.Params().t1;
            double to = from + InputSignal.Params().d;
            double step = InputSignal.Params().d / Frequency;
            for (double x = from; x <= to; x += step)
            {
                Values.Add(Tuple.Create(x, InputSignal.y(x)));
            }
        }

        public List<Tuple<double, double>> GetValues()
        {
            return Values;
        }
    }
}
