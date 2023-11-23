using ChartWinForms.Chart;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChartWinForms
{
    public class LiveChartLine : GeometryVisual<RectangleGeometry>
    {

        public int Id { get; set; }

        public LiveChartLine()
        {

            //this.PointerDown += LiveChartLine_PointerDown;
        }

        // bool _pointerDown;

        public event Action<LiveChartLine, EventArgs> PointerDown;

        internal virtual void InvokePointerDown()
        {
            PointerDown?.Invoke(this, EventArgs.Empty);
        }

        public event Action<LiveChartLine, double> Moving;

        public void OnMoving(double x)
        {
            Moving?.Invoke(this, x);
           

        }

        protected Scaler? PrimaryScaler { get; private set; }

        /// <summary>
        /// Gets the secondary scaler.
        /// </summary>
        protected Scaler? SecondaryScaler { get; private set; }
       

    }

}
