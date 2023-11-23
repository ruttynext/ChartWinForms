using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.Themes;
using LiveChartsCore.VisualElements;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartWinForms.Chart
{
    public class OzCartesianChart : CartesianChart
    {
        LiveChartLine focusedVisualElement = null;
        public OzCartesianChart()
        {
            this.MouseWheel += OzCartesianChart_MouseWheel;
            this.VisualElementsPointerDown += OzCartesianChart_VisualElementsPointerDown;
            this.GetDrawnControl().MouseMove += OzCartesianChart_MouseMove;
            this.MouseUp += OzCartesianChart_MouseUp;this.Cursor= Cursors.Help; 
           // this.ChartPointPointerDown += OzCartesianChart_ChartPointPointerDown;
           // this.GetDrawnControl().MouseDown += OzCartesianChart_MouseDown;
        }

        



        private void OzCartesianChart_MouseDown(object? sender, MouseEventArgs e)
        {
            var point = new LvcPoint(e.Location.X, e.Location.Y);
            this.Cursor = Cursors.Default;

            //foreach (LiveChartLine ve in liveChartLines)
            //{
            //    var locationX = ve.GetActualCoordinate();
            //    if (point.X >= locationX && point.X <= locationX + 1)
            //        ve.InvokePointerDown();
            //}

            //var hitElements =
            //    liveChartLines.OfType<LiveChartLine>()

            //        .SelectMany(x => x.IsHitBy(this, point));


        }

        private void OzCartesianChart_ChartPointPointerDown(IChartView chart, LiveChartsCore.Kernel.ChartPoint? point)
        {//not need
         // throw new NotImplementedException();
         //var line = LiveChartLines.Where(l => l.Xi == visualElementsArgs.ClosestToPointerVisualElement).FirstOrDefault();
         //    if (line != null)
         //    {
         //        focusedVisualElement = line;

            //    }

            //foreach (var ve in liveChartLines)
            //    ve.InvokePointerDown();
            //}
        }

        private void OzCartesianChart_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (core == null)
            {
                throw new Exception("core not found");
            }

            CartesianChart<SkiaSharpDrawingContext> obj = (CartesianChart<SkiaSharpDrawingContext>)core;
            Point location = e.Location;
            obj.Zoom(new LvcPoint(location.X, location.Y), (e.Delta <= 0) ? ZoomDirection.ZoomOut : ZoomDirection.ZoomIn);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OzCartesianChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.Name = "OzCartesianChart";
            this.ResumeLayout(false);

        }

        private ObservableCollection<LiveChartLine> liveChartLines = new ObservableCollection<LiveChartLine>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<LiveChartLine> LiveChartLines
        {
            get { return liveChartLines; }
            set
            {
                liveChartLines = value;
                VisualElements = liveChartLines;
            }
        }

        private ObservableCollection<OzAxis> xAxesList = new ObservableCollection<OzAxis>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<OzAxis> XAxesList
        {
            get { return xAxesList; }
            set
            {
                xAxesList = value;
                XAxes = xAxesList;
            }
        }

        private ObservableCollection<OzAxis> yAxesList = new ObservableCollection<OzAxis>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<OzAxis> YAxesList
        {
            get { return yAxesList; }
            set
            {
                yAxesList = value;
                YAxes = yAxesList;


            }
        }
        public LiveChartLine AddLine(double x, SKColor color, string? id = null)
        {
            var lineAtX = CreateLineForPointX(x, color, id);
            lineAtX.Moving += LineAtX_Moving;
            LiveChartLines.Add(lineAtX);
            VisualElements = LiveChartLines;
            return lineAtX;
        }


        public OzAxis AddXAxis(double? minLimit, double? maxLimit, Func<double, string> labeler)
        {
            var xAxis = CreateAxis(minLimit, maxLimit, labeler);
            xAxis.Changed += XAxis_Changed;
            XAxesList.Add(xAxis);

            XAxes = XAxesList;


            return xAxis;
        }

        private void XAxis_Changed(OzAxis arg1, double? arg2, double? arg3)
        {
            //throw new NotImplementedException();
        }

        public OzAxis AddYAxis(double? minLimit, double? maxLimit, Func<double, string> labeler)
        {
            var yAxis = CreateAxis(minLimit, maxLimit, labeler);
            yAxis.Changed += YAxis_Changed;
            YAxesList.Add(yAxis);
            YAxes = YAxesList;


            return yAxis;
        }

        private void YAxis_Changed(OzAxis arg1, double? arg2, double? arg3)
        {
            //throw new NotImplementedException();
        }

        private void LineAtX_Moving(LiveChartLine arg1, double arg2)
        {
            return;
            // throw new NotImplementedException();
        }

        public LiveChartLine CreateLineForPointX(double x, SKColor color, string? id = null)
        {
           

            var line = new LiveChartLine
            {
                X = x,
                Y = 150 , // Adjust this based on the height of the chart
                LocationUnit = MeasureUnit.ChartValues,
                Width = 2,
                Height = this.Height*150,
                SizeUnit = MeasureUnit.Pixels,
                Stroke = new SolidColorPaint(color) { ZIndex = 10, StrokeThickness = 1.5f },
               
            };


            //var sec =  new LiveChartLine
            //{
            //    Xi = x,
            //    Xj = x,

            //    Stroke = new SolidColorPaint
            //    {
            //        Color = SKColors.Red,
            //        StrokeThickness = 3,
            //        PathEffect = new DashEffect(new float[] { 6, 6 })
            //    }

            //};


            //var line = new LiveChartLine()
            //{
            //    Xi = x,
            //    Xj = x,

            //    Stroke = new SolidColorPaint
            //    {
            //        Color = SKColors.Red,

            //    }

            //    //X = x,
            //    //Y = 8,
            //    //LocationUnit = MeasureUnit.ChartValues,
            //    //Width = 3,
            //    //Height = 300,
            //    //SizeUnit = MeasureUnit.Pixels,
            //    //Fill = new SolidColorPaint(new SKColor(239, 83, 80, 220)) { ZIndex = 10 }
            //};
            //line.PointerDown += Line_PointerDown;
            //var line = new LiveChartLine
            //{
            //    Xi = x,
            //    Xj = x,

            //    Stroke = new SolidColorPaint
            //    {
            //        Color = SKColors.Red,
            //        StrokeThickness = 3,
            //        PathEffect = new DashEffect(new float[] { 6, 6 })
            //    }

            //};
            return line;
        }

        private void Line_PointerDown(LiveChartLine arg1, EventArgs arg2)
        {
            focusedVisualElement = arg1;
        }

        public OzAxis CreateAxis(double? minLimit, double? maxLimit, Func<double, string> labeler)
        {

            var axis = new OzAxis
            {
                MinLimit = minLimit,
                MaxLimit= maxLimit,
                TextSize = 12,
            };
            
            if (labeler != null)
                axis.Labeler = labeler;

            return axis;
        }

        private void OzCartesianChart_VisualElementsPointerDown(IChartView chart, VisualElementsEventArgs<SkiaSharpDrawingContext> visualElementsArgs)
        {
            if (visualElementsArgs.ClosestToPointerVisualElement is null)
                return;
            this.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.None;
            var line = LiveChartLines.Where(l => l == visualElementsArgs.ClosestToPointerVisualElement).FirstOrDefault();
            if (line != null)
            {
                focusedVisualElement = line;

            }

        }

        private void OzCartesianChart_MouseMove(object? sender, MouseEventArgs e)
        {
            if (focusedVisualElement == null)
                return;

            var positionInData = ScalePixelsToData(new(e.Location.X, e.Location.Y));
            focusedVisualElement.X = positionInData.X;
            //focusedVisualElement.Xj = positionInData.X;
            focusedVisualElement.OnMoving(positionInData.X);
        }

        private void OzCartesianChart_MouseUp(object? sender, MouseEventArgs e)
        {
            if (focusedVisualElement != null)
            {
                focusedVisualElement = null;
                this.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.Both;
                return;
            }
        }

    }
}
