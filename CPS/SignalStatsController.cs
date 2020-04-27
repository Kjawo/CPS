using System;
using CPS.Signal;
using System.Collections.Generic;
using System.Linq;

namespace CPS
{
    public class SignalStatsController
    {
        public SignalStats Stats { get; } = new SignalStats();

        public void CalculateSignalsStats(DiscreteSignal signal, Params parameters)
        {
            if (signal != null)
            {
                List<double> samples = signal.Values.Select(tuple => tuple.Item2).ToList();
                double t1 = parameters.t1;
                double t2 = t1 + parameters.d;
                bool isDiscrete = false;
                Stats.AverageValue = StatsCalculator.AverageValue(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                Stats.AverageAbsValue = StatsCalculator.AbsAverageValue(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                Stats.RootMeanSquare = StatsCalculator.RootMeanSquare(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                Stats.Variance = StatsCalculator.Variance(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                Stats.AveragePower = StatsCalculator.AveragePower(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
            }
        }

        public void CalculateSignalConversionStats(List<Tuple<double, double>> originalValues, List<Tuple<double, double>> sampledSignalValues)
        {
            Stats.MeanSquaredError = StatsCalculator.MeanSquaredError(originalValues, sampledSignalValues).ToString("0." + new string('#', 339));
            Stats.SignalNoiseRatio = StatsCalculator.SignalNoiseRatio(originalValues, sampledSignalValues).ToString("0." + new string('#', 339));
            Stats.MaxDifference = StatsCalculator.MaxDifference(originalValues, sampledSignalValues).ToString("0." + new string('#', 339));
        }
    }
}
