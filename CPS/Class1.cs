﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS
{
    class Class1
    {
        public double SineSignal(double time)
        {
            double A = 1;
            double T = 1;
            double T1 = 0;
            return A * Math.Sin((2 * Math.PI / T) * (time - T1));
        }
    }
}
