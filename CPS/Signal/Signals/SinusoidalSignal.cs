using System;

namespace CPS.Signal
{
    [Serializable]
    class SinusoidalSignal : BaseSignal
    {
        override public string Name { get => "sin"; }

        public SinusoidalSignal()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
        }

        override protected double yValueInRange(double x)
        {
            return Params.A * Math.Sin(2 * Math.PI / Params.T * (x - Params.t1));
        }
    }
}
