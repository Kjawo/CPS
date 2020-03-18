using CPS.Signal;
using System.Windows;

namespace CPS
{

    public partial class MainWindow : Window
    {
        private Params FirstSignalParams = new Params();
        private Params SecondSignalParams = new Params();
        public ChartWrapper ChartWrapper = new ChartWrapper();

        public MainWindow()
        {
            InitializeComponent();
            SecondSignalParams.T = 0.2;
            this.DataContext = this;
            Chart.DataContext = ChartWrapper;
            FirstSignalParamGrid.DataContext = FirstSignalParams;
            SecondSignalParamGrid.DataContext = SecondSignalParams;

            Generate(null, null);
        }

        public void Generate(object sender, RoutedEventArgs e)
        {
            ChartWrapper.UpdateSignal(FirstSignalParams, SecondSignalParams);
        }

    }
}
