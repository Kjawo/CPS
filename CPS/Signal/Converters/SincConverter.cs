using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class SincConverter : DigitalToAnalogConverter
    {
        protected override List<Tuple<double, double>> NewValues(List<Tuple<double, double>> signalValues,
            List<double> newTimeValues, double frequency)
        {
            var values = new List<Tuple<double, double>>();

            double Ts = 1 / frequency;
            var sum = 0.0d;
            int SamplesCount = 100;
            
            foreach (var timeValue in newTimeValues)
            {
                sum = 0.0d;
                for (int n = (int)timeValue - SamplesCount; n < (int)timeValue + SamplesCount; n++)
                {
                    if (n >= 0 && n < signalValues.Count)
                    {
                        sum += signalValues[n].Item2 * SinC(timeValue / Ts - n);
                    }
                }

                values.Add(Tuple.Create(timeValue, sum));
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
