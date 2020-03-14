using System;

namespace CPS.Signal
{
    class SinusoidalSignal : ISignal
    {
        private Params p = new Params();

        public SinusoidalSignal()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        public string Name()
        {
            return "sin";
        }

        public double y(double x)
        {
            return p.A * Math.Sin(2 * Math.PI / p.T * (x - p.t1));
        }

        public Params Params()
        {
            return p;
        }

        public void SetParams(Params Params)
        {
            p = Params;
        }

    }
}
