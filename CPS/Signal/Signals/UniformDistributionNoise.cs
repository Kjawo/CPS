using System;

namespace CPS.Signal
{
    public class UniformDistributionNoise : BaseSignal
    {
        private Params p = new Params();

        public UniformDistributionNoise()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        public override string Name()
        {
            return "uniformD";
        }

        protected override double yValueInRange(double x)
        {
            return 2* p.A * MainWindow.Random.NextDouble() - p.A;
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