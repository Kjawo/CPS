namespace CPS.Signal
{
    interface ISignal
    {
        string Name();
        double y(double x);
        Params Params();
        void SetParams(Params Params);
    }
}
