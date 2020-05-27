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
                List<double> samples = signal.Values.Select(tuple => tuple.Y.Real).ToList();
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

        public void CalculateSignalConversionStats(List<Value> originalValues, List<Value> sampledSignalValues)
        {
            var originalValuesReal = originalValues.Select(v => Tuple.Create(v.X.Real, v.Y.Real)).ToList();
            var sampledSignalValuesReal = sampledSignalValues.Select(v => Tuple.Create(v.X.Real, v.Y.Real)).ToList();
            Stats.MeanSquaredError = StatsCalculator.MeanSquaredError(originalValuesReal, sampledSignalValuesReal).ToString("0." + new string('#', 339));
            Stats.SignalNoiseRatio = StatsCalculator.SignalNoiseRatio(originalValuesReal, sampledSignalValuesReal).ToString("0." + new string('#', 339));
            Stats.MaxDifference = StatsCalculator.MaxDifference(originalValuesReal, sampledSignalValuesReal).ToString("0." + new string('#', 339));
        }
    }
}
