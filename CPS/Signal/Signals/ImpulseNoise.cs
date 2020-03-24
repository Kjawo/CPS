using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class ImpulseNoise : BaseSignal
    {
        private Params p = new Params();

        public ImpulseNoise()
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
            return "impulseNoise";
        }

        protected override double yValueInRange(double x)
        {
            if (p.p > MainWindow.Random.NextDouble())
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