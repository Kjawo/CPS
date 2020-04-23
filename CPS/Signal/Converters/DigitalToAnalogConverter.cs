using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Converters
{
    public interface DigitalToAnalogConverter
    {
        DiscreteSignal convert(DigitalizedSignal signal, double frequency);
    }
}
