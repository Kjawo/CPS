using System;

namespace CPS.Signal
{
    [Serializable]
    public class SineWaveHalfRectified : BaseSignal
    {
        override public string Name { get => "sinHalfRectified"; }

        public SineWaveHalfRectified()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
        }

        protected override double yValueInRange(double x)
        {
            return 0.5 * Params.A * (Math.Sin((2 * Math.PI / Params.T) * (x - Params.t1)) +
                                Math.Abs(Math.Sin((2 * Math.PI / Params.T) * (x - Params.t1))));
        }
    }
}