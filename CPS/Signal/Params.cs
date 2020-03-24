using System;

namespace CPS.Signal
{
    [Serializable]
    public class Params
    {
        public double A { get; set; } = 1;
        public double T { get; set; } = 1;
        public double t1 { get; set; } = 0;
        public double d { get; set; } = 4;
        public double kw { get; set; } = 0.5;
        public double ts { get; set; } = 2;
        public double p { get; set; } = 0.5;

    }
}
