# Hangul Clock

![Mac desktop clock](images/catalyst.png)

This is a Korean word clock inspired by several craft projects including:

* https://github.com/dsa28s/windows-hangul-clock
* https://hangulclock.today/
* https://www.instructables.com/Korean-Clock-HA-Yong-Son/

I built this digital clock with [.NET MAUI](https://github.com/dotnet/maui) to render native apps for Android, iOS, macOS, and Windows. 

## Maui.Markup

.NET MAUI traditionally favors writing UI in XAML, an XML based markup language that provides separation between UI and logic. By using the .NET MAUI Community Toolkit's [Maui.Markup](https://github.com/CommunityToolkit/Maui.Markup) helpers, we can remove the complexity of XAML and stick with C# alone. 

```csharp
new Grid { 
    BackgroundColor = Color.FromHex("#101010"),
    Children = {
        (tileGrid = new Grid {
            RowDefinitions = Rows.Define (
                (Row.First	, Star),
                (Row.Second , Star),
                (Row.Third  , Star),
                (Row.Fourth , Star),
                (Row.Fifth	, Star),
                (Row.Sixth	, Star)
            ),
    
            ColumnDefinitions = Columns.Define (
                (Column.First	, Star),
                (Column.Second , Star),
                (Column.Third  , Star),
                (Column.Fourth , Star),
                (Column.Fifth	, Star),
                (Column.Sixth	, Star)
            ),
    
            Children = {
                // Row 1
                new TitleLabel{ Text = "한"}.Row(Row.First),
                new TitleLabel{ Text = "두"}.Row(Row.First).Column(Column.Second),
                new TitleLabel{ Text = "세"}.Row(Row.First).Column(Column.Third),
                new TitleLabel{ Text = "네"}.Row(Row.First).Column(Column.Fourth),
                new TitleLabel{ Text = "다"}.Row(Row.First).Column(Column.Fifth),
                new TitleLabel{ Text = "섯"}.Row(Row.First).Column(Column.Sixth),
            ...
            }
        })
    }
}
```

.NET MAUI uses Model-View-ViewModel (MVVM) with databinding, or event driven imperative state updates. In this example I'm using imperative. With each timer tick, I reset the grid and update the views based on preset models.

```csharp
private void timer_Handler(object sender, System.Timers.ElapsedEventArgs e)
{
    ResetGrid();
    UpdateGrid();
}

private void UpdateGrid()
{
    var d = DateTime.Now;

    var hourModel = HoursMap[d.Hour % 12];
    int cnt = tileGrid.Children.Where(c => {
        foreach(var t in hourModel.Tiles)
        {
            if((tileGrid.GetRow(c) == t.Y && tileGrid.GetColumn(c) == t.X) || ((Label)c).Text == "시")
            {
                ((Label)c).Opacity = 1;
                ((Label)c).TextColor = Colors.LawnGreen;
                return true;
            }
                
        }
        return false;
    }).Count();

    var minuteModel = MinutesMap[d.Minute];
    int cnt2 = tileGrid.Children.Where(c => {
        foreach (var t in minuteModel.Tiles)
        {
            if ((tileGrid.GetRow(c) == t.Y && tileGrid.GetColumn(c) == t.X) || ((Label)c).Text == "분")
            {
                ((Label)c).Opacity = 1;
                ((Label)c).TextColor = Colors.OrangeRed;
                return true;
            }

        }
        return false;
    }).Count();
}

private void ResetGrid()
{
    foreach(var l in tileGrid.Children)
    {
        ((Label)l).Opacity = 0.2;
        ((Label)l).TextColor = Colors.White;
    }
}
```


![Windows Android](images/windroid.png)
