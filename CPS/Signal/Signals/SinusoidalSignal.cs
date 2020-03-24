using System;

namespace CPS.Signal
{
    [Serializable]
    class SinusoidalSignal : BaseSignal
    {
        private Params p = new Params();

        public SinusoidalSignal()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        override public string Name()
        {
            return "sin";
        }

        override protected double yValueInRange(double x)
        {
            return p.A * Math.Sin(2 * Math.PI / p.T * (x - p.t1));
        }

        override public Params Params()
        {
            return p;
        }

        override public void SetParams(Params Params)
        {
            p = Params;
        }

    }
}
