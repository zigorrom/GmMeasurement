using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmMeasurement
{
    class DoubleGatedGmDataString : DataString
    {
        public double FrontGateVoltage;
        public double BackGateVoltage;
        public double SampleVoltage;
        public double SampleCurrent;
        public double GmFG;
        public double GmBG;
        public double FrontGateCurrent;
        public double BackGateCurrent;

        public DoubleGatedGmDataString(double FrontGateVoltage = 0, double BackGateVoltage = 0, double SampleVoltage = 0, double SampleCurrent = 0, double GmFG = 0, double GmBG = 0, double FrontGateCurrent = 0, double BackGateCurrent = 0)
        {
            this.FrontGateVoltage = FrontGateVoltage;
            this.BackGateVoltage = BackGateVoltage;
            this.SampleVoltage = SampleVoltage;
            this.SampleCurrent = SampleCurrent;
            this.GmFG = GmFG;
            this.GmBG = GmBG;
            this.FrontGateCurrent = FrontGateCurrent;
            this.BackGateCurrent = BackGateCurrent;

        }
        public DoubleGatedGmDataString blankCopy(double FrontGateVoltage = 0, double BackGateVoltage = 0, double SampleVoltage = 0, double SampleCurrent = 0, double GmFG = 0,double GmBG=0)
        {
            return new DoubleGatedGmDataString(FrontGateVoltage, BackGateVoltage, SampleVoltage, SampleCurrent, GmFG,GmBG);
        }
        public override MeasurDataInterface clone()
        {
            return new DoubleGatedGmDataString(this.FrontGateVoltage, this.BackGateVoltage, this.SampleVoltage, this.SampleCurrent, this.GmFG, this.GmBG);
        }
    }
}
