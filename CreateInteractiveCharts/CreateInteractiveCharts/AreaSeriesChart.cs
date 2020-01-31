using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.SfChart.XForms;

namespace CreateInteractiveCharts
{
    public class AreaSeriesChart : SfChart
    {
        public AreaSeriesChart()
        {
            const int numberOfDays = 20;
            const int maxValue = 100;

            var dataSource = GenerateData(numberOfDays, maxValue).ToList();

            var areaSeries = new AreaSeries
            {
                Opacity = 0.9,
                Label = nameof(ChartDataModel),
                ItemsSource = dataSource,
                XBindingPath = nameof(ChartDataModel.Date),
                YBindingPath = nameof(ChartDataModel.Value)
            };

            Series = new ChartSeriesCollection { areaSeries };

            PrimaryAxis = new DateTimeAxis
            {
                IntervalType = DateTimeIntervalType.Days,
                Interval = 1,
                RangePadding = DateTimeRangePadding.Round,
                Minimum = dataSource.Min(x => x.Date),
                Maximum = dataSource.Max(x => x.Date)
            };

            SecondaryAxis = new NumericalAxis
            {
                Minimum = 0,
                Maximum = maxValue
            };

            ChartBehaviors = new ChartBehaviorCollection
            {
                new ChartZoomPanBehavior(),
                new ChartTrackballBehavior()
            };
        }

        static IEnumerable<ChartDataModel> GenerateData(int count, int maxValue)
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
