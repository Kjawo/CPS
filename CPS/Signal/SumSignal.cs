using System;

namespace CPS.Signal
{
    class SumSignal : ISignal
    {
        private ISignal A;
        private ISignal B;

        public SumSignal(ISignal A, ISignal B)
        {
            this.A = A;
            this.B = B;
        }

        public string Name()
        {
            return "sum";
        }

        public double y(double x)
        {
            return A.y(x) + B.y(x);
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
