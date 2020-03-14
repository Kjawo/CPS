using CPS.Signal;
using System.Windows;

namespace CPS
{

    public partial class MainWindow : Window
    {
        public double A { get; set; } = 1;
        public double t1 { get; set; } = 0;
        public double T { get; set; } = 3.14;
        public double d { get; set; } = 3.14;
        public ChartWrapper ChartWrapper = new ChartWrapper();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Chart.DataContext = ChartWrapper;

            Generate(null, null);
        }

        public void Generate(object sender, RoutedEventArgs e)
        {
            Params p = new Params();
            p.A = A;
            p.d = d;
            p.T = T;
            p.t1 = t1;
            ChartWrapper.UpdateSignal(p);
        }

    }
}
