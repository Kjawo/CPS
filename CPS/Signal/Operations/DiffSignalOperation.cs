using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal.Operations
{
    class DiffSignalOperation : SignalOperation
    {
        public override DiscreteSignal Process(DiscreteSignal a, DiscreteSignal b)
        {
            var Aggregated = new List<Tuple<double, double>>();
            Aggregated.AddRange(a.Values);
            Aggregated.AddRange(b.Values);
            var Added = Aggregated
                .GroupBy(tuple => tuple.Item1)
                .Select(group => new Tuple<double, double>(
                    group.Key,
                    group.Select(tuple => tuple.Item2).Aggregate((val1, val2) => val1 - val2))
                )
                .ToList();
            return DiscreteSignal.ForParameters("diff", a.Frequency, Added);
        }
    }
}
