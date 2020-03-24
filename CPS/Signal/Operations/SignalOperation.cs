namespace CPS.Signal.Operations
{
    public abstract class SignalOperation
    {
        public abstract DiscreteSignal Process(DiscreteSignal a, DiscreteSignal b);
    }
}
