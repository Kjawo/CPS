using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class TriangleWave : BaseSignal
    {
        override public string Name { get => "triangleWave"; }

        public TriangleWave()
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
            if (x >= k * Params.T + Params.t1 && x < Params.kw * Params.T + k * Params.T + Params.t1)
            {
                return (Params.A / (Params.kw * Params.T)) * (x - k * Params.T - Params.t1);
            }

            return -Params.A / (Params.T * (1 - Params.kw)) * (x - k * Params.T - Params.t1) + (Params.A / (1 - Params.kw));
        }
    }
}