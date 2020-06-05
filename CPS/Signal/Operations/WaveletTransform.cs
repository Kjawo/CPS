using System.Collections.Generic;
using System.Numerics;

namespace CPS.Signal.Operations
{
    public class WaveletTransform
    {
        
        private static List<double> H = new List<double>
        {
            0.47046721, 1.14111692, 0.650365, -0.19093442, -0.12083221, 0.0498175 
        };

        private static List<double> G = new List<double>
        {
            H[5],
            -H[4],
            H[3],
            -H[2],
            H[1],
            -H[0]
        };


        public static List<Complex> WaveletTransformation(DiscreteSignal signal)
        {
            List<Complex> result = new List<Complex>();
            
            var convolution = new Convolution();
            
            var discreteSignalH = new DiscreteSignal();
            var valuesH = new List<Value>();
            for (int i = 0; i < H.Count; i++)
            {
                valuesH.Add(new Value {Y = H[i]});
            }
            discreteSignalH.Values = valuesH;
            
            var discreteSignalG = new DiscreteSignal();
            var valuesG = new List<Value>();
            for (int i = 0; i < G.Count; i++)
            {
                valuesG.Add(new Value {Y = G[i]});
            }
            discreteSignalG.Values = valuesG;

            List<Value> hSamples = convolution.Process(signal, discreteSignalH).Values;
            List<Value> gSamples = convolution.Process(signal, discreteSignalG).Values;
            
            List<Complex> hHalf = new List<Complex>();
            List<Complex> gHalf = new List<Complex>();

            for (int i = 0; i < hSamples.Count; i++)
            {
                if (i % 2 == 0)
                    hHalf.Add(hSamples[i].Y);
            
                else
                    gHalf.Add(gSamples[i].Y);
            }

            for (int i = 0; i < gHalf.Count; i++)
                result.Add(new Complex(hHalf[i].Real, gHalf[i].Imaginary));

            return result;
        }

        public static DiscreteSignal WaveletBackwardTransformation(DiscreteSignal signal)
        {
            List<double> hRevesed = new List<double>(H);
            List<double> gReversed = new List<double>(G);

            hRevesed.Reverse();
            gReversed.Reverse();

            List<double> hSamples = new List<double>();
            List<double> gSamples = new List<double>();

            for (int i = 0; i < signal.Values.Count; i++)
            {
                hSamples.Add(signal.Values[i].Y.Real);
                hSamples.Add(0);

                gSamples.Add(0);
                gSamples.Add(signal.Values[i].Y.Imaginary);
            }
            
            
            
            var convolution = new Convolution();
            var addition = new Sum();
            
            var discreteSignalHSamples = new DiscreteSignal();
            var valuesHSamples = new List<Value>();
            for (int i = 0; i < hSamples.Count; i++)
            {
                valuesHSamples.Add(new Value {Y = hSamples[i]});
            }
            discreteSignalHSamples.Values = valuesHSamples;
            
            var discreteSignalHReversed = new DiscreteSignal();
            var valuesHReversed = new List<Value>();
            for (int i = 0; i < hRevesed.Count; i++)
            {
                valuesHReversed.Add(new Value {Y = hRevesed[i]});
            }
            discreteSignalHReversed.Values = valuesHReversed;
            
            var discreteSignalGSamples = new DiscreteSignal();
            var valuesGSamples = new List<Value>();
            for (int i = 0; i < gSamples.Count; i++)
            {
                valuesGSamples.Add(new Value {Y = gSamples[i]});
            }
            discreteSignalGSamples.Values = valuesGSamples;
            
            var discreteSignalGReversed = new DiscreteSignal();
            var valuesGReversed = new List<Value>();
            for (int i = 0; i < gReversed.Count; i++)
            {
                valuesGReversed.Add(new Value {Y = gReversed[i]});
            }
            discreteSignalGReversed.Values = valuesGReversed;
            


            DiscreteSignal hResult = convolution.Process(discreteSignalHSamples, discreteSignalHReversed);
            DiscreteSignal gResult = convolution.Process(discreteSignalGSamples, discreteSignalGReversed);

            return addition.Process(hResult, gResult);
        }

    }
}