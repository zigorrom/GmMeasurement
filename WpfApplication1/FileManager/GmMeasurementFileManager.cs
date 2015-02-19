using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class GmMeasurementFileManager:FileManagerParent
    {

        public GmMeasurementFileManager(string folder):base(folder)
         {
             _ListDataString = new ListGmDataString();
        }
        private ListGmDataString _ListDataString;
        public void PrepareForMeasurement(string FileName)
        {
            _ListDataString.FileName = FileName;
            _ListDataString.OpenFileForWriting(true);
            AllCustomEvents.Instance.DataPoint_Arrived += DataPointArrived;
        }
        private void DataPointArrived(object sender, PointArrivedEventArgs data)
        {
            _ListDataString.GetDataFromEventAndWriteToOpenedFile(data);
        }

        public void FinishMeasurement()
        {
            AllCustomEvents.Instance.DataPoint_Arrived -= DataPointArrived;
            _ListDataString.CloseFileForWriting();

        }
        

    }
}
