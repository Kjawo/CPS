﻿using System;

namespace CPS.Signal
{
    public class GaussianNoise : BaseSignal
    {
        private Params p = new Params();

        public GaussianNoise()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        public override string Name()
        {
            return "gauss";
        }

        //See https://mathworld.wolfram.com/Box-MullerTransformation.html
        //and https://stackoverflow.com/questions/218060/random-gaussian-variables
        protected override double yValueInRange(double x)
        {
            double stdDev = p.A / 3;
            double u1 = 1.0 - MainWindow.Random.NextDouble();
            double u2 = 1.0 - MainWindow.Random.NextDouble();
            double normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return normal * stdDev;
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