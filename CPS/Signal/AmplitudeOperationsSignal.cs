using static CPS.Generator;

namespace CPS.Signal
{
    class AmplitudeOperationSignal : ISignal
    {
        private ISignal A;
        private ISignal B;
        private Mode Mode;

        public AmplitudeOperationSignal(ISignal A, ISignal B, Mode Mode)
        {
            this.A = A;
            this.B = B;
            this.Mode = Mode;
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
            return new Params();
        }

        public void SetParams(Params Params)
        {
        }

    }
}
