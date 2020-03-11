using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace CPS
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
       public double SineSignal(double time)
        {
            double A = 1;
            double T = 1;
            double T1 = 0;
            return A * Math.Sin((2 * Math.PI / T) * (time - T1));
        }
        public MainWindow()
        {
            InitializeComponent();
            

            double T1_StartTime = 0;
            double D_DurationOfTheSignal = 5;
            int Frequency = 32;

            List<double> X = new List<double>();
            List<double> Y = new List<double>();


            for (decimal i = (decimal)T1_StartTime; i < (decimal)(T1_StartTime + D_DurationOfTheSignal); i += 1 / (decimal)Frequency)
            {
                X.Add((double)i);
                Y.Add(SineSignal((double)i));
            }


            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Sin",
                    Values = new ChartValues<double> (Y)
                },
            };

            XFormatter = value => value.ToString();
            YFormatter = value => value.ToString();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        
        private void draw_Click(object sender, RoutedEventArgs e)
        {
            double A = 1;
            double T = 1;
            double T1 = 0;

            A = Double.Parse(amplitude.Text);
            
            

        }
    }



}
