using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;


namespace GmMeasurement
{
    class ZedGraphController
    {
        private ZedGraphControl zgc;
        private GraphPane GraphicPane;
        public ZedGraphController(ZedGraphControl zdf)
        {
            zgc = zdf;
            GraphicPane = zgc.GraphPane;
            
        }
        public void DisableTitle()
        {
            zgc.GraphPane.Title.IsVisible = false;
        }
       public void setTitle(string Title)
        {
            zgc.GraphPane.Title.IsVisible = true;
            this.zgc.GraphPane.Title.Text = Title;
        }
        
        public void SetLogXY()
         {
             GraphicPane.XAxis.Type = AxisType.Log;
             GraphicPane.YAxis.Type = AxisType.Log;
         }
        public void SetAutoScale()
        {
            GraphicPane.XAxis.Scale.MinAuto = true;
            GraphicPane.XAxis.Scale.MaxAuto = true;

            // Установим масштаб по умолчанию для оси Y
            GraphicPane.YAxis.Scale.MinAuto = true;
            GraphicPane.YAxis.Scale.MaxAuto = true;
        }
        public void Refresh()
         {
            zgc.AxisChange();
            zgc.Invalidate();
         }
        public void Replot()
        {
            zgc.Invalidate();
        }
        public void Clear(string name="")
        {
            if(name=="")GraphicPane.CurveList.Clear();
            else
            {LineItem curve = this.getCurveByName(name);
            if (curve != null)
                GraphicPane.CurveList.Remove(curve);
                 }
            this.Replot();
            
        }
        public void AddCurve(PointPairList a, System.Drawing.Color c, string name = "Noise1")
        {

            LineItem curve = this.getCurveByName(name);
            if (curve == null)
                curve = GraphicPane.AddCurve(name, a, c, SymbolType.None);
            else
                curve.Points = a;

            this.Replot();

        }
        public void AddToCurve(PointPairList data, string name)
        {
            LineItem curve = this.getCurveByName(name);
            if (curve == null) { this.AddCurve(data, System.Drawing.Color.Black, name); return; }
            PointPairList PreviousData = new PointPairList(curve.Points);
            PreviousData.Add(data);
            curve.Points = PreviousData;
                        
        }
        public CurveList Curves
        {
            get 
            {
                return this.GraphicPane.CurveList;
            }
        }
        public LineItem getCurveByName(string name = "")
        {
            foreach (LineItem needle in GraphicPane.CurveList)
            {
                if (name == needle.Label.Text)
                {
                    return needle;
                }

            }
            return null;    
        }
        

      }
}
