using System;
using CPS;
using CPS.Signal;

namespace CPS.Signal
{
    [Serializable]
    public class ImpulseNoise : BaseSignal
    {
        public override string Name { get => "impulseNoise"; }
        public override SignalType Type { get => SignalType.DISCRETE; }

        public ImpulseNoise()
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
            if (Params.p > MainWindow.Random.NextDouble())
            {
                return Params.A;
            }

            return 0;
        }
    }
}