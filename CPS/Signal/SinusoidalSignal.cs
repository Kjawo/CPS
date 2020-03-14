using System;

namespace CPS.Signal
{
    class SinusoidalSignal : ISignal
    {
        private Params par = new Params();

        public SinusoidalSignal()
        {
            par.A = 1;
            par.d = 10;
            par.T = 1;
            par.t1 = 0;
        }

        public string Name()
        {
            return "sin";
        }

        public double y(double x)
        {
            return Math.Sin(x);
        }

        public Params Params()
        {
            return par;
        }

        public void SetParams(Params Params)
        {
            par = Params;
        }

    }
}
