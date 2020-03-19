using System.Collections.Generic;
using CPS.Signal;

namespace CPS
{
    public class Generator
    {
        public enum Mode
        {
            DEFAULT, SUM, DIFF, MUL, DIV
        }

        private double Frequency = 1;
        private ISignal FirstSignal;
        private ISignal SecondSignal;
        private Mode OutputMode;

        public Generator withFrequency(double f)
        {
            Frequency = f;
            return this;
        }

        public Generator withSignal(ISignal signal)
        {
            FirstSignal = signal;
            return this;
        }

        public Generator withSecondarySignal(ISignal signal)
        {
            SecondSignal = signal;
            return this;
        }

        public Generator withMode(Mode mode)
        {
            this.OutputMode = mode;
            return this;
        }

        public DiscreteSignal build()
        {
            ISignal SignalToBuild = FirstSignal;
            if (OutputMode == Mode.SUM)
            {
                SignalToBuild = new SumSignal(FirstSignal, SecondSignal);
            }
            return new DiscreteSignal(Frequency, SignalToBuild);
        }

    }
}
