using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class DoubleGatedGmMeasurementFileManager:FileManagerParent
    {
        public DoubleGatedGmMeasurementFileManager(string folder)
            : base(folder)
         {
             _ListDataString = new DoubleGatedListGmDataString();
        }
        private DoubleGatedListGmDataString _ListDataString;
        public void PrepareForMeasurement(string FileName)
        {
            _ListDataString.FileName = FileName;
            _ListDataString.OpenFileForWriting(true);
            AllCustomEvents.Instance.DoubleGatedPoint_Arrived += DataPointArrived;
        }
        private void DataPointArrived(object sender, DoubleGatedPointArrivedEventArgs data)
        {
            _ListDataString.GetDataFromEventAndWriteToOpenedFile(data);
        }

        public void FinishMeasurement()
        {
            AllCustomEvents.Instance.DoubleGatedPoint_Arrived -= DataPointArrived;
            _ListDataString.CloseFileForWriting();

        }
    }
}
