using System;

namespace CPS.Signal
{
    [Serializable]
    public class SineWaveHalfRectified : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "sinHalfRectified"; }

        public SineWaveHalfRectified()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        protected override double yValueInRange(double x)
        {
            return 0.5 * p.A * (Math.Sin((2 * Math.PI / p.T) * (x - p.t1)) +
                                Math.Abs(Math.Sin((2 * Math.PI / p.T) * (x - p.t1))));
        }
    }
}