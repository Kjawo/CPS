using System;

namespace CPS.Signal
{
    [Serializable]
    public class UniformDistributionNoise : BaseSignal
    {
        override public string Name { get => "uniformDistribution"; }

        public UniformDistributionNoise()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
        }

        protected override double yValueInRange(double x)
        {
            return 2* Params.A * MainWindow.Random.NextDouble() - Params.A;
        }
    }
}