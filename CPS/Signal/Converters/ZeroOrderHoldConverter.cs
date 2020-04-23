using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class ZeroOrderHoldConverter : DigitalToAnalogConverter
    {
        public DiscreteSignal convert(DigitalizedSignal signal, double frequency)
        {
            var values = new List<Tuple<double, double>>();

            List<double> timeValues = NewTimeValues(signal, frequency);

            var p1 = 0;
            var p2 = 1;
            foreach (var timeValue in timeValues)
            {
                if (timeValue > signal.Values[p2].Item1)
                {
                    p1++;
                    p2++;
                }
                values.Add(Tuple.Create(timeValue, signal.Values[p1].Item2));
            }

            return DiscreteSignal.ForParameters(signal.Name, frequency, values);
        }

        private List<double> NewTimeValues(DigitalizedSignal signal, double frequency)
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
