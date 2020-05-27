using System;
using System.Collections.Generic;

namespace CPS.Signal.Operations
{
    public abstract class SignalOperation
    {
        protected abstract string Name { get; }

        public DiscreteSignal Process(DiscreteSignal a, DiscreteSignal b)
        {
            List<Value> newValues = NewValues(a, b);
            return DiscreteSignal.ForParameters(Name, GetType(a.Type, b.Type), a.Frequency, newValues);
        }

        private SignalType GetType(SignalType type1, SignalType type2)
        {
            if (type1 == SignalType.DISCRETE || type1 == SignalType.DISCRETE)
                return SignalType.DISCRETE;
            return SignalType.CONTINUOUS;
        }

        protected abstract List<Value> NewValues(DiscreteSignal a, DiscreteSignal b);
    }
}
