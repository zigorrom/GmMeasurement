using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class MeasurementFinishedEventArgs: EventArgs
    {
        public string Message;
        public MeasurementFinishedEventArgs(string FinishMessage)
        {
            Message = FinishMessage;
        }
    }
}
