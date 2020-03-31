using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    [Serializable]
    public class UnitImpulse : BaseSignal
    {
        private Params p = new Params();

        public UnitImpulse()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
            
            p.kw = 0.5;
            p.ts = 2;
        }

        public override string Name()
        {
            return "unitImpulse";
        }

        protected override double yValueInRange(double x)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (x == p.ts)  
                return p.A;   

            return 0;
        }

        public override DiscreteSignal ToDiscrete(double Frequency)
        {
            List<Tuple<double, double>> Values = new List<Tuple<double, double>>();
            double from = p.t1;
            double to = from + p.d;
            double step = p.d / Frequency;
            for (double x = from; x <= to; x += step)
            {
                if (Math.Abs(x - p.ts) < 0.0000001)
                    Values.Add(Tuple.Create(x, p.A));
                else
                    Values.Add(Tuple.Create(x, 0.0));
            }
            return DiscreteSignal.ForParameters(Name(), Frequency, Values);
        }

        public override Params Params()
        {
            return p;
        }

        public override void SetParams(Params Params)
        {
            p = Params;
        }
    }
}