using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class SquareWaveSymetricalSignal : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "squareWave"; }

        public SquareWaveSymetricalSignal()
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

            return -p.A;
        }
    }
}