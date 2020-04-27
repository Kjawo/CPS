using System;
using System.Collections.Generic;

namespace CPS.Signal
{
    public class StatsCalculator
    {
        public static double AverageValue(List<double> samples, double t1 = 0, double t2 = 0, bool isDiscrete = true)
        {
            double avg;

            if (isDiscrete)
            {
                avg = Sum(samples) / samples.Count;
            }
            else
            {
                avg = Integral(Math.Abs((t2 - t1) / samples.Count), samples) / (t2 - t1);
            }

            return avg;
        }

        public static double AbsAverageValue(List<double> samples, double t1 = 0, double t2 = 0, bool isDiscrete = true)
        {
            double avg;

            if (isDiscrete)
            {
                avg = Sum(samples, Math.Abs) / samples.Count;
            }
            else
            {
                avg = Integral(Math.Abs((t2 - t1) / samples.Count), samples, Math.Abs) / (t2 - t1);
            }

            return avg;
        }

        public static double RootMeanSquare(List<double> samples, double t1 = 0, double t2 = 0, bool isDiscrete = true)
        {
            double meanSquareRoot;

            if (isDiscrete)
            {
                meanSquareRoot = Math.Sqrt(AveragePower(samples, isDiscrete: true));
            }
            else
            {
                meanSquareRoot = Math.Sqrt(AveragePower(samples, t1, t2));
            }

            return meanSquareRoot;
        }

        public static double Variance(List<double> samples, double t1 = 0, double t2 = 0, bool isDiscrete = true)
        {
            double result;

            if (isDiscrete)
            {
                result = Sum(samples, d => Math.Pow(d - AverageValue(samples, isDiscrete: true), 2)) / samples.Count;
            }
            else
            {
                result = Integral(Math.Abs((t2 - t1) / samples.Count), samples,
                    d => Math.Pow(d - AverageValue(samples, t1, t2), 2)) / (t2 - t1);
            }

            return result;
        }


        public static double AveragePower(List<double> samples, double t1 = 0, double t2 = 0, bool isDiscrete = true)
        {
            double result;

            if (isDiscrete)
            {
                result = Sum(samples, d => d * d) / samples.Count;
            }
            else
            {
                result = Integral(Math.Abs((t2 - t1) / samples.Count), samples, d => d * d) / (t2 - t1);
            }

            return result;
        }

        private static double Integral(double dx, List<double> samples, Func<double, double> additionalFunc = null)
        {
            double integral = 0;
            foreach (var sample in samples)
            {
                if (additionalFunc != null)
                    integral += additionalFunc(sample);
                else
                    integral += sample;
            }

            integral *= dx;

            return integral;
        }

        private static double Sum(List<double> samples, Func<double, double> additionalFunc = null)
        {
            double sum = 0;
            foreach (var sample in samples)
            {
                if (additionalFunc != null)
                    sum += additionalFunc(sample);
                else
                    sum += sample;
            }

            return sum;
        }


        //Conversion statistics
        public static double MeanSquaredError(List<Tuple<double, double>> originalSignalValues,
            List<Tuple<double, double>> sampledSignalValues)
        {
            int N = sampledSignalValues.Count;
            double sum = 0;

            for (int i = 0; i < N; i++)
            {
                sum += Math.Pow((originalSignalValues[i].Item2 - sampledSignalValues[i].Item2), 2);
            }

            return sum / N;
        }

        public static double SignalNoiseRatio(List<Tuple<double, double>> originalSignalValues,
            List<Tuple<double, double>> sampledSignalValues)
        {
            double numerator = 0;
            double denominator = 0;
            int N = sampledSignalValues.Count;

            for (int i = 0; i < N; i++)
            {
                numerator += Math.Pow(originalSignalValues[i].Item2, 2);
            }

            for (int i = 0; i < N; i++)
            {
                denominator += Math.Pow(originalSignalValues[i].Item2 - sampledSignalValues[i].Item2, 2);
            }

            return 10 * Math.Log10(numerator / denominator);
        }

        public static double MaxDifference(List<Tuple<double, double>> originalSignalValues,
            List<Tuple<double, double>> sampledSignalValues)
        {
            double maxDifference = 0.0d;
            double difference;
            int N = sampledSignalValues.Count;

            for(int i = 0; i < N; i++)
            {
                difference = Math.Abs(originalSignalValues[i].Item2 - sampledSignalValues[i].Item2);
                if (difference > maxDifference)
                    maxDifference = difference;
            }

            return maxDifference;
        }
    }
}