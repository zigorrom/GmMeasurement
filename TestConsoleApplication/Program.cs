using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keithley24XXFamily;
using HP34401A;
using Devices;
using KeithleyOldMultimeter;
using StanfordLockInSR830;
namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            GPIB_Board train = new GPIB_Board(1);
            string[] Device_IDs = train.Devices;
            int CountOfDevices = Device_IDs.Count();
            Console.WriteLine("NumberOfConnectedDevices:" + CountOfDevices);
            foreach(string DeviceID in Device_IDs)
            {
                Console.WriteLine(DeviceID);
            }

            #region KeithleyIDN_TEST
            Console.WriteLine();
            GPIB_Device DeviceKeithley = new GPIB_Device("KEITHLEY");
            GPIB_Device DeviceKeithley1 = new GPIB_Device("KEITHLEY", 1);
            if (DeviceKeithley.isAlive) Console.WriteLine("Device 0 IDN:" + DeviceKeithley.RequestQuery("*IDN?"));
            else Console.WriteLine(" Device Keithley 0 not Online");
            if (DeviceKeithley1.isAlive) Console.WriteLine("Device 1 IDN:" + DeviceKeithley1.RequestQuery("*IDN?"));
            else Console.WriteLine(" Device Keithley 1 not Online");
            #endregion

            #region HEWLETT-Packard Test
            GPIB_Device DeviceHewlettPackard = new GPIB_Device("34401A");
            if (DeviceHewlettPackard.isAlive) Console.WriteLine(" Device HP MULTIM IDN: " + DeviceHewlettPackard.RequestQuery("*IDN?"));
            else Console.WriteLine(" Device HP MULTIM not Online");
            #endregion

            Keithley_24XX Keithley1 = new Keithley_24XX("KEITHLEY");
            if (Keithley1.isAlive) Console.WriteLine("Keithley1 alive");

            HP34401AMultimeter HPMultim1 = new HP34401AMultimeter("34401");
            if (HPMultim1.isAlive) Console.WriteLine("HPMultim1 alive");

            double TestMeasurementOfVoltage;
            HPMultim1.ReadVoltage(out TestMeasurementOfVoltage);
            Console.WriteLine("HP Test Measurement of Voltage: " + TestMeasurementOfVoltage.ToString());


            KeithleyMultimeter KeithleyMult = new KeithleyMultimeter("NDCV");
            if (KeithleyMult.isAlive) Console.WriteLine("Keithley Multim alive");

            KeithleyMult.ReadVoltage(out TestMeasurementOfVoltage);
            Console.WriteLine("KeithleyMultim Test Measurement of Voltage: " + TestMeasurementOfVoltage.ToString());

            StanfordSR830 LockIn = new StanfordSR830("SR830");
            if (LockIn.isAlive) Console.WriteLine("LockIn alive");

            LockIn.ReadSignal(out TestMeasurementOfVoltage);
            Console.WriteLine("LockIn Test Measurement of Signal: " + TestMeasurementOfVoltage.ToString());

            
            Keithley1.ShowText("Jo POSONY!");
            Console.WriteLine("EndOfProgram");

        }
    }
}
