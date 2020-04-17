using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class UnitStep : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "unitStep"; }

        public UnitStep()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
            
            p.kw = 0.5;
            p.ts = 2;
        }

        protected override double yValueInRange(double x)
        {
            if (x > p.ts)
            {
                return p.A;
            }

            if (x.Equals(p.ts))
            {
                return 0.5 * p.A;
            }

            return 0;
        }
    }
}