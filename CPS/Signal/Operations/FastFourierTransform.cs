using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPS.Signal.Operations
{
    class FastFourierTransform
    {
        public static List<Complex> Transform(List<Complex> signal, int Is)
        {
            var result = new List<Complex>();
            var N = signal.Count;
            for (int k = 0; k < N; k++)
            {
                Complex Xk = 0;
                for (int n = 0; n < N; n++)
                {
                    Xk += signal[n] * Complex.Exp(-1 * (Complex.ImaginaryOne * 2 * Math.PI / N) * k * n);
                }
                result.Add(Xk);
            }
            return result;
        }
    }
}
