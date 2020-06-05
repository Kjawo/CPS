using CPS.Signal;
using CPS.Signal.Converters;
using CPS.Signal.Signals;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CPS.Signal.Operations;
using System.Numerics;

namespace CPS
{
    [Serializable]
    public class SignalWrapper
    {
        public string Name { get; set; }
        public BaseSignal Signal { get; set; }
    }

    public class ConverterWrapper
    {
        public string Name { get; set; }
        public DigitalToAnalogConverter Converter { get; set; }
    }

    public partial class SignalControls : UserControl
    {
        public static List<SignalWrapper> SignalList { get; } = new List<SignalWrapper>
        {
            new SignalWrapper {Name = "Sygnał sinusoidalny", Signal = new SinusoidalSignal()},
            new SignalWrapper {Name = "Szum gaussowski", Signal = new GaussianNoise()},
            new SignalWrapper {Name = "Szum o rozkładzie jednostajnym", Signal = new UniformDistributionNoise()},
            new SignalWrapper
                {Name = "Sygnał sinusoidalny \nwyprostowany jednopołówkowo", Signal = new SineWaveHalfRectified()},
            new SignalWrapper
                {Name = "Sygnał sinusoidalny \nwyprostowany dwupołówkowo", Signal = new SineWaveFullRectified()},
            new SignalWrapper {Name = "Sygnał prostokątny", Signal = new SquareWaveSignal()},
            new SignalWrapper {Name = "Sygnał prostokątny symetryczny", Signal = new SquareWaveSymetricalSignal()},
            new SignalWrapper {Name = "Sygnał trójkątny", Signal = new TriangleWave()},
            new SignalWrapper {Name = "Skok jednostkowy", Signal = new UnitStep()},
            new SignalWrapper {Name = "Impuls jednostkowy", Signal = new UnitImpulse()},
            new SignalWrapper {Name = "Szum impulsowy", Signal = new ImpulseNoise()},
        };

        public static List<ConverterWrapper> ConvertersList { get; } = new List<ConverterWrapper>
        {
            new ConverterWrapper {Name = "Ekstrapolacja zerowego rzędu", Converter = new ZeroOrderHoldConverter()},
            new ConverterWrapper {Name = "Interpolacja pierwszego rzędu", Converter = new FirstOrderHoldConverter()},
            new ConverterWrapper {Name = "Rekonstrukcja sinc", Converter = new SincConverter()},
        };

        public string Title { get; set; } = "";
        public int SignalSlot { get; set; } = 0;
        public double Frequency { get; set; } = 200;
        public double SamplingFreq { get; set; } = 10;
        public double QuantizationStep { get; set; } = 0.01;
        public ChartWrapper ChartWrapper { get; set; } = null;
        public HistogramWrapper HistogramWrapper { get; set; } = null;
        public SignalWrapper SelectedSignal { get; set; } = null;
        public ConverterWrapper SelectedConverter { get; set; } = null;
        public DiscreteSignal Signal { get; set; } = null;
        public DiscreteSignal OriginalSignal { get; set; } = null;
        public Params Params { get; set; } = new Params();
        public int K { get; set; } = 8;
        public int M { get; set; } = 63;
        public SignalStatsController StatsController { get; } = new SignalStatsController();

        public SignalControls()
        {
            InitializeComponent();
            this.DataContext = this;
            ParamsGrid.DataContext = Params;
            SelectedSignal = SignalList[0];
            SelectedConverter = ConvertersList[0];
        }

        public void Generate(object sender, RoutedEventArgs e)
        {
            BaseSignal s = (BaseSignal) SelectedSignal.Signal.Clone();
            s.Params = Params;
            Signal = s.ToDiscrete(Frequency);
            OriginalSignal = s.ToDiscrete(Frequency);
            StatsController.CalculateSignalsStats(Signal, Params);
            ReplotChartAndHistogram();
        }

        public void ClearSignal(object sender, RoutedEventArgs e)
        {
            Signal = null;
            ReplotChartAndHistogram();
        }

        public void SaveSignal(object sender, RoutedEventArgs e)
        {
            string path = Serializer.FilePath(false);
            if (string.IsNullOrEmpty(path)) return;
            BinaryWrapper binaryWrapper = new BinaryWrapper
            {
                SelectedSignal = SelectedSignal,
                SignalParams = Params,
                DiscreteSignal = Signal
            };
            Serializer.SaveToBinaryFile(binaryWrapper, path);
        }

        public void LoadSignal(object sender, RoutedEventArgs e)
        {
            string path = Serializer.FilePath(true);
            if (string.IsNullOrEmpty(path)) return;
            BinaryWrapper binaryWrapper = Serializer.ReadFromBinaryFile(path);
            Signal = binaryWrapper.DiscreteSignal;
            ReplotChartAndHistogram();
        }

        private void Digitalize(object sender, RoutedEventArgs e)
        {
            Signal = new DigitalizedSignal(Signal, QuantizationStep, SamplingFreq);
            ReplotChartAndHistogram();
        }

        private void Analogize(object sender, RoutedEventArgs e)
        {
            DigitalizedSignal digitalSignal = (DigitalizedSignal)Signal;
            Signal = SelectedConverter.Converter.Convert(digitalSignal, Frequency);
            StatsController.CalculateSignalConversionStats(OriginalSignal.Values, Signal.Values);
            ReplotChartAndHistogram();
        }


        private void GenerateImpulseResponseLowpass(object sender, RoutedEventArgs e)
        {
            Signal = new LowPassImpulseResponse(K, M);
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ReplotChartAndHistogram();
        }

        private void GenerateImpulseResponseBandpass(object sender, RoutedEventArgs e)
        {
            Signal = new BandPassImpulseResponse(K, M);
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ReplotChartAndHistogram();
        }

        private void ComputeHammingWindow(object sender, RoutedEventArgs e)
        {
            Signal = new  HammingWindow(Signal, M);
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ReplotChartAndHistogram();
        }

        private void ReplotChartAndHistogram()
        {
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ChartWrapper.Replot();
            HistogramWrapper.SetSignal(SignalSlot, Signal);
            HistogramWrapper.Replot();
        }


        
        private void FFT(object sender, RoutedEventArgs e)
        {
            var input = Signal.Values.Select(value => value.Y).ToList();
            var fftOutput = FastFourierTransform.Transform(input, 1);
            var output = new List<Value>();
            for (int i = 0; i < fftOutput.Count(); i++)
            {
                var value = new Value
                {
                    X = new Complex(i, 0),
                    Y = new Complex(fftOutput[i].Real, 0)
                };
                output.Add(value);
            }
            Signal = DiscreteSignal.ForParameters("fft", CPS.Signal.SignalType.CONTINUOUS, 1, output);
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ChartWrapper.Replot();
        }
        
        private void DB6(object sender, RoutedEventArgs e)
        {
            var waveletTransformationOutput = WaveletTransform.WaveletTransformation(Signal);
            var output = new List<Value>();
            for (int i = 0; i < waveletTransformationOutput.Count(); i++)
            {
                var value = new Value
                {
                    X = new Complex(i, 0),
                    Y = new Complex(waveletTransformationOutput[i].Real, 0)
                };
                output.Add(value);
            }
            Signal = DiscreteSignal.ForParameters("DB6", CPS.Signal.SignalType.CONTINUOUS, 1, output);
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ChartWrapper.Replot();
        }
        
        private void DB6reverse(object sender, RoutedEventArgs e)
        {
            Signal = WaveletTransform.WaveletBackwardTransformation(Signal);
            ChartWrapper.SetSignal(SignalSlot, Signal);
            ChartWrapper.Replot();
        }
    }
}
