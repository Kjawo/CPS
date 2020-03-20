using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    public class TriangleWave : BaseSignal
    {
        private Params p = new Params();

        public TriangleWave()
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
            return "triangleWave";
        }

        protected override double yValueInRange(double x)
        {
            int k = (int)((x / p.T) - (p.t1 / p.T));
            if (x >= k * p.T + p.t1 && x < p.kw * p.T + k * p.T + p.t1)
            {
                return (p.A / (p.kw * p.T)) * (x - k * p.T - p.t1);
            }

            return -p.A / (p.T * (1 - p.kw)) * (x - k * p.T - p.t1) + (p.A / (1 - p.kw));
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