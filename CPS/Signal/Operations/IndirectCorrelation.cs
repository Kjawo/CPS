using System;
using System.Collections.Generic;
using System.Linq;

namespace CPS.Signal.Operations
{
    public class IndirectCorrelation : SignalOperation
    {
        protected override string Name => "IndirectCorrelation";
        protected override List<Tuple<double, double>> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            var values = new List<Tuple<double, double>>();
            
            List<double> firstSignalItem2 = new List<double>();
            foreach (var tuple in a.Values)
            {
                firstSignalItem2.Add(tuple.Item2);
            }

            firstSignalItem2.Reverse();
            
            for(int i = 0; i < a.Values.Count; i++)
            {
                values.Add(Tuple.Create( a.Values[i].Item1, firstSignalItem2[i]));
            }
            
            a.Values = values;

            SignalOperation convolution = new Convolution();
            
            return convolution.Process(a, b).Values;
        }
    }
}