using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
namespace GmMeasurement
{
    
    class DataString : MeasurDataInterface
    {
        
        public string toString()
        {
            string result = "";
            FieldInfo[] info = this.GetType().BaseType.GetFields().Concat(this.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)).ToArray();
            for (int i = 0; i < info.Length - 1; i++)
            {

                if (info[i].FieldType == typeof(string))
                    result += info[i].GetValue(this) + "\t";
                if (info[i].FieldType == typeof(double))
                    result += Convert.ToString(info[i].GetValue(this), ImportantConstants.NumberFormat()) + "\t";
            }

            if (info[info.Length - 1].FieldType == typeof(string))
                result += info[info.Length - 1].GetValue(this) + "\n";
            if (info[info.Length - 1].FieldType == typeof(double))
                result += Convert.ToString(info[info.Length - 1].GetValue(this), ImportantConstants.NumberFormat()) ;
            return result;
        }

        public void parseFromString(string ReadString)
        {
            if (ReadString == "") throw new Exception("String to Parse is empty");
            string[] Parsed = ReadString.Split(new string[] { "\t" }, StringSplitOptions.None);

            FieldInfo[] info = this.GetType().BaseType.GetFields().Concat(this.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)).ToArray();
            if (Parsed.Count() < info.Count()) throw new Exception("Wrong File Format");
            for (int i = 0; i < info.Length; i++)
            {

                if (info[i].FieldType == typeof(string))
                    info[i].SetValue(this, Parsed[i]);
                if (info[i].FieldType == typeof(double))
                    info[i].SetValue(this, Double.Parse(Parsed[i], ImportantConstants.NumberFormat()));
            }



        }
        public virtual MeasurDataInterface clone()
        {
            return new DataString();
        }
    }
}
