﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    public abstract class DigitalToAnalogConverter
    {
        public abstract DiscreteSignal Convert(DigitalizedSignal signal, double frequency);

        public List<double> NewTimeValues(DigitalizedSignal signal, double frequency)
        {
            double step = 1 / frequency;
            double beginTime = signal.Values.First().Item1;
            double endTime = signal.Values.Last().Item1;
            var timeValues = new List<double>();
            for (double x = beginTime; x <= endTime; x += step)
            {
                timeValues.Add(x);
            }
            return timeValues;
        }
    }
}