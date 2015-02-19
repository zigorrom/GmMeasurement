using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class ListGmDataString : ListDataString
    {
        public ListGmDataString(){
            this.DataString = new GmDataString();
            this.ListOfData = new List<MeasurDataInterface>();

            FileHeader = "V\\-(FrontGate)\tV\\-(BackGate)\tV\\-(Sample)\tI\\-(Sample)\tg\\-(m)\tI\\-(Front Gate)\tI\\-(Back Gate)";
            FileSubheader = "V\tV\tV\tA\tA/V\tA\tA";
            FileName = "GmMeasurement.dat";
        }
            public void GetDataFromEventAndWriteToOpenedFile(PointArrivedEventArgs data)
            {
                ListOfData.Clear();
                ListOfData.Add(data.data);
                this.writeToFile();
            }

        }
    }

