using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class UnitStep : BaseSignal
    {
        override public string Name { get => "unitStep"; }

        public UnitStep()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
            
            Params.kw = 0.5;
            Params.ts = 2;
        }

        protected override double yValueInRange(double x)
        {
            if (x > Params.ts)
            {
                return Params.A;
            }

            if (x.Equals(Params.ts))
            {
                return 0.5 * Params.A;
            }

            return 0;
        }
    }
}