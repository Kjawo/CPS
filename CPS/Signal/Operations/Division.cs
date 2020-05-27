using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal.Operations
{
    class Division : SignalOperation
    {
        protected override string Name => "div";

        protected override List<Value> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            var Aggregated = new List<Value>();
            Aggregated.AddRange(a.Values);
            Aggregated.AddRange(b.Values);
            var Added = Aggregated
                .GroupBy(tuple => tuple.X)
                .Select(group => new Value {
                    X = group.Key,
                    Y = group.Select(tuple => tuple.Y).Aggregate((val1, val2) => val1 / val2)
                })
                .ToList();
            return Added;
        }
    }
}
