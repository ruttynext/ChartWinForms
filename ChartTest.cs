using ChartWinForms.Chart;
using CommunityToolkit.Mvvm.ComponentModel;
using Eco.FrameworkImpl.LockRegions;
using Eco.ViewModel.Runtime;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
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
using LiveChartsCore.VisualElements;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartWinForms
{

    public enum LineType
    {
        Start,
        End
    }
    public partial class ChartTest : UserControl
    {
        //private readonly ChartVM chartVM;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<ISeries> Series
        {
            get => chart.Series;
            set
            {
                chart.Series = value;
            }
        }

        private Cursor myCursor;

        public Cursor MyCursor
        {
            get { return myCursor; }
            set { myCursor = value; OnCursorChanged(); }
        }
        public event Action<Cursor> CursorChange;

        public void OnCursorChange(Cursor c)
        {
            CursorChange?.Invoke(c);


        }

       
        private void OnCursorChanged()
        {
            throw new NotImplementedException();
        }

        ResolutionChart? resolutionChart;

        public ResolutionChart? ResolutionChart
        {
            get => resolutionChart;
            set
            {
                // chart.XAxes = value;


                if (!DesignMode)
                {
                    if (resolutionChart != value)
                    {
                        resolutionChart = value;
                        if (value != null)
                        {

                            RegisterToOnResolutionChartChanged();

                        }
                    }
                }



            }
        }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<ICartesianAxis> YAxes
        {
            get => chart.YAxes;
            set
            {
                chart.YAxes = value;
            }
        }

        ObservableCollection<TimeRange>? ranges;
        public ObservableCollection<TimeRange>? Ranges
        {
            get => ranges;
            set
            {
                if (!DesignMode)
                {
                    if (ranges != value)
                    {
                        if (ranges != null)//The previous/origional value
                        {
                            ranges.CollectionChanged -= Ranges_CollectionChanged;
                        }
                        ranges = value;
                        HandleRangesChanged();
                    }
                }
            }
        }


        public RectangularSection[] Sections { get; set; } =
   {
        new RectangularSection
        {
            Xi=DateTime.Now.ToOADate(), Xj=8,

            Stroke = new SolidColorPaint
            {
                Color = SKColors.Red,
                StrokeThickness = 3,
                PathEffect = new DashEffect(new float[] { 6, 6 })
            }

        },

    };

        private void Ranges_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    break;

                case NotifyCollectionChangedAction.Remove:
                  
                    break;

                default:
                    break;
            }

          
        }


        List<UITimeRangeLine> allRangesLines = new List<UITimeRangeLine>();
        int index = 0;
        private void RegisterToOnRangesChanged()
        {
            foreach (var range in Ranges)
            {
                AddRange(range, index);
                index++;
            }
        }



        private void AddRange(TimeRange range, int index)
        {

            var rangeLines = new UITimeRangeLine()
            {
                Range = range,
                StartLine = CreateLineAtX(range.Start.XDate, index, LineType.Start),
                EndLine = CreateLineAtX(range.End.XDate, index, LineType.End)
            };
            allRangesLines.Add(rangeLines);
            range.StartPositionChanged += Range_StartPositionChanged;
            range.EndPositionChanged += Range_EndPositionChanged;

        }

        private LiveChartLine CreateLineAtX(DateTime time, int index, LineType lineType)
        {
            var x = time.ToOADate();
            var color = getColor(index, lineType);
            return chart.AddLine(x, color);


        }

        private SKColor getColor(int index, LineType lineType)
        {
            SKColor color;

            if (index == 0)
            { 
                if(lineType == LineType.Start)
                    color = new SKColor(System.Drawing.Color.Red.R, System.Drawing.Color.Red.G, System.Drawing.Color.Red.B, System.Drawing.Color.Red.A);
                else
                    color = new SKColor(System.Drawing.Color.Yellow.R, System.Drawing.Color.Yellow.G, System.Drawing.Color.Yellow.B, System.Drawing.Color.Yellow.A);
                
            }
            else
            {
                Random random = new Random();

                var randomColor = new SKColor(
                    (byte)random.Next(256),   // Red component
                    (byte)random.Next(256),   // Green component
                    (byte)random.Next(256),   // Blue component
                    (byte)random.Next(256)    // Alpha component
                );
                color = randomColor;
            }
            return color;
        }

        private void RemoveRange(TimeRange range)
        {
            var foundRange = allRangesLines.Where(rl => rl.Range.Equals(range)).FirstOrDefault();
            if (foundRange == null) return;

            range.StartPositionChanged -= Range_StartPositionChanged;
            range.EndPositionChanged -= Range_EndPositionChanged;
            allRangesLines.Remove(foundRange);
        }

        private void ClearRanges()
        {
            if (Ranges == null) return;
            foreach (var rangeLine in Ranges)
            {
                RemoveRange(rangeLine);
            }
        }

        UIResolutionChart? uiResolutionChart;
        private void RegisterToOnResolutionChartChanged()
        {
           
            var uiResolutionChart = new UIResolutionChart()
            {
                Resolution = ResolutionChart,
                XAxis = chart.AddXAxis(ResolutionChart.XAxisResolution.MinLimit, 
                                       ResolutionChart.XAxisResolution.MaxLimit, 
                                       ResolutionChart.XAxisResolution.Labeler),
                YAxis = chart.AddYAxis(ResolutionChart.YAxisResolution.MinLimit,
                                       ResolutionChart.YAxisResolution.MaxLimit,
                                       ResolutionChart.YAxisResolution.Labeler),
               
            };

            ResolutionChart.XAxisResolutionChanged += ResolutionChart_XAxisResolutionChanged;
            ResolutionChart.YAxisResolutionChanged += ResolutionChart_YAxisResolutionChanged;


        }

        private void ResolutionChart_YAxisResolutionChanged(ResolutionChart arg1, AxisResolution arg2)
        {
            //throw new NotImplementedException();
        }

        private void ResolutionChart_XAxisResolutionChanged(ResolutionChart arg1, AxisResolution arg2)
        {
            //throw new NotImplementedException();
        }
    

        private void Range_StartPositionChanged(TimeRange sender, TimeRangeLine obj)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            if (DataSource != null)
            {
                var rangesProperty = DataSource.GetType().GetProperty("Ranges");

                if (rangesProperty != null)
                {
                    rangesProperty.SetValue(DataSource, Ranges);
                }
            }
        }

        private void Range_EndPositionChanged(TimeRange sender, TimeRangeLine obj)
        {

            UpdateDataSource();
        }

        private OzCartesianChart chart;

        public ChartTest()
        {
            myCursor = Cursors.Default;
            if (!DesignMode)
            {
                chart = new OzCartesianChart
                {
                    //ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.Both,
                    Size = new System.Drawing.Size(this.Width, this.Height),
                    EasingFunction = null,
                    Dock = DockStyle.Fill,
                    Tooltip = null,
                    Cursor= myCursor,   
                };
                SetMappingChart();
                Controls.Add(chart);
                 
            }
            this.CursorChange += ChartTest_CursorChange;

            InitializeComponent();
            
        }
        
        private void ChartTest_CursorChange(Cursor obj)
        {
            throw new NotImplementedException();
        }

        protected override void OnBindingContextChanged(EventArgs e)
        {
            base.OnBindingContextChanged(e);

            HandleDataSourceChanged();
        }

        private void HandleDataSourceChanged()
        {

            // Check if the DataSource is set and if it contains the "Ranges" property
            if (this.DataSource != null && this.DataSource.GetType().GetProperty("Ranges") != null)
            {
                var rangesProperty = this.DataSource.GetType().GetProperty("Ranges");
                var rangesValue = rangesProperty.GetValue(this.DataSource, null);

                // Here, handle the rangesValue as needed, for example, binding it to the ListBox items
                if (rangesValue is IEnumerable rangesEnumerable)
                {
                    Ranges = rangesValue as ObservableCollection<TimeRange>;
                }
            }
        }


        private void HandleRangesChanged()
        {
            if (ranges != null)
            {


                ranges.CollectionChanged += Ranges_CollectionChanged;
                RegisterToOnRangesChanged();

            }
        }

        private object dataSource;

        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if (dataSource != value)
                {
                    dataSource = value;
                    OnDataSourceChanged();
                }
            }
        }

        protected virtual void OnDataSourceChanged()
        {
            HandleDataSourceChanged();
        }

        private void SetMappingChart()
        {
            LiveCharts.Configure(config =>
                config.HasMap<SamplingRecordPoint>(
                           (sample, chartPoint) =>
                           {
                               chartPoint.Coordinate = new(sample.Time, sample.Value.Value);
                           }));
        }
    }

    public class TimeRange
    {
        public TimeRange(string name, DateTime start, DateTime end, Color? color = null)
        {
            Start = new TimeRangeLine(start, color);
            End = new TimeRangeLine(end, color);
        }

        private TimeRangeLine _start;
        private TimeRangeLine _end;

        public string Name { get; set; }

        public TimeRangeLine Start
        {
            get { return _start; }
            set
            {
                if (_start != value)
                {
                    if (_start != null)
                    {
                        _start.XDateChanged -= OnStartXDateChanged;
                        _start.ColorChanged -= OnStartColorChanged;
                    }

                    _start = value;

                    if (_start != null)
                    {
                        _start.XDateChanged += OnStartXDateChanged;
                        _start.ColorChanged += OnStartColorChanged;
                    }

                    StartPositionChanged?.Invoke(this, value);
                }
            }
        }

        public TimeRangeLine End
        {
            get { return _end; }
            set
            {
                if (_end != value)
                {
                    if (_end != null)
                    {
                        _end.XDateChanged -= OnEndXDateChanged;
                        _end.ColorChanged -= OnEndColorChanged;
                    }

                    _end = value;

                    if (_end != null)
                    {
                        _end.XDateChanged += OnEndXDateChanged;
                        _end.ColorChanged += OnEndColorChanged;
                    }

                    EndPositionChanged?.Invoke(this, value);
                }
            }
        }

        public event Action<TimeRange, TimeRangeLine> StartPositionChanged;
        public event Action<TimeRange, TimeRangeLine> EndPositionChanged;

        private void OnStartXDateChanged(object sender, EventArgs eventArgs)
        {
            StartPositionChanged?.Invoke(this, Start);
        }

        private void OnStartColorChanged(object sender, EventArgs eventArgs)
        {
            // Handle start color change
        }

        private void OnEndXDateChanged(object sender, EventArgs eventArgs)
        {
            EndPositionChanged?.Invoke(this, End);
        }

        private void OnEndColorChanged(object sender, EventArgs eventArgs)
        {
            // Handle end color change
        }
    }


    public class UITimeRangeLine
    {


        public TimeRange Range { get; set; }
        private LiveChartLine startLine;

        public LiveChartLine StartLine
        {
            get { return startLine; }
            set
            {
                startLine = value;
                startLine.Moving += StartLine_Moving;
            }
        }

        private void StartLine_Moving(LiveChartLine arg1, double arg2)
        {
            Range.Start.XDate = DateTime.FromOADate(arg2);
        }

        private LiveChartLine endLine;

        public LiveChartLine EndLine
        {
            get { return endLine; }
            set
            {
                endLine = value;
                endLine.Moving += EndLine_Moving; ;
            }
        }

        private void EndLine_Moving(LiveChartLine arg1, double arg2)
        {
            Range.End.XDate = DateTime.FromOADate(arg2);
        }
    }


    public class TimeRangeLine
    {
        private DateTime xdate;
        private Color? color;

        public TimeRangeLine()
        {

        }

        public TimeRangeLine(DateTime start, Color? color = null)
        {
            XDate = start;
            Color = color;
        }
        public DateTime XDate
        {
            get { return xdate; }
            set { xdate = value; RaiseXDateChangedEvent(); }
        }

        public event EventHandler XDateChanged;

        public Color? Color
        {
            get { return color; }
            set { color = value; RaiseColorChangedEvent(); }
        }

        public event EventHandler ColorChanged;

        private void RaiseXDateChangedEvent()
        {
            if (XDateChanged != null)
            {
                XDateChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseColorChangedEvent()
        {
            if (ColorChanged != null)
            {
                ColorChanged(this, EventArgs.Empty);
            }
        }
    }

    public class UIResolutionChart
    {


        public ResolutionChart Resolution { get; set; }

        private OzAxis xAxis;

        public OzAxis XAxis
        {
            get { return xAxis; }
            set
            {
                xAxis = value;
                xAxis.Changed += XAxis_Changed;
            }
        }

        private void XAxis_Changed(OzAxis arg1, double? minLimit, double? maxLimit)
        {
            Resolution.XAxisResolution.SetLimits(minLimit, maxLimit);
        }

        private OzAxis yAxis;

        public OzAxis YAxis
        {
            get { return yAxis; }
            set
            {
                yAxis = value;
                yAxis.Changed += YAxis_Changed;
            }
        }

        private void YAxis_Changed(OzAxis arg1, double? minLimit, double? maxLimit)
        {
            Resolution.YAxisResolution.SetLimits(minLimit, maxLimit);
        }
    }

    public class ResolutionChart
    {
        public ResolutionChart(double? minX, double? maxX, double? minY, double? maxY, Func<double, string>? labelerX, Func<double, string>? labelerY)
        {
            XAxisResolution = new AxisResolution(minX, maxX, labelerX);
            YAxisResolution = new AxisResolution(minY, maxY, labelerY);
        }

        private AxisResolution _xAxisResolution;
        private AxisResolution _yAxisResolution;


        public AxisResolution XAxisResolution
        {
            get { return _xAxisResolution; }
            set
            {
                if (_xAxisResolution != value)
                {
                    if (_xAxisResolution != null)
                    {
                        _xAxisResolution.MinLimitChanged -= OnXAxisResolutionChanged;
                    }

                    _xAxisResolution = value;

                    if (_xAxisResolution != null)
                    {
                        _xAxisResolution.MinLimitChanged += OnXAxisResolutionChanged;

                    }

                    XAxisResolutionChanged?.Invoke(this, value);
                }
            }
        }

        public AxisResolution YAxisResolution
        {
            get { return _yAxisResolution; }
            set
            {
                if (_yAxisResolution != value)
                {
                    if (_yAxisResolution != null)
                    {
                        _yAxisResolution.MinLimitChanged -= OnYAxisResolutionChanged;
                    }

                    _yAxisResolution = value;

                    if (_yAxisResolution != null)
                    {
                        _yAxisResolution.MinLimitChanged += OnYAxisResolutionChanged;
                    }

                    YAxisResolutionChanged?.Invoke(this, value);
                }
            }
        }

        public event Action<ResolutionChart, AxisResolution> XAxisResolutionChanged;
        public event Action<ResolutionChart, AxisResolution> YAxisResolutionChanged;

        private void OnXAxisResolutionChanged(object sender, EventArgs eventArgs)
        {
            XAxisResolutionChanged?.Invoke(this, XAxisResolution);
        }

        private void OnYAxisResolutionChanged(object sender, EventArgs eventArgs)
        {
            YAxisResolutionChanged?.Invoke(this, YAxisResolution);
        }
    }
    public class AxisResolution
    {

        private Func<double, string>? labeler;

        public Func<double, string>? Labeler 
        {
            get { return labeler; }
            set { labeler = value; }
        }


        private double? _minLimit;

        private double? _maxLimit;

        public AxisResolution()
        {

        }

        public AxisResolution(double? minLimit, double? maxLimit, Func<double, string>? labeler)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
            Labeler = labeler;  
        }

        public double? MinLimit
        {
            get
            {
                return _minLimit;
            }

            set { _minLimit = value; RaiseMinLimitChangedEvent(); }
        }

        public event EventHandler MinLimitChanged;

        public double? MaxLimit
        {
            get
            {
                return _maxLimit;
            }
            set { _maxLimit = value; RaiseMaxLimitChangedEvent(); }
        }

        public event EventHandler MaxLimitChanged;

        private void RaiseMinLimitChangedEvent()
        {
            if (MinLimitChanged != null)
            {
                MinLimitChanged(this, EventArgs.Empty);
            }
        }

        private void RaiseMaxLimitChangedEvent()
        {
            if (MaxLimitChanged != null)
            {
                MaxLimitChanged(this, EventArgs.Empty);
            }
        }


        public void SetLimits(double? min, double? max)
        {
            MinLimit = min;
            MaxLimit = max;
        }

    }
}
