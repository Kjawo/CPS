using System;

namespace CPS.Signal
{
    [Serializable]
    public class SineWaveFullRectified : BaseSignal
    {
        private Params p = new Params();

        public SineWaveFullRectified()
        {
            p.A = 1;
            p.d = 10;
            p.T = 1;
            p.t1 = 0;
        }

        public override string Name()
        {
            return "sinFullRectified";
        }

        protected override double yValueInRange(double x)
        {
            return p.A * Math.Abs(Math.Sin((2 * Math.PI / p.T) * (x - p.t1)));
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