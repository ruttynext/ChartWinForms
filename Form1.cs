using ChartWinForms.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartWinForms
{
    public partial class Form1 : Form
    {
        public ChartVM vm = new();
        public Form1()
        {
            InitializeComponent();
            var timeRange = new TimeRange("", DateTime.Now, DateTime.Now.AddSeconds(30));
            //DataContext = vm;

            vm.Ranges = new ObservableCollection<TimeRange>() { timeRange };
            OzAxis xAxes = new OzAxis
            {
                TextSize = 12,
                Labeler = value => Formatter(value)
            };


            var resolutionChart = new ResolutionChart(null, null, null, null, value => Formatter(value), null);
            chartTest1.DataSource = vm;
            chartTest1.ResolutionChart = resolutionChart;
            chartTest1.Series = vm.SeriesCollection;

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
