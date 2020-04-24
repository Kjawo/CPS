using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    class SincConverter : DigitalToAnalogConverter
    {
        override public DiscreteSignal Convert(DigitalizedSignal signal, double frequency)
        {
            return null;
        }
    }
}
