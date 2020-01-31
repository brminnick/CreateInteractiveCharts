# Xamarin.Forms: Easily Creating Interactive Charts

Displaing data 

| Before | After |
| ------ | ----- |
| <img src="https://user-images.githubusercontent.com/13558917/73576899-6f9e8480-4430-11ea-9c61-810d14e51b50.png" height="500"/> | <img src="https://user-images.githubusercontent.com/13558917/73576092-83e18200-442e-11ea-9b44-05e3ed72fae9.gif" height="500"/> |


## 0. Getting Started

### Install Syncfusion NuGet Packages

- Install [Syncfusion.Xamarin.SfChart NuGet Package](https://www.nuget.org/packages/Syncfusion.Xamarin.SfChart/) into each project, e.g. .NET Standard Project, Xamarin.iOS Project and Xamarin.Android project

### Initialize Syncfusion Charts, iOS

In `AppDelegate.cs`, in the `FinishedLaunching` method, add `Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();` after `Xamarin.Forms.Forms.Init();`.

Here is an example:
```cs
public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        Xamarin.Forms.Forms.Init();
        Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();

        LoadApplication(new App());

        return base.FinishedLaunching(app, options);
    }
}
```

### Create Data Model for Chart

Let's create the model we'll use for our chart data:

```csharp
class ChartDataModel
{
    public DateTime Date { get; set; }
    public int Value { get; set; }
}
```

## 1. Create a Custom Chart

Let's start by creating a new class called `AreaSeriesChart`:

```csharp
class AreaSeriesChart : SfChart
{
    public AreaSeriesChart()
    {
    }
}
```

In this class, let's create a static method to generate data called `IEnumerable<ChartDataModel> GenerateData()`:

```csharp
class AreaSeriesChart : SfChart
{
    public AreaSeriesChart()
    {
    }

    static IEnumerable<ChartDataModel> GenerateData(int count, int maxValue)
    {
        var random = new Random();

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
```

Next, let's create an `AreaSeries` that will display our data:

```csharp
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
}
```

Lastly, let's define our `PrimaryAxis` and `SecondaryAxis`:

```csharp
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
```

## 2. Display Chart in App

First, we'll create a new class called `SyncfusionChartPage` which will inherit from `Xamarin.Forms.ContentPage` to display our new chart:

```csharp
class SyncfusionChartPage : ContentPage
{
    public SyncfusionChartPage()
    {
        Title = "Syncfusion Area Series Chart";

        Padding = new Thickness(0, 10, 0, 0);

        Content = new AreaSeriesChart();
    }
}
```

And lastly, in `App.cs`, we'll assign `MainPage` to be our newly created `SyncfusionChartPage`:

```csharp
public class App : Application
{
    public App() 
    {
        MainPage = new NavigationPage(new SyncfusionChartPage());
    }
}
```

Great! Now we have a chart in our app that displays our data:

<img src="https://user-images.githubusercontent.com/13558917/73576237-db7fed80-442e-11ea-8a37-ea850dd45488.png" height="250"/>

## 3. Make the Chart Interactive

First, let's add the ability to see the exact value of the data by adding `ChartTrackballBehavior`

```csharp
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

    ChartBehaviors = new ChartBehaviorCollection
    {
        new ChartZoomPanBehavior(),
        new ChartTrackballBehavior()
    };
}
```

Next, let's add the ability to pan and zoom by adding `ChartZoomPanBehavior`:

```csharp
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

    ChartBehaviors = new ChartBehaviorCollection
    {
        new ChartZoomPanBehavior(),
        new ChartTrackballBehavior()
    };
}
```

And now we have an interactive chart!

![Interactive Chart](https://user-images.githubusercontent.com/13558917/73576092-83e18200-442e-11ea-9b44-05e3ed72fae9.gif)