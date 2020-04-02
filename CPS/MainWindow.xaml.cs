using System;
using CPS.Signal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CPS.Annotations;
using CPS.Signal.Operations;

namespace CPS
{
    [Serializable]
    public class OperationWrapper
    {
        public string Name { get; set; }
        public SignalOperation Operation { get; set; }
    }

    [Serializable]
    public class SignalWrapper
    {
        public string Name { get; set; }
        public BaseSignal Signal { get; set; }
    }

    public partial class MainWindow : Window
    {
        public static Random Random = new Random();

        private ChartWrapper ChartWrapper = new ChartWrapper();
        private HistogramWrapper HistogramWrapper = new HistogramWrapper();
        private DiscreteSignal[] Signals = { null, null, null };
        private Params[] SignalParams = { new Params(), new Params(), new Params() };
        private SignalStats[] SignalStats = { new SignalStats(), new SignalStats(), new SignalStats() };
        public double Frequency { get; set; } = 200;

        public List<OperationWrapper> OperationsList { get; } = new List<OperationWrapper>
        {
            new OperationWrapper {Name = "Suma", Operation = new SumSignalOperation()},
            new OperationWrapper {Name = "Różnica", Operation = new DiffSignalOperation()},
            new OperationWrapper {Name = "Iloczyn", Operation = new MulSignalOperation()},
            new OperationWrapper {Name = "Iloraz", Operation = new DivSignalOperation()},
        };

        public List<SignalWrapper> SignalList { get; } = new List<SignalWrapper>
        {
            new SignalWrapper() {Name = "Sygnał sinusoidalny", Signal = new SinusoidalSignal()},
            new SignalWrapper() {Name = "Szum gaussowski", Signal = new GaussianNoise()},
            new SignalWrapper() {Name = "Szum o rozkładzie jednostajnym", Signal = new UniformDistributionNoise()},
            new SignalWrapper()
                {Name = "Sygnał sinusoidalny \nwyprostowany jednopołówkowo", Signal = new SineWaveHalfRectified()},
            new SignalWrapper()
                {Name = "Sygnał sinusoidalny \nwyprostowany dwupołówkowo", Signal = new SineWaveFullRectified()},
            new SignalWrapper() {Name = "Sygnał prostokątny", Signal = new SquareWaveSignal()},
            new SignalWrapper() {Name = "Sygnał prostokątny symetryczny", Signal = new SquareWaveSymetricalSignal()},
            new SignalWrapper() {Name = "Sygnał trójkątny", Signal = new TriangleWave()},
            new SignalWrapper() {Name = "Skok jednostkowy", Signal = new UnitStep()},
            new SignalWrapper() {Name = "Impuls jednostkowy", Signal = new UnitImpulse()},
            new SignalWrapper() {Name = "Szum impulsowy", Signal = new ImpulseNoise()},
        };

        public List<int> SlotsList { get; set; } = new List<int> { 1, 2, 3 };

        public OperationWrapper SelectedOperation { get; set; }
        public SignalWrapper[] SelectedSignal { get; set; } = { null, null, null };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Chart.DataContext = ChartWrapper;
            Histogram.DataContext = HistogramWrapper;
            HistogramGroupCount.DataContext = HistogramWrapper;
            FirstSignalParamGrid.DataContext = SignalParams[0];
            SecondSignalParamGrid.DataContext = SignalParams[1];
            FirstSignalStats.DataContext = SignalStats[0];
            SecondSignalStats.DataContext = SignalStats[1];
            SelectedOperation = OperationsList[0];
            SelectedSignal[0] = SignalList[0];
            SelectedSignal[1] = SignalList[1];
        }

        public void CalculateSignalsStats()
        {
            for (int i = 0; i < Signals.Length; i++)
            {
                if (Signals[i] != null)
                {
                    List<double> samples = new List<double>();
                    foreach (var value in Signals[i].GetValues())
                    {
                        samples.Add(value.Item2);
                    }

                    double t1 = SignalParams[i].t1;
                    double t2 = t1 + SignalParams[i].d;
                    bool isDiscrete = false;
                    SignalStats[i].AverageValue = Stats.AverageValue(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                    SignalStats[i].AverageAbsValue = Stats.AbsAverageValue(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                    SignalStats[i].RootMeanSquare = Stats.RootMeanSquare(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                    SignalStats[i].Variance = Stats.Variance(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                    SignalStats[i].AveragePower = Stats.AveragePower(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                }
            }
        } 

        private void Generate(object sender, RoutedEventArgs e)
        {
            string source = ((Button)sender).Name;
            int i = 0;
            if (source == "GenerateFirstSignal")
                i = 0;
            else if (source == "GenerateSecondSignal")
                i = 1;
            BaseSignal s = (BaseSignal) SelectedSignal[i].Signal.Clone();
            s.SetParams(SignalParams[i]);
            Signals[i] = s.ToDiscrete(Frequency);
            ChartWrapper.SetSignal(i, Signals[i]);
            ChartWrapper.Replot();
            HistogramWrapper.SetSignal(i, Signals[i]);
            HistogramWrapper.Replot();
            CalculateSignalsStats();
        }

        private void ClearSignal(int i)
        {
            Signals[i] = null;
            ChartWrapper.SetSignal(i, null);
            HistogramWrapper.SetSignal(i, null);
            ChartWrapper.Replot();
            HistogramWrapper.Replot();
        }

        private void ClearOperationResult(object sender, RoutedEventArgs e)
        {
            ClearSignal(2);
        }

        private void Operation(object sender, RoutedEventArgs e)
        {
            if (Signals[0] != null && Signals[1] != null)
            {
                int i = ((int)OperationSlot.SelectedItem) - 1;
                Signals[i] = SelectedOperation.Operation.Process(Signals[0], Signals[1]);
                ChartWrapper.SetSignal(i, Signals[i]);
                ChartWrapper.Replot();
                HistogramWrapper.SetSignal(i, Signals[i]);
                HistogramWrapper.Replot();
                CalculateSignalsStats();
            }
        }

        private void SaveSignal(object sender, RoutedEventArgs e)
        {
            string source = ((Button)sender).Name;
            string path = Serializer.FilePath(false);
            if (string.IsNullOrEmpty(path))
                return;

            int i = 0;
            BinaryWrapper binaryWrapper = new BinaryWrapper();
            if (source == "SaveFirstSignalButton")
                i = 0;
            else if (source == "SaveSecondSignalButton")
                i = 1;
            else if (source == "SaveOperationResult")
                i = 2;
            binaryWrapper.SelectedSignal = SelectedSignal[i];
            binaryWrapper.SignalParams = SignalParams[i];
            binaryWrapper.DiscreteSignal = Signals[i];
            Serializer.SaveToBinaryFile(binaryWrapper, path);
        }

        private void LoadSignal(object sender, RoutedEventArgs e)
        {
            string source = ((Button)sender).Name;
            string path = Serializer.FilePath(true);
            if (string.IsNullOrEmpty(path))
                return;

            int i = 0;
            BinaryWrapper binaryWrapper = new BinaryWrapper();
            binaryWrapper = Serializer.ReadFromBinaryFile(path);
            if (source == "LoadFirstSignalButton")
                i = 0;
            else if (source == "LoadSecondSignalButton")
                i = 1;
            Signals[i] = binaryWrapper.DiscreteSignal;
            ChartWrapper.SetSignal(i, Signals[i]);
            ChartWrapper.Replot();
            HistogramWrapper.SetSignal(i, Signals[i]);
            HistogramWrapper.Replot();
        }

        private void RebuildHistogram(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Signals.Length; i++)
            {
                HistogramWrapper.SetSignal(i, Signals[i]);
            }
            HistogramWrapper.Replot();
        }

        private void ClearSignalOnClick(object sender, RoutedEventArgs e)
        {
            string source = ((Button)sender).Name;
            int i = 0;
            if (source == "ClearFirstSignal")
                i = 0;
            else if (source == "ClearSecondSignal")
                i = 1;
            ClearSignal(i);
        }

    }
}