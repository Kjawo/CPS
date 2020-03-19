using CPS.Signal;
using System.Windows;

namespace CPS
{

    public partial class MainWindow : Window
    {
        private Params FirstSignalParams = new Params();
        private Params SecondSignalParams = new Params();
        private ChartWrapper ChartWrapper = new ChartWrapper();
        public double Frequency { get; set; } = 100;
        public bool SecondSignalEnabled { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Chart.DataContext = ChartWrapper;
            SecondSignalEnabler.DataContext = this;
            FirstSignalParamGrid.DataContext = FirstSignalParams;
            SecondSignalParamGrid.DataContext = SecondSignalParams;
            SecondSignalParams.T = 0.2;

            Generate(null, null);
        }

        public void Generate(object sender, RoutedEventArgs e)
        {
            ChartWrapper.Clear();
            ISignal s1 = new SinusoidalSignal();
            s1.SetParams(FirstSignalParams);
            ISignal s2 = new SinusoidalSignal();
            s2.SetParams(SecondSignalParams);

            DiscreteSignal ds1 = new Generator()
                                 .withFrequency(Frequency)
                                 .withSignal(s1)
                                 .build();
            ChartWrapper.AddSeries(ds1);

            if (SecondSignalEnabled)
            {
                DiscreteSignal ds2 = new Generator()
                                     .withFrequency(Frequency)
                                     .withSignal(s2)
                                     .build();
                ChartWrapper.AddSeries(ds2);
            }
        }

    }
}
