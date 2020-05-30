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
            int n = 1;
            while (n << 1 <= signal.Count())
                n = n << 1;

            List<Complex> b, c;
            var s = new List<Complex>();
            var t = new List<Complex>();

            var a = signal.Take(n).ToList();
            var x = new List<Complex>();

            if (n == 1)
            {
                x.Add(a[0]);
                return x;
            }

            int nh = n / 2;
            
            for (int k = 0; k < nh; k++)
            {
                s.Add(a[k]);
                t.Add(a[k + nh]);
            }

            for (int k = 0; k < nh; k++)
            {
                var tempS = s[k];
                var tempT = t[k];
                s[k] = tempS + tempT;
                t[k] = tempS - tempT;
            }

            double v = Is * 0.5;
            for (int k = 0; k < nh; k++)
            {
                t[k] = t[k] * Complex.Exp(v * 2.0 * Math.PI * Complex.ImaginaryOne * k / nh);
            }

            b = FastFourierTransform.Transform(s, Is);
            c = FastFourierTransform.Transform(t, Is);

            for (int k = 0; k < nh; k++)
            {
                x.Add(b[k]);
                x.Add(c[k]);
            }

            return x;
        }
    }
}
