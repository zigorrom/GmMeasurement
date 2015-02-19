using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class MeasurementProgressReportedEventArgs : EventArgs
    {


        private double _percentProgress;
        public double percentProgress
            {
                get { return _percentProgress; }
                set { _percentProgress = value; }
            }


            public MeasurementProgressReportedEventArgs(double PercentOfProgress)
            {
                percentProgress = PercentOfProgress;
            }

        
    }
}
