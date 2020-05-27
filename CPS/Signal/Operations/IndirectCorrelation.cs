using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CPS.Signal.Operations
{
    public class IndirectCorrelation : SignalOperation
    {
        protected override string Name => "IndirectCorrelation";
        protected override List<Value> NewValues(DiscreteSignal a, DiscreteSignal b)
        {
            var values = new List<Value>();
            
            List<Complex> firstSignalItem2 = new List<Complex>();
            foreach (var tuple in a.Values)
            {
                firstSignalItem2.Add(tuple.Y);
            }

            firstSignalItem2.Reverse();
            
            for(int i = 0; i < a.Values.Count; i++)
            {
                //values.Add(Tuple.Create(a.Values[i].X, firstSignalItem2[i]));
                values.Add(new Value { X = a.Values[i].X, Y = firstSignalItem2[i] });
            }
            
            a.Values = values;

            SignalOperation convolution = new Convolution();
            
            return convolution.Process(a, b).Values;
        }
    }
}