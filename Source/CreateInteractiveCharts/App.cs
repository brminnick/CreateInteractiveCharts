using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CreateInteractiveCharts
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            var navigationPage = new Xamarin.Forms.NavigationPage(new SyncfusionChartPage());
            navigationPage.On<iOS>().SetPrefersLargeTitles(true);

            MainPage = navigationPage;
        }
    }
}
