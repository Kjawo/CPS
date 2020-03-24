using System;
using System.Runtime.Serialization;

namespace CPS.Signal
{
    [Serializable]
    public abstract class BaseSignal : ICloneable
    {
        public abstract string Name();

        public abstract Params Params();

        public abstract void SetParams(Params Params);

        public double y(double x)
        {
            if (x < Params().t1 || x > Params().t1 + Params().d)
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
