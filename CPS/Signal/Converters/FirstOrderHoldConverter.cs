using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class FirstOrderHoldConverter : DigitalToAnalogConverter
    {
        protected override List<Tuple<double, double>> NewValues(List<Tuple<double, double>> signalValues,
            List<double> newTimeValues, double frequency)
        {
            var values = new List<Tuple<double, double>>();
            var p1 = 0;
            var p2 = 1;
            foreach (var timeValue in newTimeValues)
            {
                if (timeValue > signalValues[p2].Item1)
                {
                    p1++;
                    p2++;
                }

                // Linear function between points of digital signal
                double dx = (signalValues[p2].Item1 - signalValues[p1].Item1);
                double dy = (signalValues[p2].Item2 - signalValues[p1].Item2);
                double a = dy / dx;
                double b = signalValues[p1].Item2 - a * signalValues[p1].Item1;

                values.Add(Tuple.Create(timeValue, a * timeValue + b));
            }
            return values;
        }
    }
}
