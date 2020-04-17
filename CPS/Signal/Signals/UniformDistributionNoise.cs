using System;

namespace CPS.Signal
{
    [Serializable]
    public class UniformDistributionNoise : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "uniformDistribution"; }

        public UniformDistributionNoise()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        protected override double yValueInRange(double x)
        {
            return 2* p.A * MainWindow.Random.NextDouble() - p.A;
        }
    }
}