using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class MainWindow_DataModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion

        #region FrontGateSettings
        private double _FrontGateVoltageStart = 0.0;
        public double FrontGateVoltageStart
        {
            get { return _FrontGateVoltageStart; }
            set
            {
                _FrontGateVoltageStart = value;
                OnPropertyChanged("FrontGateVoltageStart");
            }
        }

        private double _FrontGateVoltageStop = 0.0;
        public double FrontGateVoltageStop
        {
            get { return _FrontGateVoltageStop; }
            set
            {
                _FrontGateVoltageStop = value;
                OnPropertyChanged("FrontGateVoltageStop");
            }
        }

        private int _FrontGateVoltageNumberOfPoints = 1;
        public int FrontGateVoltageNumberOfPoints
        {
            get { return _FrontGateVoltageNumberOfPoints; }
            set { _FrontGateVoltageNumberOfPoints = value;
            OnPropertyChanged("FrontGateVoltageNumberOfPoints");
            }
        }

        private string _FrontGateVoltageStartUnits = "V";
        public string FrontGateVoltageStartUnits
        {
            get { return _FrontGateVoltageStartUnits; }
            set
            {
                _FrontGateVoltageStartUnits = value;
                OnPropertyChanged("FrontGateVoltageStartUnits");
            }
        }

        private string _FrontGateVoltageStopUnits = "V";
        public string FrontGateVoltageStopUnits
        {
            get { return _FrontGateVoltageStopUnits; }
            set
            {
                _FrontGateVoltageStopUnits = value;
                OnPropertyChanged("FrontGateVoltageStopUnits");
            }
        }

        private double _FrontGateVoltageStartValue;
        public double FrontGateVoltageStartValue
        {
            get {
                 switch(FrontGateVoltageStartUnits){
                     case "V":  {return FrontGateVoltageStart;}
                     case "mV": { return FrontGateVoltageStart * 0.001; }
                     default: return FrontGateVoltageStart;
                 }
            }
            set 
                {
                    _FrontGateVoltageStartValue=value;
                FrontGateVoltageStart=value;
                FrontGateVoltageStartUnits="V";
            }
        }
        

    private double _FrontGateVoltageStopValue;
        public double FrontGateVoltageStopValue
        {
            get {
                 switch(FrontGateVoltageStopUnits){
                     case "V":  {return FrontGateVoltageStop;break;}
                     case "mV": {return FrontGateVoltageStop*0.001;break;}
                     default: return FrontGateVoltageStop;
                 }
            }
            set 
                {
                    _FrontGateVoltageStopValue=value;
                FrontGateVoltageStop=value;
                FrontGateVoltageStopUnits = "V";
            }
        }

        #endregion

        #region BackGateSettings
        private double _BackGateVoltageStart = 0.0;
        public double BackGateVoltageStart
        {
            get { return _BackGateVoltageStart; }
            set
            {
                _BackGateVoltageStart = value;
                OnPropertyChanged("BackGateVoltageStart");
            }
        }

        private double _BackGateVoltageStop = 0.0;
        public double BackGateVoltageStop
        {
            get { return _BackGateVoltageStop; }
            set
            {
                _BackGateVoltageStop = value;
                OnPropertyChanged("BackGateVoltageStop");
            }
        }

        private int _BackGateVoltageNumberOfPoints = 1;
        public int BackGateVoltageNumberOfPoints
        {
            get { return _BackGateVoltageNumberOfPoints; }
            set
            {
                _BackGateVoltageNumberOfPoints = value;
                OnPropertyChanged("BackGateVoltageNumberOfPoints");
            }
        }

        private string _BackGateVoltageStartUnits = "V";
        public string BackGateVoltageStartUnits
        {
            get { return _BackGateVoltageStartUnits; }
            set
            {
                _BackGateVoltageStartUnits = value;
                OnPropertyChanged("BackGateVoltageStartUnits");
            }
        }

        private string _BackGateVoltageStopUnits = "V";
        public string BackGateVoltageStopUnits
        {
            get { return _BackGateVoltageStopUnits; }
            set
            {
                _BackGateVoltageStopUnits = value;
                OnPropertyChanged("BackGateVoltageStopUnits");
            }
        }

        private double _BackGateVoltageStartValue;
        public double BackGateVoltageStartValue
        {
            get
            {
                switch (BackGateVoltageStartUnits)
                {
                    case "V": { return BackGateVoltageStart; break; }
                    case "mV": { return BackGateVoltageStart * 0.001; break; }
                    default: return BackGateVoltageStart;
                }
            }
            set
            {
                _BackGateVoltageStartValue = value;
                BackGateVoltageStart = value;
                BackGateVoltageStartUnits = "V";
            }
        }


        private double _BackGateVoltageStopValue;
        public double BackGateVoltageStopValue
        {
            get
            {
                switch (BackGateVoltageStopUnits)
                {
                    case "V": { return BackGateVoltageStop; break; }
                    case "mV": { return BackGateVoltageStop * 0.001; break; }
                    default: return BackGateVoltageStop;
                }
            }
            set
            {
                _BackGateVoltageStopValue = value;
                BackGateVoltageStop = value;
                BackGateVoltageStopUnits = "V";
            }
        }
        #endregion 

        #region WorkFolder and File

        private string _WorkFolder = "";
        public string WorkFolder
        {
            get { return _WorkFolder; }
            set
            {
                _WorkFolder = value;
                OnPropertyChanged("WorkFolder");
            }
        }

        private string _FileName = "Measurement.dat";
        public string FileName
        {
            get { return _FileName; }
            set
            {
                _FileName = value;
                OnPropertyChanged("FileName");
            }
        }
        #endregion

        private double _KAmpl = 1;
        public double KAmpl
        {
            get { return _KAmpl; }
            set
            {
                _KAmpl = value;
                OnPropertyChanged("KAmpl");
            }
        }

        private double _StimulationValue = 1;
        public double StimulationValue
        {
            get { return _StimulationValue; }
            set
            {
                _StimulationValue = value;
                OnPropertyChanged("StimulationValue");
            }
        }
    }
    
}
