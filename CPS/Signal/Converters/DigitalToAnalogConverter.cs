﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    public abstract class DigitalToAnalogConverter
    {
        public DiscreteSignal Convert(DigitalizedSignal signal, double frequency)
        {
            List<double> timeValues = NewTimeValues(signal, frequency);
            List<Value> values = NewValues(signal.Values, timeValues, signal.frequency);
            return DiscreteSignal.ForParameters(signal.Name, signal.OriginalType, frequency, values);
        }

        protected abstract List<Value> NewValues(List<Value> signalValues,
            List<double> newTimeValues, double frequency);

        public List<double> NewTimeValues(DigitalizedSignal signal, double frequency)
        {
            double step = 1 / frequency;
            double beginTime = signal.Values.First().X.Real;
            double endTime = signal.Values.Last().X.Real;
            var timeValues = new List<double>();
            for (double x = beginTime; x <= endTime; x += step)
            {
                timeValues.Add(x);
            }
            return timeValues;
        }
    }
}
