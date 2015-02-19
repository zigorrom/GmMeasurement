using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
namespace GmMeasurement
{
    class PointArrivedEventArgs:EventArgs
    {
        private GmDataString _data;
        public GmDataString data
        {
            get {return _data;}
            set { _data = value; }
        }

        
        public PointArrivedEventArgs(GmDataString Data)
        {
            data = Data;
        }
        
    }
}
