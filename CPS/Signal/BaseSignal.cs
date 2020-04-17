using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    [Serializable]
    public abstract class BaseSignal : ICloneable
    {
        public abstract string Name { get; }

        public Params Params { get; set; } = new Params();

        virtual public DiscreteSignal ToDiscrete(double Frequency)
        {
            List<Tuple<double, double>> Values = new List<Tuple<double, double>>();
            double from = Params.t1;
            double to = from + Params.d;
            double step = 1 / Frequency;
            for (double x = from; x <= to; x += step)
            {
                Values.Add(Tuple.Create(x, y(x)));
            }
            return DiscreteSignal.ForParameters(Name, Frequency, Values);
        }

        public double y(double x)
        {
            if (x < Params.t1 || x > Params.t1 + Params.d)
            {
                return 0;
            }
            else return yValueInRange(x);
        }

        protected abstract double yValueInRange(double x);

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
