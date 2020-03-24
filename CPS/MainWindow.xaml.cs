using System;
using CPS.Signal;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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

        private Params FirstSignalParams = new Params();
        private Params SecondSignalParams = new Params();
        private ChartWrapper ChartWrapper = new ChartWrapper();
        private HistogramWrapper HistogramWrapper = new HistogramWrapper();
        private DiscreteSignal Signal1;
        private DiscreteSignal Signal2;
        public double Frequency { get; set; } = 500;
        public bool SecondSignalEnabled { get; set; } = false;

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

        public OperationWrapper SelectedOperation { get; set; }
        public SignalWrapper SelectedSignalSecond { get; set; }
        public SignalWrapper SelectedSignalFirst { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Chart.DataContext = ChartWrapper;
            Histogram.DataContext = HistogramWrapper;
            HistogramGroupCount.DataContext = HistogramWrapper;
            SecondSignalEnabler.DataContext = this;
            FirstSignalParamGrid.DataContext = FirstSignalParams;
            SecondSignalParamGrid.DataContext = SecondSignalParams;
            SecondSignalParams.T = 0.2;
            SelectedOperation = OperationsList[0];
            SelectedSignalFirst = SignalList[0];
            SelectedSignalSecond = SignalList[1];
            Generate(null, null);
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            BaseSignal s1 = (BaseSignal) SelectedSignalFirst.Signal.Clone();
            BaseSignal s2 = (BaseSignal) SelectedSignalSecond.Signal.Clone(); 
            s1.SetParams(FirstSignalParams);
            s2.SetParams(SecondSignalParams);

            Signal1 = new DiscreteSignal(Frequency, s1);
            Signal2 = new DiscreteSignal(Frequency, s2);

            RebuildChart(null, null);
            RebuildHistogram(null, null);
        }

        private void Operation(object sender, RoutedEventArgs e)
        {
            if (Signal2 != null)
            {
                Signal1 = SelectedOperation.Operation.Process(Signal1, Signal2);
                RebuildChart(null, null);
                RebuildHistogram(null, null);
                SecondSignalEnabler.IsChecked = false;
            }
        }

        private void RebuildChart(object sender, RoutedEventArgs e)
        {
            ChartWrapper.Clear();
            ChartWrapper.AddSeries(Signal1);
            if (SecondSignalEnabled)
                ChartWrapper.AddSeries(Signal2);
        }
        private void RebuildHistogram(object sender, RoutedEventArgs e)
        {
            HistogramWrapper.Clear();
            HistogramWrapper.AddSeries(Signal1);
            if (SecondSignalEnabled)
                HistogramWrapper.AddSeries(Signal2);
        }

        private void SaveSignal(object sender, RoutedEventArgs e)
        {
            string source = ((Button)sender).Name;
            string path = Serializer.FilePath(false);
            BinaryWrapper binaryWrapper = new BinaryWrapper();
            if (source == "SaveFirstSignalButton")
            {
                binaryWrapper.SelectedSignal = SelectedSignalFirst;
                binaryWrapper.SignalParams = FirstSignalParams;
                binaryWrapper.DiscreteSignal = Signal1;
            }
            else if (source == "SaveSecondSignalButton")
            {
                binaryWrapper.SelectedSignal = SelectedSignalSecond;
                binaryWrapper.SignalParams = SecondSignalParams;
                binaryWrapper.DiscreteSignal = Signal2;
            }
            Serializer.SaveToBinaryFile(binaryWrapper, path);
        }

        private void LoadSignal(object sender, RoutedEventArgs e)
        {
            string source = ((Button)sender).Name;
            string path = Serializer.FilePath(true);
            BinaryWrapper binaryWrapper = new BinaryWrapper();
            binaryWrapper = Serializer.ReadFromBinaryFile(path);
            if (source == "LoadFirstSignalButton")
            {
                Signal1 = binaryWrapper.DiscreteSignal;
            }
            else if (source == "LoadSecondSignalButton")
            {
                Signal2 = binaryWrapper.DiscreteSignal;
                SecondSignalEnabler.IsChecked = true;
            }
            RebuildChart(null, null);
            RebuildHistogram(null, null);
        }

    }
}