using System;
using System.Collections;
using CPS.Signal;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using static CPS.Generator;

namespace CPS
{
    [Serializable]
    public class ModeWrapper
    {
        public string Name { get; set; }
        public Mode Mode { get; set; }
    }
    [Serializable]
    public class SignalWrapper
    {
        public string Name { get; set; }
        public SignalEnum Signal { get; set; }
    }

    public partial class MainWindow : Window
    {
        public static Random Random = new Random();

        private Params FirstSignalParams = new Params();
        private Params SecondSignalParams = new Params();
        private ChartWrapper ChartWrapper = new ChartWrapper();
        private HistogramWrapper HistogramWrapper = new HistogramWrapper();
        private DiscreteSignal ds1;
        private DiscreteSignal ds2;
        public double Frequency { get; set; } = 16;
        public int HistogramGroupsCount { get; set; } = 50;
        public bool SecondSignalEnabled { get; set; } = false;

        public List<ModeWrapper> ModeList { get; } = new List<ModeWrapper>
        {
            new ModeWrapper {Name = "Domyślny", Mode = Mode.DEFAULT},
            new ModeWrapper {Name = "Suma", Mode = Mode.SUM},
            new ModeWrapper {Name = "Różnica", Mode = Mode.DIFF},
            new ModeWrapper {Name = "Iloczyn", Mode = Mode.MUL},
            new ModeWrapper {Name = "Iloraz", Mode = Mode.DIV},
        };

        public List<SignalWrapper> SignalList { get; } = new List<SignalWrapper>
        {
            new SignalWrapper() {Name = "Sygnał sinusoidalny", Signal = SignalEnum.sin},
            new SignalWrapper() {Name = "Szum gaussowski", Signal = SignalEnum.gauss},
            new SignalWrapper() {Name = "Szum o rozkładzie jednostajnym", Signal = SignalEnum.uniformD},
            new SignalWrapper()
                {Name = "Sygnał sinusoidalny \nwyprostowany jednopołówkowo", Signal = SignalEnum.sinHalfRectified},
            new SignalWrapper()
                {Name = "Sygnał sinusoidalny \nwyprostowany dwupołówkowo", Signal = SignalEnum.sinFullRectified},
            new SignalWrapper() {Name = "Sygnał prostokątny", Signal = SignalEnum.squareWave},
            new SignalWrapper() {Name = "Sygnał prostokątny symetryczny", Signal = SignalEnum.squareWaveSymetrical},
            new SignalWrapper() {Name = "Sygnał trójkątny", Signal = SignalEnum.triangleWave},
            new SignalWrapper() {Name = "Skok jednostkowy", Signal = SignalEnum.unitStep},
            new SignalWrapper() {Name = "Impuls jednostkowy", Signal = SignalEnum.unitImpulse},
            new SignalWrapper() {Name = "Szum impulsowy", Signal = SignalEnum.impulseNoise},
        };

        public ModeWrapper SelectedMode { get; set; }
        public SignalWrapper SelectedSignalSecond { get; set; }
        public SignalWrapper SelectedSignalFirst { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Chart.DataContext = ChartWrapper;
            Histogram.DataContext = HistogramWrapper;
            SecondSignalEnabler.DataContext = this;
            FirstSignalParamGrid.DataContext = FirstSignalParams;
            SecondSignalParamGrid.DataContext = SecondSignalParams;
            SecondSignalParams.T = 0.2;
            SelectedMode = ModeList[0];


            SelectedSignalFirst = SignalList[1];
            SelectedSignalSecond = SignalList[0];

            GenerateFromParameters(null, null);
        }

        public void SaveFirst(object sender, RoutedEventArgs e)
        {
            string path = Serializer.FilePath(false);

            BinaryWrapper binaryWrapper = new BinaryWrapper();
            binaryWrapper.SelectedSignal = SelectedSignalFirst;
            binaryWrapper.SignalParams = FirstSignalParams;
            binaryWrapper.DiscreteSignal = ds1;

            Serializer.SaveToBinaryFile(binaryWrapper, path);
        }

        public void LoadFirst(object sender, RoutedEventArgs e)
        {
            string path = Serializer.FilePath(true);
            
            BinaryWrapper binaryWrapper = new BinaryWrapper();
            binaryWrapper = Serializer.ReadFromBinaryFile(path);

            FirstSignalParams = binaryWrapper.SignalParams;
            SelectedSignalFirst = binaryWrapper.SelectedSignal;
            ds1 = binaryWrapper.DiscreteSignal;

            FirstSignalParamGrid.DataContext = FirstSignalParams;
            FirstSignalParamGrid.DataContext = SelectedSignalFirst;
        }

        public void SaveSecond(object sender, RoutedEventArgs e)
        {
            string path = Serializer.FilePath(false);

            BinaryWrapper binaryWrapper = new BinaryWrapper();
            binaryWrapper.SelectedSignal = SelectedSignalFirst;
            binaryWrapper.SignalParams = FirstSignalParams;
            binaryWrapper.DiscreteSignal = ds2;


            Serializer.SaveToBinaryFile(binaryWrapper, path);
        }

        public void LoadSecond(object sender, RoutedEventArgs e)
        {
            string path = Serializer.FilePath(true);
            
            BinaryWrapper binaryWrapper = new BinaryWrapper();
            binaryWrapper = Serializer.ReadFromBinaryFile(path);

            SecondSignalParams = binaryWrapper.SignalParams;
            SelectedSignalSecond = binaryWrapper.SelectedSignal;
            ds2 = binaryWrapper.DiscreteSignal;

            SecondSignalParamGrid.DataContext = SecondSignalParams;
            SecondSignalParamGrid.DataContext = SelectedSignalSecond;
        }

        public void GenerateFromParameters(object sender, RoutedEventArgs e)
        {
            ChartWrapper.Clear();
            HistogramWrapper.Clear();
            HistogramWrapper.HistogramGroupsCount = HistogramGroupsCount;

            BaseSignal s1 = GetSelectedSignal(SelectedSignalFirst);
            BaseSignal s2 = GetSelectedSignal(SelectedSignalSecond);
            s1.SetParams(FirstSignalParams);
            s2.SetParams(SecondSignalParams);

            ds1 = new Generator()
                .withFrequency(Frequency)
                .withSignal(s1)
                .withSecondarySignal(s2)
                .withMode(SelectedMode.Mode)
                .build();
            ChartWrapper.AddSeries(ds1);
            HistogramWrapper.AddSeries(ds1);

            if (SecondSignalEnabled && SelectedMode.Mode == Mode.DEFAULT)
            {
                ds2 = new Generator()
                    .withFrequency(Frequency)
                    .withSignal(s2)
                    .withMode(SelectedMode.Mode)
                    .build();
                ChartWrapper.AddSeries(ds2);
                HistogramWrapper.AddSeries(ds2);
            }
        }
        
        public void GenerateFromDiscrete(object sender, RoutedEventArgs e)
        {
            ChartWrapper.Clear();
            HistogramWrapper.Clear();
            HistogramWrapper.HistogramGroupsCount = HistogramGroupsCount;

            ChartWrapper.AddSeries(ds1);
            HistogramWrapper.AddSeries(ds1);

            if (SecondSignalEnabled && SelectedMode.Mode == Mode.DEFAULT && ds2 != null)
            {
                BaseSignal s2 = GetSelectedSignal(SelectedSignalSecond);
                s2.SetParams(SecondSignalParams);
                ds2 = new Generator()
                    .withFrequency(Frequency)
                    .withSignal(s2)
                    .withMode(SelectedMode.Mode)
                    .build();
                ChartWrapper.AddSeries(ds2);
                HistogramWrapper.AddSeries(ds2);
            }
        }
    }
}