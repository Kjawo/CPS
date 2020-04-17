using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class SquareWaveSignal : BaseSignal
    {
        override public string Name { get => "squareWave"; }

        public SquareWaveSignal()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
            
            Params.kw = 0.5;
            Params.ts = 2;
        }
        protected override double yValueInRange(double x)
        {
            int k = (int)((x / Params.T) - (Params.t1 / Params.T));
            if (x >= (k * Params.T + Params.t1) && x < (Params.kw * Params.T + k * Params.T + Params.t1))
            {
                return Params.A;
            }

            return 0;
        }
    }
}