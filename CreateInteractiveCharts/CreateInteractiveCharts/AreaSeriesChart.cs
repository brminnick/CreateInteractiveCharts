using System;
using System.Collections.Generic;
using Syncfusion.SfChart.XForms;

namespace CreateInteractiveCharts
{
    public class AreaSeriesChart : SfChart
    {
        public AreaSeriesChart()
        {
            const int numberOfDays = 20;
            const int maxValue = 100;

            var areaSeries = new AreaSeries
            {
                Opacity = 0.9,
                Label = nameof(ChartDataModel),
                ItemsSource = GenerateData(numberOfDays, maxValue),
                XBindingPath = nameof(ChartDataModel.Date),
                YBindingPath = nameof(ChartDataModel.Value)
            };

            Series = new ChartSeriesCollection { areaSeries };

            PrimaryAxis = new DateTimeAxis
            {
                IntervalType = DateTimeIntervalType.Days,
                Interval = 1,
                RangePadding = DateTimeRangePadding.Round,
                Minimum = DateTime.Now.Subtract(TimeSpan.FromDays(numberOfDays - 1)),
                Maximum = DateTime.Now
            };

            SecondaryAxis = new NumericalAxis
            {
                Minimum = 0,
                Maximum = maxValue
            };
        }

        static IEnumerable<ChartDataModel> GenerateData(int count = 10, int maxValue = 100)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < count; i++)
            {
                yield return new ChartDataModel
                {
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(i)),
                    Value = random.Next(maxValue)
                };
            }
        }
    }
}
