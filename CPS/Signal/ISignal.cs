namespace CPS.Signal
{
    public interface ISignal
    {
        string Name();
        double y(double x);
        Params Params();
        void SetParams(Params Params);
    }
}
