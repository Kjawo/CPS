using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class SincConverter : DigitalToAnalogConverter
    {
        protected override List<Value> NewValues(List<Value> signalValues,
            List<double> newTimeValues, double frequency)
        {
            var values = new List<Value>();

            double Ts = 1 / frequency;
            var sum = Complex.Zero;
            int SamplesCount = 100;
            
            foreach (var timeValue in newTimeValues)
            {
                sum = 0.0d;
                for (int n = (int)timeValue - SamplesCount; n < (int)timeValue + SamplesCount; n++)
                {
                    if (n >= 0 && n < signalValues.Count)
                    {
                        sum += signalValues[n].Y * SinC(timeValue / Ts - n);
                    }
                }

                values.Add(new Value { X = timeValue, Y = sum });
            }
            return values;
        }

        private double SinC(double y)
        {
            // When y equals/is close to 0 - return 1
            if (Math.Abs(y) < 0.000000000000001)
            {
                return 1;
            }

            return Math.Sin(Math.PI * y) / (Math.PI * y);
        }
    }
}
