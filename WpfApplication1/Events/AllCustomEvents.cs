using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GmMeasurement
{
    class AllCustomEvents
    {
        private static AllCustomEvents _instance;
        public static AllCustomEvents Instance
        {
            get
            {
                if (_instance == null) _instance = new AllCustomEvents();
                return _instance;
            }
        }
        private AllCustomEvents(){
        }

        public virtual void OnDataPoint_Arrived(object Sender, PointArrivedEventArgs e)
        {
            EventHandler<PointArrivedEventArgs> handler = _DataPoint_Arrived;
            if (handler != null)
            {
                handler(Sender, e);
            }
        }
        public virtual void OnDataPoint_Arrived(object Sender,DoubleGatedPointArrivedEventArgs e)
        {
            EventHandler<DoubleGatedPointArrivedEventArgs> handler = _DoubleGatedPoint_Arrived;
            if (handler != null)
            {
                handler(Sender, e);
            }
        }

        private EventHandler<DoubleGatedPointArrivedEventArgs> _DoubleGatedPoint_Arrived;
        public event EventHandler<DoubleGatedPointArrivedEventArgs> DoubleGatedPoint_Arrived
        {
            add
            {
                if (_DoubleGatedPoint_Arrived == null || !_DoubleGatedPoint_Arrived.GetInvocationList().Contains(value))
                {
                    _DoubleGatedPoint_Arrived += value;
                }
            }
            remove
            {
                _DoubleGatedPoint_Arrived -= value;
            }
        }


        private EventHandler<PointArrivedEventArgs> _DataPoint_Arrived;
        public event EventHandler<PointArrivedEventArgs> DataPoint_Arrived
        {
            add
            {
                if (_DataPoint_Arrived == null || !_DataPoint_Arrived.GetInvocationList().Contains(value))
                {
                    _DataPoint_Arrived += value;
                }
            }
            remove
            {
                _DataPoint_Arrived -= value;
            }
        }


        public virtual void OnMeasurementProgressReported(object Sender, MeasurementProgressReportedEventArgs e)
        {
            EventHandler<MeasurementProgressReportedEventArgs> handler = _MeasurementProgressReported;
            if (handler != null)
            {
                handler(Sender, e);
            }
        }
        private EventHandler<MeasurementProgressReportedEventArgs> _MeasurementProgressReported;
        public event EventHandler<MeasurementProgressReportedEventArgs> MeasurementProgressReported
        {
            add
            {
                if (_MeasurementProgressReported == null || !_MeasurementProgressReported.GetInvocationList().Contains(value))
                {
                    _MeasurementProgressReported += value;
                }
            }
            remove
            {
                _MeasurementProgressReported -= value;
            }
        }

        public virtual void OnMeasurementFinished(object Sender, EventArgs e)
        {
            EventHandler<EventArgs> handler = _MeasurementFinished;
            if (handler != null)
            {
                handler(Sender, e);
            }
        }
        private EventHandler<EventArgs> _MeasurementFinished;
        public event EventHandler<EventArgs> MeasurementFinished
        {
            add
            {
                if (_MeasurementFinished == null || !_MeasurementFinished.GetInvocationList().Contains(value))
                {
                    _MeasurementFinished += value;
                }
            }
            remove
            {
                _MeasurementFinished -= value;
            }
        }

    }
}
