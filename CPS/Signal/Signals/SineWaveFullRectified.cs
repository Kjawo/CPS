using System;

namespace CPS.Signal
{
    [Serializable]
    public class SineWaveFullRectified : BaseSignal
    {
        override public string Name { get => "sinFullRectified"; }

        public SineWaveFullRectified()
        {
            Params.A = 1;
            Params.d = 10;
            Params.T = 1;
            Params.t1 = 0;
        }

        protected override double yValueInRange(double x)
        {
            return Params.A * Math.Abs(Math.Sin((2 * Math.PI / Params.T) * (x - Params.t1)));
        }
    }
}