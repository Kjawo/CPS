using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal.Operations
{
    class SumSignalOperation : SignalOperation
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
                    group.Select(tuple => tuple.Item2).Sum()
                ))
                .ToList();
            return DiscreteSignal.ForParameters("sum", a.Frequency, Added);
        }
    }
}
