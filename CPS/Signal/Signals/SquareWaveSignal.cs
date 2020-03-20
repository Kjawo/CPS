using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    public class SquareWaveSignal : BaseSignal
    {
        private Params p = new Params();

        public SquareWaveSignal()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
            
            p.kw = 0.5;
            p.ts = 2;
        }

        public override string Name()
        {
            return "squareWave";
        }

        protected override double yValueInRange(double x)
        {
            int k = (int)((x / p.T) - (p.t1 / p.T));
            if (x >= (k * p.T + p.t1) && x < (p.kw * p.T + k * p.T + p.t1))
            {
                return p.A;
            }

            return 0;
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