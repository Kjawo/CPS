using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class ZeroOrderHoldConverter : DigitalToAnalogConverter
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
                values.Add(new Value { X = timeValue, Y = signalValues[p1].Y });
            }
            return values;
        }
    }
}
