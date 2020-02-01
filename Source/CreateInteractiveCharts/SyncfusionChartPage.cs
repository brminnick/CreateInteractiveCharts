using Xamarin.Forms;

namespace CreateInteractiveCharts
{
    class SyncfusionChartPage : ContentPage
    {
        public SyncfusionChartPage()
        {
            Title = "Syncfusion Area Series Chart";

            Padding = new Thickness(0, 10, 0, 0);

            Content = new AreaSeriesChart();
        }
    }
}
