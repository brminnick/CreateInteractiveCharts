using Xamarin.Forms;

namespace CreateInteractiveCharts
{
public class App : Application
{
    public App() => MainPage = new NavigationPage(new SyncfusionChartPage());
}
}
