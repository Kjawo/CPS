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

    public partial class MainWindow : Window, INotifyPropertyChanged
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

        public void CalculateSignalsStats()
        {
            List<double> samples = new List<double>();
            foreach (var value in Signal1.GetValues())
            {
                samples.Add(value.Item2);
            }

            double t1 = FirstSignalParams.t1;
            double t2 = t1 + FirstSignalParams.d;

            bool isDiscrete = false;
            AverageValue1 = Stats.AverageValue(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
            AverageAbsValue1 = Stats.AbsAverageValue(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
            RootMeanSquare1 = Stats.RootMeanSquare(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
            Variance1 = Stats.Variance(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
            AveragePower1 = Stats.AveragePower(samples, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
            
            OnPropertyChanged(nameof(AverageValue1));
            OnPropertyChanged(nameof(AverageAbsValue1));
            OnPropertyChanged(nameof(RootMeanSquare1));
            OnPropertyChanged(nameof(Variance1));
            OnPropertyChanged(nameof(AveragePower1));
            
            if (SecondSignalEnabler.IsChecked == true)
            {
                List<double> samples2 = new List<double>();
                foreach (var value in Signal2.GetValues())
                {
                    samples2.Add(value.Item2);
                }
            
                t1 = SecondSignalParams.t1;
                t2 = t1 + SecondSignalParams.d;
                
                AverageValue2 = Stats.AverageValue(samples2, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                AverageAbsValue2 = Stats.AbsAverageValue(samples2, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                RootMeanSquare2 = Stats.RootMeanSquare(samples2, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                Variance2 = Stats.Variance(samples2, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                AveragePower2 = Stats.AveragePower(samples2, t1, t2, isDiscrete).ToString("0." + new string('#', 339));
                
                OnPropertyChanged(nameof(AverageValue2));
                OnPropertyChanged(nameof(AverageAbsValue2));
                OnPropertyChanged(nameof(RootMeanSquare2));
                OnPropertyChanged(nameof(Variance2));
                OnPropertyChanged(nameof(AveragePower2));
            }
        }

        public string AveragePower2 { get; set; }

        public string Variance2 { get; set; }

        public string RootMeanSquare2 { get; set; }

        public string AverageAbsValue2 { get; set; }

        public string AverageValue2 { get; set; }

        public string AveragePower1 { get; set; }
        public string Variance1 { get; set; }
        public string RootMeanSquare1 { get; set; }
        public string AverageAbsValue1 { get; set; }
        public string AverageValue1 { get; set; }


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
                SecondSignalEnabler.IsChecked = false;
                RebuildChart(null, null);
                RebuildHistogram(null, null);
            }
        }

        private void RebuildChart(object sender, RoutedEventArgs e)
        {
            ChartWrapper.Clear();
            ChartWrapper.AddSeries(Signal1);
            if (SecondSignalEnabled)
                ChartWrapper.AddSeries(Signal2);
            
            CalculateSignalsStats();
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}