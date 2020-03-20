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
        
        public enum SignalEnum
        {
            sin, gauss, uniformD
        }

        private double Frequency = 1;
        private BaseSignal FirstSignal;
        private BaseSignal SecondSignal;
        private Mode OutputMode;

        public Generator withFrequency(double f)
        {
            Frequency = f;
            return this;
        }

        public Generator withSignal(BaseSignal signal)
        {
            FirstSignal = signal;
            return this;
        }

        public Generator withSecondarySignal(BaseSignal signal)
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
            BaseSignal SignalToBuild = FirstSignal;
            if (OutputMode != Mode.DEFAULT)
            {
                SignalToBuild = new AmplitudeOperationSignal(FirstSignal, SecondSignal, OutputMode);
            }
            return new DiscreteSignal(Frequency, SignalToBuild);
        }

        public static BaseSignal GetSelectedSignal(SignalWrapper s)
        {
            switch (s.Signal)
            {
                case SignalEnum.sin:
                    return new SinusoidalSignal();
                case SignalEnum.gauss:
                    return new GaussianNoise();
                case SignalEnum.uniformD:
                    return new UniformDistributionNoise();
                default:
                    return new SinusoidalSignal();
            }
        }
    }
}
