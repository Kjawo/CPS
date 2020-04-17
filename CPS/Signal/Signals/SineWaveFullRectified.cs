using System;

namespace CPS.Signal
{
    [Serializable]
    public class SineWaveFullRectified : BaseSignal
    {
        private Params p = new Params();

        override public string Name { get => "sinFullRectified"; }

        public SineWaveFullRectified()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        protected override double yValueInRange(double x)
        {
            return p.A * Math.Abs(Math.Sin((2 * Math.PI / p.T) * (x - p.t1)));
        }
    }
}