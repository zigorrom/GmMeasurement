using System;

using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Keithley24XXFamily;
using HP34401A;
using Devices;
using KeithleyOldMultimeter;
using StanfordLockInSR830;

namespace GmMeasurement
{
    class GmMeasurementParameters
    {
        public double FrontGateVoltageStart=0;
        public double FrontGateVoltageStop=0;
        public double FrontGateVoltageIncrement=0;
        public int FrontGateVoltageNumberOfPoints=0;

        public double BackGateVoltageStart=0;
        public double BackGateVoltageStop=0;
        public double BackGateVoltageIncrement=0;
        public int BackGateNumberOfPoints=0;

        public double kAmpl=0;
        public double StimulationValue=0;

        public string FileName="";
        public string folder="";

        public GmMeasurementParameters(MainWindow_DataModel data)
        {
            FrontGateVoltageStart = data.FrontGateVoltageStart;
            FrontGateVoltageStop = data.FrontGateVoltageStop;
            FrontGateVoltageNumberOfPoints = data.FrontGateVoltageNumberOfPoints;
            if (FrontGateVoltageNumberOfPoints == 0)
            {
                FrontGateVoltageNumberOfPoints = 1;
                FrontGateVoltageIncrement = 0;
            }
            else
             FrontGateVoltageIncrement = (FrontGateVoltageStop - FrontGateVoltageStart) / (FrontGateVoltageNumberOfPoints - 1);

            BackGateVoltageStart = data.BackGateVoltageStart;
            BackGateVoltageStop = data.BackGateVoltageStop;
            BackGateNumberOfPoints = data.BackGateVoltageNumberOfPoints;
            if (BackGateNumberOfPoints == 0)
            {
                BackGateNumberOfPoints = 1;
                BackGateVoltageIncrement = 0;
            }
            else
            BackGateVoltageIncrement = (BackGateVoltageStop - BackGateVoltageStart) / (BackGateNumberOfPoints-1);
            kAmpl = data.KAmpl;
            StimulationValue = data.StimulationValue;
        }

    }
    class GmMeasurement
    {
       
        private GmMeasurementParameters _MeasurementParameters;
        private Keithley_24XX FrontGateKeithley;
        private Keithley_24XX BackGateKeithley;
        private KeithleyMultimeter SampleVoltage;
        private StanfordSR830 GmMeasurer;
        public BackgroundWorker _MainWorker;
        private HP34401AMultimeter CurrentMeasurer;
        public GmMeasurement(GmMeasurementParameters MeasurementParameters)
        {
            this._MeasurementParameters = MeasurementParameters;
            FrontGateKeithley = new Keithley_24XX("2400");
            BackGateKeithley = new Keithley_24XX("2430");
            SampleVoltage = new KeithleyMultimeter("NDCV");
            CurrentMeasurer = new HP34401AMultimeter("34401");
            GmMeasurer = new StanfordSR830("SR830");
            if (!FrontGateKeithley.isAlive) throw new Exception("Keithley 2400 does not work");
            if (!BackGateKeithley.isAlive) throw new Exception("Keithley 2430 does not work");
            if (!SampleVoltage.isAlive) throw new Exception("Keithley Multimeter does not work");
            if (!CurrentMeasurer.isAlive) throw new Exception("HP34401a Multimeter does not work");
            if (!GmMeasurer.isAlive) throw new Exception("Stanford SR830 Lock in does not work");
            
        }
        
        private void PerformSinglePointMeasurement(double FrontGateVoltage, double BackGateVoltage)
        {
            FrontGateKeithley.SourceVoltage(FrontGateVoltage);
            BackGateKeithley.SourceVoltage(BackGateVoltage);
            var Result = new GmDataString();
            Result.FrontGateVoltage=FrontGateVoltage;
            Result.BackGateVoltage=BackGateVoltage;
            double Voltage,Current,Resistance,Signal;
            FrontGateKeithley.MeasureAll(out Voltage, out Current, out Resistance);
            Result.FrontGateCurrent = Current;
            FrontGateKeithley.MeasureAll(out Voltage, out Current, out Resistance);
            Result.BackGateCurrent = Current;
            GmMeasurer.ReadSignal(out Signal);
            Result.Gm = Signal / _MeasurementParameters.kAmpl / _MeasurementParameters.StimulationValue;
            SampleVoltage.ReadVoltage(out Voltage);
            Result.SampleVoltage = Voltage;
            CurrentMeasurer.ReadVoltage(out Voltage);
            Result.SampleCurrent = Voltage / _MeasurementParameters.kAmpl;
            AllCustomEvents.Instance.OnDataPoint_Arrived(this, new PointArrivedEventArgs(Result));
        }

        private void MapGmInWorker(object sender, DoWorkEventArgs e )
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int total_count = _MeasurementParameters.FrontGateVoltageNumberOfPoints*_MeasurementParameters.BackGateNumberOfPoints;
            int counter=0;
            FrontGateKeithley.SourceVoltage(0);
            BackGateKeithley.SourceVoltage(0);
            FrontGateKeithley.SwitchOn();
            BackGateKeithley.SwitchOn();
            for (double BackGateVoltage = _MeasurementParameters.BackGateVoltageStart; (_MeasurementParameters.BackGateVoltageStart <= _MeasurementParameters.BackGateVoltageStop) ? (BackGateVoltage <= _MeasurementParameters.BackGateVoltageStop) : ((BackGateVoltage >= _MeasurementParameters.BackGateVoltageStop)); BackGateVoltage += _MeasurementParameters.BackGateVoltageIncrement)
                for (double FrontGateVoltage = _MeasurementParameters.FrontGateVoltageStart; (_MeasurementParameters.FrontGateVoltageStart <= _MeasurementParameters.FrontGateVoltageStop) ? (FrontGateVoltage <= _MeasurementParameters.FrontGateVoltageStop) : (FrontGateVoltage >= _MeasurementParameters.FrontGateVoltageStop); FrontGateVoltage += _MeasurementParameters.FrontGateVoltageIncrement)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        ImportantConstants.MeasurementInProgress = false;
                        break;
                    }
                    PerformSinglePointMeasurement(FrontGateVoltage, BackGateVoltage);
                    counter++;
                    int progress = (int)(((double)counter / (double)total_count) * 100.0);
                    worker.ReportProgress(progress);
                }
            FrontGateKeithley.SwitchOFF();
            BackGateKeithley.SwitchOFF();
            
        }

        private void MeasurementProgress(object sender,ProgressChangedEventArgs e)
        {
            AllCustomEvents.Instance.OnMeasurementProgressReported(this, new MeasurementProgressReportedEventArgs(e.ProgressPercentage));
        }
        private void MainWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var message="";
            if(e.Cancelled==true)
            {
                message = "Measurement Cancelled";
            }
            if(e.Cancelled==false)
            {
                message = "Measurement Finished";
            }
            AllCustomEvents.Instance.OnMeasurementFinished(this, new MeasurementFinishedEventArgs(message));
        }
        public void StartGmMeasurement()
        {
             _MainWorker= new BackgroundWorker();
            _MainWorker.WorkerSupportsCancellation = true;
            _MainWorker.WorkerReportsProgress = true;
            _MainWorker.ProgressChanged += MeasurementProgress;
            _MainWorker.DoWork += MapGmInWorker;
            _MainWorker.RunWorkerCompleted+=MainWorker_RunWorkerCompleted;
            ImportantConstants.MeasurementInProgress = true;
            _MainWorker.RunWorkerAsync();
        }

        
        public void StopGmMeasurement()
        {
            if(_MainWorker.IsBusy)
            {
                _MainWorker.CancelAsync();
            }
        }



    }
}
