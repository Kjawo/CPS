using System.ComponentModel;

namespace CPS
{
    public class SignalStats : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _AveragePower = "";
        private string _Variance = "";
        private string _RootMeanSquare = "";
        private string _AverageAbsValue = "";
        private string _AverageValue = "";

        public string AveragePower
        {
            get => _AveragePower;
            set
            {
                _AveragePower = value;
                Notify("AveragePower");
            }
        }

        public string Variance
        {
            get => _Variance;
            set
            {
                _Variance = value;
                Notify("Variance");
            }
        }

        public string RootMeanSquare
        {
            get => _RootMeanSquare;
            set
            {
                _RootMeanSquare = value;
                Notify("RootMeanSquare");
            }
        }

        public string AverageAbsValue
        {
            get => _AverageAbsValue;
            set
            {
                _AverageAbsValue = value;
                Notify("AverageAbsValue");
            }
        }

        public string AverageValue
        {
            get => _AverageValue;
            set
            {
                _AverageValue = value;
                Notify("AverageValue");
            }
        }

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
