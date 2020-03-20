using System;
using static CPS.Generator;

namespace CPS.Signal
{
    class AmplitudeOperationSignal : ISignal
    {
        private ISignal A;
        private ISignal B;
        private Mode Mode;
        private Params p = new Params();

        public AmplitudeOperationSignal(ISignal A, ISignal B, Mode Mode)
        {
            this.A = A;
            this.B = B;
            this.Mode = Mode;
            // Set params
            Params pA = A.Params();
            Params pB = B.Params();
            double startTime = Math.Min(pA.t1, pB.t1);
            double endTime = Math.Max(pA.t1 + pA.d, pB.t1 + pB.d);
            p.t1 = startTime;
            p.d = endTime - startTime;
        }

        public string Name()
        {
            switch (Mode)
            {
                case Mode.SUM:
                    return "sum";
                case Mode.DIFF:
                    return "diff";
                case Mode.MUL:
                    return "mul";
                case Mode.DIV:
                    return "div";
                default:
                    return "";
            }
        }

        public double y(double x)
        {
            switch (Mode)
            {
                case Mode.SUM:
                    return A.y(x) + B.y(x);
                case Mode.DIFF:
                    return A.y(x) - B.y(x);
                case Mode.MUL:
                    return A.y(x) * B.y(x);
                case Mode.DIV:
                    return A.y(x) / B.y(x);
                default:
                    return 0;
            }
        }

        public Params Params()
        {
            return p;
        }

        public void SetParams(Params Params)
        {
        }

    }
}
