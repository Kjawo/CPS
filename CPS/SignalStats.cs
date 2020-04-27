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
        
        private string _MeanSquaredError = "";
        private string _SignalNoiseRatio = "";
        private string _MaxDifference = "";

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
        
        public string MeanSquaredError
        {
            get => _MeanSquaredError;
            set
            {
                _MeanSquaredError = value;
                Notify("MeanSquaredError");
            }
        }
        
        public string SignalNoiseRatio
        {
            get => _SignalNoiseRatio;
            set
            {
                _SignalNoiseRatio = value;
                Notify("SignalNoiseRatio");
            }
        }
        
        public string MaxDifference
        {
            get => _MaxDifference;
            set
            {
                _MaxDifference = value;
                Notify("MaxDifference");
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
