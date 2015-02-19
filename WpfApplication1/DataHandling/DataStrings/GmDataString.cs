using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class GmDataString : DataString
    {
        public double FrontGateVoltage;
        public double BackGateVoltage;
        public double SampleVoltage;
        public double SampleCurrent;
        public double Gm;
        public double FrontGateCurrent;
        public double BackGateCurrent;

        public GmDataString(double FrontGateVoltage1 = 0, double BackGateVoltage1 = 0, double SampleVoltage1 = 0, double SampleCurrent1 = 0, double Gm1 = 0, double FrontGateCurrent1=0,double BackGateCurrent1=0)
        {
            FrontGateVoltage = FrontGateVoltage1;
            BackGateVoltage = BackGateVoltage1;
            SampleVoltage = SampleVoltage1;
            SampleCurrent = SampleCurrent1;
            Gm = Gm1;
            FrontGateCurrent = FrontGateCurrent1;
            BackGateCurrent = BackGateCurrent1;

        }
        public GmDataString blankCopy(double FrontGateVoltage1 = 0,double BackGateVoltage1 = 0,double SampleVoltage1 = 0, double SampleCurrent1 = 0, double Gm1 = 0 )
        {
            return new GmDataString(FrontGateVoltage1,BackGateVoltage1,SampleVoltage1,SampleCurrent1, Gm1);
        }
        public override MeasurDataInterface clone()
        {
            return new GmDataString(this.FrontGateVoltage, this.BackGateVoltage, this.SampleVoltage, this.SampleCurrent, this.Gm);
        }
    }
}
