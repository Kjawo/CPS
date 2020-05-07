using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal.Operations
{
    class Sum : SignalOperation
    {
        protected override string Name => "sum";

        protected override List<Tuple<double, double>> NewValues(DiscreteSignal a, DiscreteSignal b)
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
            return Added;
        }
    }
}
