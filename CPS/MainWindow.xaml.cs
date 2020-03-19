using CPS.Signal;
using System.Collections.Generic;
using System.Windows;
using static CPS.Generator;

namespace CPS
{
    public class ModeWrapper
    {
        public string Name { get; set; }
        public Mode Mode { get; set; }
    }

    public partial class MainWindow : Window
    {
        private Params FirstSignalParams = new Params();
        private Params SecondSignalParams = new Params();
        private ChartWrapper ChartWrapper = new ChartWrapper();
        public double Frequency { get; set; } = 100;
        public bool SecondSignalEnabled { get; set; } = false;
        public List<ModeWrapper> ModeList { get; } = new List<ModeWrapper>
        {
            new ModeWrapper { Name = "Domyślny", Mode = Mode.DEFAULT },
            new ModeWrapper { Name = "Suma", Mode = Mode.SUM },
        };
        public ModeWrapper SelectedMode { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Chart.DataContext = ChartWrapper;
            SecondSignalEnabler.DataContext = this;
            FirstSignalParamGrid.DataContext = FirstSignalParams;
            SecondSignalParamGrid.DataContext = SecondSignalParams;
            SecondSignalParams.T = 0.2;
            SelectedMode = ModeList[0];

            Generate(null, null);
        }

        public void Generate(object sender, RoutedEventArgs e)
        {
            ChartWrapper.Clear();
            ISignal s1 = new SinusoidalSignal();
            ISignal s2 = new SinusoidalSignal();
            s1.SetParams(FirstSignalParams);
            s2.SetParams(SecondSignalParams);

            DiscreteSignal ds1 = new Generator()
                                 .withFrequency(Frequency)
                                 .withSignal(s1)
                                 .withSecondarySignal(s2)
                                 .withMode(SelectedMode.Mode)
                                 .build();
            ChartWrapper.AddSeries(ds1);

            if (SecondSignalEnabled && SelectedMode.Mode == Mode.DEFAULT)
            {
                DiscreteSignal ds2 = new Generator()
                                 .withFrequency(Frequency)
                                 .withSignal(s2)
                                 .withMode(SelectedMode.Mode)
                                 .build();
                ChartWrapper.AddSeries(ds2);
            }
        }

    }
}
