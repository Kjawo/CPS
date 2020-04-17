using System;

namespace CPS.Signal
{
    [Serializable]
    class SinusoidalSignal : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "sin"; }

        public SinusoidalSignal()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        override protected double yValueInRange(double x)
        {
            return p.A * Math.Sin(2 * Math.PI / p.T * (x - p.t1));
        }
    }
}
