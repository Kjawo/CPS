﻿using System;
using CPS.Signal;
using System.Collections.Generic;
using System.Windows;
using CPS.Signal.Operations;

namespace CPS
{
    [Serializable]
    public class OperationWrapper
    {
        public string Name { get; set; }
        public SignalOperation Operation { get; set; }
    }

    public partial class MainWindow : Window
    {
        public static Random Random = new Random();

        public List<OperationWrapper> OperationsList { get; } = new List<OperationWrapper>
        {
            new OperationWrapper {Name = "Suma", Operation = new Sum()},
            new OperationWrapper {Name = "Różnica", Operation = new Difference()},
            new OperationWrapper {Name = "Iloczyn", Operation = new Multiplication()},
            new OperationWrapper {Name = "Iloraz", Operation = new Division()},
            new OperationWrapper {Name = "Splot", Operation = new Convolution()},
            new OperationWrapper {Name = "Korelacja przez splot", Operation = new IndirectCorrelation()},
            new OperationWrapper {Name = "Korelacja bezpośrednia", Operation = new DirectCorrelation()},
        };

        private ChartWrapper ChartWrapper = new ChartWrapper();
        private HistogramWrapper HistogramWrapper = new HistogramWrapper();
        public List<int> SlotsList { get; set; } = new List<int> { 1, 2, 3 };
        private SignalStats[] SignalStats = { new SignalStats(), new SignalStats(), new SignalStats() };
        public OperationWrapper SelectedOperation { get; set; }
        private SignalControls operationResultSignalControls = new SignalControls(); 

        double frequency = 16;
        public double Frequency
        {
            get => frequency;
            set {
                frequency = value;
                FirstSignalControls.Frequency = value;
                SecondSignalControls.Frequency = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Chart.DataContext = ChartWrapper;
            ChartImaginary.DataContext = ChartWrapper;
            FirstSignalControls.ChartWrapper = ChartWrapper;
            SecondSignalControls.ChartWrapper = ChartWrapper;
            operationResultSignalControls.ChartWrapper = ChartWrapper;

            Histogram.DataContext = HistogramWrapper;
            HistogramGroupCount.DataContext = HistogramWrapper;
            FirstSignalControls.HistogramWrapper = HistogramWrapper;
            SecondSignalControls.HistogramWrapper = HistogramWrapper;
            operationResultSignalControls.HistogramWrapper = HistogramWrapper;

            FirstSignalStats.DataContext = FirstSignalControls.StatsController.Stats;
            SecondSignalStats.DataContext = SecondSignalControls.StatsController.Stats;

            FirstSignalControls.SignalSlot = 0;
            SecondSignalControls.SignalSlot = 1;
            operationResultSignalControls.SignalSlot = 2;
            SelectedOperation = OperationsList[0];
        }

        private void ClearOperationResult(object sender, RoutedEventArgs e)
        {
            operationResultSignalControls.ClearSignal(null, null);
        }

        private void SaveOperationResult(object sender, RoutedEventArgs e)
        {
            operationResultSignalControls.SaveSignal(null, null);
        }

        private void Operation(object sender, RoutedEventArgs e)
        {
            DiscreteSignal first = FirstSignalControls.Signal;
            DiscreteSignal second = SecondSignalControls.Signal;
            if (first != null && second != null)
            {
                int i = ((int)OperationSlot.SelectedItem) - 1;
                DiscreteSignal result = SelectedOperation.Operation.Process(first, second);
                switch (i)
                {
                    case 0: FirstSignalControls.Signal = result;
                        break;
                    case 1: SecondSignalControls.Signal = result;
                        break;
                    case 2: operationResultSignalControls.Signal = result;
                        break;
                }
                ChartWrapper.SetSignal(i, result);
                ChartWrapper.Replot();
                HistogramWrapper.SetSignal(i, result);
                HistogramWrapper.Replot();
            }
        }

        private void RebuildHistogram(object sender, RoutedEventArgs e)
        {
            HistogramWrapper.Replot();
        }

        private void SecondSignalControls_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}