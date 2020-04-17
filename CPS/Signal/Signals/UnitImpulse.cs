using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    [Serializable]
    public class UnitImpulse : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "unitImpulse"; }

        public UnitImpulse()
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
            double step = 1 / Frequency;
            for (double x = from; x <= to; x += step)
            {
                if (Math.Abs(x - p.ts) < 0.0000001)
                    Values.Add(Tuple.Create(x, p.A));
                else
                    Values.Add(Tuple.Create(x, 0.0));
            }
            return DiscreteSignal.ForParameters(Name, Frequency, Values);
        }
    }
}