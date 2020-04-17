using System;

namespace CPS.Signal
{
    [Serializable]
    public class GaussianNoise : BaseSignal
    {
        override public string Name { get => "gauss"; }

        public GaussianNoise()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
        }

        //See https://mathworld.wolfram.com/Box-MullerTransformation.html
        //and https://stackoverflow.com/questions/218060/random-gaussian-variables
        protected override double yValueInRange(double x)
        {
            double stdDev = Params.A / 3;
            double u1 = 1.0 - MainWindow.Random.NextDouble();
            double u2 = 1.0 - MainWindow.Random.NextDouble();
            double normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return normal * stdDev;
        }
    }
}