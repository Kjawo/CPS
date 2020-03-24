using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal.Operations
{
    class DivSignalOperation : SignalOperation
    {
        public override DiscreteSignal Process(DiscreteSignal a, DiscreteSignal b)
        {
            var Aggregated = new List<Tuple<double, double>>();
            Aggregated.AddRange(a.GetValues());
            Aggregated.AddRange(b.GetValues());
            var Added = Aggregated
                .GroupBy(tuple => tuple.Item1)
                .Select(group => new Tuple<double, double>(
                    group.Key,
                    group.Select(tuple => tuple.Item2).Aggregate((val1, val2) => val1 / val2))
                )
                .ToList();
            return DiscreteSignal.ForParameters("div", a.Frequency, Added);
        }
    }
}
