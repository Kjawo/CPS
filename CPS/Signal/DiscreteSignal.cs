﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal
{
    public class DiscreteSignal
    {
        public string Name { get => InputSignal.Name(); }
        private BaseSignal InputSignal;
        private double Frequency;
        private List<Tuple<double, double>> Values = new List<Tuple<double, double>>();

        public DiscreteSignal(double f, BaseSignal signal)
        {
            InputSignal = signal;
            Frequency = f;
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
