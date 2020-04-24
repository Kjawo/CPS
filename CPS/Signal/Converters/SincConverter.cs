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
            List<double> newTimeValues)
        {
            return null;
        }
    }
}
