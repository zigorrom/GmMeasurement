using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;
namespace GmMeasurement
{
    public class I_V_Graph
    {
        private ZedGraphControl _zgc;
        private GraphPane _zgcGraphPane;
        private ZedGraphController _zgcController;
        private double _CurrentBackGate=0.0;
        private string _CurrentCurveName = "";
        public I_V_Graph(ZedGraphControl zgc)
        {
            this._zgc = zgc;
            this._zgcGraphPane = this._zgc.GraphPane;
            _zgcController = new ZedGraphController(zgc);
            _zgcController.DisableTitle();

            //this._zgcGraphPane.XAxis.Title.Text = "Time (ms)";
            this._zgcGraphPane.XAxis.Title.IsVisible = true;
            this._zgcGraphPane.XAxis.Title.Text = "Front Gate Voltage (V)";
            this._zgcGraphPane.YAxis.Title.Text = "Drain Current,  (A)";
            //this._zgcGraphPane.XAxis.Cross = 0;
            //this._zgcGraphPane.YAxis.Cross = 0;
        }
       /* public void SubscribeForContiniousDataAcquisition()
        {
            AllCustomEvents.Instance.AI_Data_Arrived += AddDataToGraph;
        }
        public void UnsubscribeForContiniousDataAcquisition()
        {
            AllCustomEvents.Instance.AI_Data_Arrived -= AddDataToGraph;
        }*/
    /*   private void AddDataToGraph(object Sender, ADC_DataArrivedEventArgs data)
        {
            int i = 0;
            foreach (AI_Channel channel in AI_Channels.Instance.ChannelArray)
            {
                if (channel.Enabled)
                {
                    _AddPointPairListToTrace(data.data[i], channel.number,AI_Channels.Instance.ACQ_Rate);
                }
                i++;
            }
            _zgc.AxisChange();
            _zgc.Invalidate();
        }*/
        public void PrepareForMeasurement(double initialBackGate)
        {
            this.ClearGraphs();

            SetCurrentBackgate(initialBackGate);
            SetCurrentCurveName(initialBackGate);
            AddCurveForBackGate(_CurrentBackGate);
            AllCustomEvents.Instance.DataPoint_Arrived += DataArrived;
        }
        private void DataArrived(object sender, PointArrivedEventArgs data)
        {
            if (data.data.BackGateVoltage != _CurrentBackGate)
            {
                SetCurrentBackgate(data.data.BackGateVoltage);
                SetCurrentCurveName(data.data.BackGateVoltage);
                AddCurveForBackGate(data.data.BackGateVoltage);
            }
            _zgcController.AddToCurve(new PointPairList(new double[] { data.data.FrontGateVoltage }, new double[] { data.data.SampleCurrent }), _CurrentCurveName);
            _zgcController.Refresh();
                

        }
        public void FinishMeasurement()
        {
            AllCustomEvents.Instance.DataPoint_Arrived -= DataArrived;

        }
        private string GenerateCurveName(double BackGate)
        {
            return "Vbg=" + Convert.ToString(BackGate, ImportantConstants.NumberFormat());
        }
        private void SetCurrentBackgate(double BackGate)
        {
            _CurrentBackGate = BackGate;
        }
        private void SetCurrentCurveName(double BackGate)
        {
            _CurrentCurveName = GenerateCurveName(_CurrentBackGate);
        }
        private void AddCurveForBackGate(double BackGate)
        {
            
            
            _zgcController.AddCurve(new PointPairList(), this.GetRandomColor(), _CurrentCurveName);
        }
        private Color GetRandomColor()
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            return Color.FromKnownColor(randomColorName);
        }
        public void ClearGraphs()
        {
            _zgcGraphPane.CurveList.Clear();
            _zgc.Invalidate();
        }
    }
}
