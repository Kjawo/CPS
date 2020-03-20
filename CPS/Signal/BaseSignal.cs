namespace CPS.Signal
{
    public abstract class BaseSignal
    {
        public abstract string Name();

        public abstract Params Params();

        public abstract void SetParams(Params Params);

        public double y(double x)
        {
            if (x < Params().t1 || x > Params().t1 + Params().d)
            {
                return 0;
            }
            else return yValueInRange(x);
        }

        protected abstract double yValueInRange(double x);
    }
}
