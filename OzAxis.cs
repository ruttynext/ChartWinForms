using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ChartWinForms
{
    public class OzAxis : Axis<SkiaSharpDrawingContext, LabelGeometry, LineGeometry>
    {
        public OzAxis()
        {
            
        }
        protected override void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
        {
            base.SetProperty(ref field, newValue, propertyName);

            if (propertyName == "MinLimit" || propertyName == "MaxLimit")
            {
                Changed?.Invoke(this, MinLimit, MaxLimit);
            }
        }

        public event Action<OzAxis, double?, double?> Changed;

    }
}
