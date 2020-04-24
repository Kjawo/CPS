﻿using System;
using System.Collections.Generic;

namespace CPS.Signal.Operations
{
    public abstract class SignalOperation
    {
        public DiscreteSignal Process(DiscreteSignal a, DiscreteSignal b)
        {
            List<Tuple<double, double>> newValues = NewValues(a, b);
            return DiscreteSignal.ForParameters("div", GetType(a.Type, b.Type), a.Frequency, newValues);
        }

        private SignalType GetType(SignalType type1, SignalType type2)
        {
            if (type1 == SignalType.DISCRETE || type1 == SignalType.DISCRETE)
                return SignalType.DISCRETE;
            return SignalType.CONTINUOUS;
        }

        protected abstract List<Tuple<double, double>> NewValues(DiscreteSignal a, DiscreteSignal b);
    }
}
