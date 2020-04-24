using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    [Serializable]
    public class UnitImpulse : BaseSignal
    {
        public override string Name { get => "unitImpulse"; }
        public override SignalType Type { get => SignalType.DISCRETE; }

        public UnitImpulse()
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
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (x == Params.ts)  
                return Params.A;   

            return 0;
        }

        public override DiscreteSignal ToDiscrete(double Frequency)
        {
            List<Tuple<double, double>> Values = new List<Tuple<double, double>>();
            double from = Params.t1;
            double to = from + Params.d;
            double step = 1 / Frequency;
            for (double x = from; x <= to; x += step)
            {
                if (Math.Abs(x - Params.ts) < 0.0000001)
                    Values.Add(Tuple.Create(x, Params.A));
                else
                    Values.Add(Tuple.Create(x, 0.0));
            }
            return DiscreteSignal.ForParameters(Name, Type, Frequency, Values);
        }
    }
}