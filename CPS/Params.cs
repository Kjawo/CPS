using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPS
{
    class Params
    {
        public double A { get; set; }
        public double T { get; set; }
        public double T1 { get; set; }
        public double d { get; set; }

        public Params(double a, double t, double t1, double d)
        {
            A = a;
            T = t;
            T1 = t1;
            this.d = d;
        }
    }
}
