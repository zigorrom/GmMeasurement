using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class DoubleGatedListGmDataString:ListDataString
    {
        public DoubleGatedListGmDataString()
        {
            this.DataString = new DoubleGatedGmDataString();
            this.ListOfData = new List<MeasurDataInterface>();

            FileHeader = "V\\-(FrontGate)\tV\\-(BackGate)\tV\\-(Sample)\tI\\-(Sample)\tFG g\\-(m)\tBG g\\-(m)\tI\\-(Front Gate)\tI\\-(Back Gate)";
            FileSubheader = "V\tV\tV\tA\tA/V\tA/V\tA\tA";
            FileName = "DoubleGatedGmMeasurement.dat";
        }
        public void GetDataFromEventAndWriteToOpenedFile(DoubleGatedPointArrivedEventArgs data)
        {
            ListOfData.Clear();
            ListOfData.Add(data.data);
            this.writeToFile();
        }
    }
}
