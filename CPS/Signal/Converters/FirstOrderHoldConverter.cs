using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class FirstOrderHoldConverter : DigitalToAnalogConverter
    {
        override public DiscreteSignal Convert(DigitalizedSignal signal, double frequency)
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

                // Linear function between points of digital signal
                double dx = (signal.Values[p2].Item1 - signal.Values[p1].Item1);
                double dy = (signal.Values[p2].Item2 - signal.Values[p1].Item2);
                double a = dy / dx;
                double b = signal.Values[p1].Item2 - a * signal.Values[p1].Item1;

                values.Add(Tuple.Create(timeValue, a * timeValue + b));
            }

            return DiscreteSignal.ForParameters(signal.Name, frequency, values);
        }
    }
}
