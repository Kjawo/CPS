using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class FirstOrderHoldConverter : DigitalToAnalogConverter
    {
        protected override List<Value> NewValues(List<Value> signalValues,
            List<double> newTimeValues, double frequency)
        {
            var values = new List<Value>();
            var p1 = 0;
            var p2 = 1;
            foreach (var timeValue in newTimeValues)
            {
                if (timeValue > signalValues[p2].X.Real)
                {
                    p1++;
                    p2++;
                }

                // Linear function between points of digital signal
                Complex dx = (signalValues[p2].X - signalValues[p1].X);
                Complex dy = (signalValues[p2].Y - signalValues[p1].Y);
                Complex a = dy / dx;
                Complex b = signalValues[p1].Y - a * signalValues[p1].X;

                values.Add(new Value { X = timeValue, Y = a * timeValue + b });
            }
            return values;
        }
    }
}
