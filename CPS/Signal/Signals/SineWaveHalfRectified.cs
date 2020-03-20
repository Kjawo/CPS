using System;

namespace CPS.Signal
{
    public class SineWaveHalfRectified : BaseSignal
    {
        private Params p = new Params();

        public SineWaveHalfRectified()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        public override string Name()
        {
            return "sinHalfRectified";
        }

        protected override double yValueInRange(double x)
        {
            return 0.5 * p.A * (Math.Sin((2 * Math.PI / p.T) * (x - p.t1)) +
                                Math.Abs(Math.Sin((2 * Math.PI / p.T) * (x - p.t1))));
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