using ChartWinForms.Chart;
using CommunityToolkit.Mvvm.ComponentModel;
using Eco.ObjectRepresentation;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartWinForms
{
    public class ChartVM : ObservableObject
    {

        private ObservableCollection<TimeRange> ranges;

        public ObservableCollection<TimeRange> Ranges
        {
            get { return ranges; }
       
            set
            {
                ranges = value;
                OnPropertyChanged(nameof(Ranges));
            }
        }

        public ISeries[] SeriesCollection { get; set; }

        private Axis xAxes { get; set; }
        public Axis XAxes
        {
            get => xAxes;
            set
            {
                xAxes = value;
                OnPropertyChanged(nameof(XAxes));
            }
        }

        private Axis yAxes { get; set; }
        public Axis YAxis
        {
            get => yAxes;
            set
            {
                yAxes = value;
                OnPropertyChanged(nameof(YAxis));
            }
        }


        private int minY;
        public int MinY
        {
            get
            {
                return minY;
            }
            set
            {
                minY = value;
                OnPropertyChanged(nameof(MinY));
            }

        }


        private bool displayCalibrationTimeInReport;
        public bool DisplayCalibrationTimeInReport
        {
            get => displayCalibrationTimeInReport;
            set
            {
                displayCalibrationTimeInReport = value;
                OnPropertyChanged(nameof(DisplayCalibrationTimeInReport));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private bool autoCalculationCalibrationTime;
        public bool AutoCalculationCalibrationTime
        {
            get => autoCalculationCalibrationTime;
            set
            {
                autoCalculationCalibrationTime = value;
                OnPropertyChanged(nameof(AutoCalculationCalibrationTime));
            }
        }

        private double start;
        public double Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                OnPropertyChanged(nameof(Start));
            }
        }


        private DateTime end;
        public DateTime End
        {
            get => end;
            set
            {
                end = value;
                OnPropertyChanged(nameof(End));
            }
        }

        private TimeSpan calibrationTimeCalculation;
        public TimeSpan CalibrationTimeCalculation
        {
            get => calibrationTimeCalculation;
            set
            {
                calibrationTimeCalculation = value;
                OnPropertyChanged(nameof(CalibrationTimeCalculation));
            }
        }

        private double? stabilizationLine;
        public double? StabilizationLine
        {
            get => stabilizationLine;
            set
            {
                stabilizationLine = value;
                OnPropertyChanged(nameof(StabilizationLine));
            }
        }

        private double overshoot;
        public double Overshoot
        {
            get => overshoot;
            set
            {
                overshoot = value;
                OnPropertyChanged(nameof(Overshoot));
            }
        }

        private double undershoot;
        public double Undershoot
        {
            get => undershoot;
            set
            {
                undershoot = value;
                OnPropertyChanged(nameof(Undershoot));
            }
        }

        private TimeSpan stabilizationLineCalculation;
        public TimeSpan StabilizationLineCalculation
        {
            get => stabilizationLineCalculation;
            set
            {
                stabilizationLineCalculation = value;
                OnPropertyChanged(nameof(StabilizationLineCalculation));
            }
        }

        private bool autoCalculationStabilizationLine;
        public bool AutoCalculationStabilizationLine
        {
            get => autoCalculationStabilizationLine;
            set
            {
                autoCalculationStabilizationLine = value;
                OnPropertyChanged(nameof(AutoCalculationStabilizationLine));
            }
        }

        private bool displayOvershoot;
        public bool DisplayOvershoot
        {
            get => displayOvershoot;
            set
            {
                displayOvershoot = value;
                OnPropertyChanged(nameof(DisplayOvershoot));
            }
        }

        private bool displayUndershoot;
        public bool DisplayUndershoot
        {
            get => displayUndershoot;
            set
            {
                displayUndershoot = value;
                OnPropertyChanged(nameof(DisplayUndershoot));
            }
        }


        #region ChartVM constructor
        public ChartVM()
        {

            xAxes = new Axis
            {
                TextSize = 12,
                Labeler = value => Formatter(value)
            };


            yAxes = new Axis
            {
                MaxLimit = 10,
                MinLimit = 0
            };

            SeriesCollection = new ISeries[]
            {
                new LineSeries<SamplingRecordPoint>
                {
                    Values = GetValues(),
                    Fill = null,
                    GeometrySize = 0,
                    Stroke = new SolidColorPaint(new(229, 150, 53), 1),
                }
            };

            Start = DateTime.Now.ToOADate();


        }
        #endregion


        public ObservableCollection<SamplingRecordPoint> GetValues()
        {
            Random rand = new Random();

            var values = new ObservableCollection<SamplingRecordPoint>();
            for (int i = 0; i < 1000; i++)
            {
                double? newValue = rand.Next(1, 8);
                values.Add(new SamplingRecordPoint(DateTime.Now.AddSeconds(i + 2).ToOADate(), newValue));
            }

            return values;
        }


        private static string Formatter(double samplingDate)
        {
            DateTime date = DateTime.FromOADate(samplingDate);


            //DateTime firstTime = DateTime.Now.AddMinutes(-50);
            //var secsAgo = (date - firstTime).TotalSeconds;

            return date.ToString("HH:mm:ss");// + Environment.NewLine + $"{secsAgo:N0}s ago";

        }
    }
}
