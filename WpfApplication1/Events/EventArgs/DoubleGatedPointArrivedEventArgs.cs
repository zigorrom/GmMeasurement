using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class DoubleGatedPointArrivedEventArgs:EventArgs
    {
        private DoubleGatedGmDataString _data;
        public DoubleGatedGmDataString data
        {
            get { return _data; }
            set { _data = value; }
        }

        public DoubleGatedPointArrivedEventArgs(DoubleGatedGmDataString Data)
        {
            _data = Data;
        }
    }
}
