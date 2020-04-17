using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class SquareWaveSignal : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "squareWave"; }

        public SquareWaveSignal()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
            
            p.kw = 0.5;
            p.ts = 2;
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
    }
}