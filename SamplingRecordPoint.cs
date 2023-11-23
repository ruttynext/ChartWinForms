

using LiveChartsCore.Kernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChartWinForms
{

    public class SamplingRecordPoint : IChartEntity, INotifyPropertyChanged
    {
        private double _time;
        private double? _value;

        public SamplingRecordPoint()
        {
        }

        public SamplingRecordPoint(double time, double? value)
        {
            Time = time;
            Value = value;
            Coordinate = value is null ? Coordinate.Empty : new(time, value.Value);
        }

        public double Time { get => _time; set { _time = value; OnPropertyChanged(); } }

        public double? Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        /// <inheritdoc cref="IChartEntity.MetaData"/>
        #if NET5_0_OR_GREATER
        [System.Text.Json.Serialization.JsonIgnore]
        #else
      [Newtonsoft.Json.JsonIgnore]
        #endif
        public ChartEntityMetaData? MetaData { get; set; }

        /// <inheritdoc cref="IChartEntity.Coordinate"/>
        #if NET5_0_OR_GREATER
        [System.Text.Json.Serialization.JsonIgnore]
        #else
    [Newtonsoft.Json.JsonIgnore]
        #endif
        public Coordinate Coordinate { get; set; } = Coordinate.Empty;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Called when a property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            Coordinate = _value is null ? Coordinate.Empty : new(_time, _value.Value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
