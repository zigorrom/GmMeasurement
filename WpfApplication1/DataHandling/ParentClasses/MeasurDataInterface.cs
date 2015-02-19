using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GmMeasurement
{
    interface MeasurDataInterface {
         string toString();
         void parseFromString(string readString);
         MeasurDataInterface clone();
    }
}
