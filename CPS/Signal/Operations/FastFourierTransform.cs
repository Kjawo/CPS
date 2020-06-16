using System;
using System.Collections.Generic;
using System.Numerics;

namespace CPS.Signal.Operations
{
    class FastFourierTransform
    {
        private static Complex J = Complex.ImaginaryOne;

        private static int BitReverse(int n, int bits)
        {
            int reversedN = n;
            int count = bits - 1;

            n >>= 1;
            while (n > 0)
            {
                reversedN = (reversedN << 1) | (n & 1);
                count--;
                n >>= 1;
            }

            return ((reversedN << count) & ((1 << bits) - 1));
        }

        private static void NormalizeRoundingError(Complex[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Complex(Math.Round(array[i].Real * 10000) / 10000, Math.Round(array[i].Imaginary * 10000) / 10000);
            }
        }

        public static List<Complex> Forward(List<Complex> input)
        {
            return Transform(input, -1);
        }

        public static List<Complex> Inverse(List<Complex> input)
        {
            var result = Transform(input, 1);
            for (int i = 0; i < input.Count; i++)
            {
                result[i] = result[i] / input.Count;
            }
            return result;
        }

        private static List<Complex> Transform(List<Complex> input, int direction)
        {
            var NTotal = (int) Math.Pow(2, Math.Floor(Math.Log(input.Count, 2)));
            var X = new Complex[NTotal];

            // Reorder array
            int bits = (int) Math.Log(NTotal, 2);
            for (int k = 0; k < NTotal; k++)
                X[BitReverse(k, bits)] = input[k];

            // Iteatively do butterfly operator
            for (int N = 2; N <= NTotal; N *= 2)
            {
                for (int subproblem = 0; subproblem < NTotal / N; subproblem++)
                {
                    for (int k = 0; k < N / 2; k++)
                    {
                        var a = k + subproblem * N;
                        var b = a + N / 2;
                        var tempXa = X[a];
                        var tempXb = X[b];
                        X[a] = tempXa + tempXb * Complex.Exp(direction * 2 * Math.PI * J * k / N);
                        X[b] = tempXa - tempXb * Complex.Exp(direction * 2 * Math.PI * J * k / N);
                    }
                }
                NormalizeRoundingError(X);
            }

            return new List<Complex>(X);
        }
    }
}
