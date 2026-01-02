using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Markup;
using System.Reflection;
using System.Reflection.Metadata;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HangulClock;

public class TimeModel
{
    public string Label { get; set; }
    public Point[] Tiles { get; set; }

    public TimeModel(string label, Point[] tiles)
    {
        Label = label;
        Tiles = tiles;
    }

    internal static TimeModel Make(string label, string[] points)
    {
        PointCollection pc = new PointCollection();
        foreach (var s in points)
        {
            Point pnt;
            Point.TryParse(s, out pnt);
            pc.Add(pnt);
        }

        return new TimeModel(label, pc.ToArray<Point>());
    }
}

public class MainPage : ContentPage
{
    Grid tileGrid;

    readonly TimeModel[] HoursMap = {
        new TimeModel("열두", new Point[]{ new Point(2,2), new Point(4,2) }),
        new TimeModel("한", new Point[]{ new Point(0,0) }),
        new TimeModel("두", new Point[]{ new Point(1,0) }),
        new TimeModel("세", new Point[]{ new Point(2,0) }),
        new TimeModel("네", new Point[]{ new Point(3,0) }),
        new TimeModel("다섯", new Point[]{ new Point(4,0), new Point(5,0) }),
        new TimeModel("여섯", new Point[]{ new Point(0,1), new Point(1,1) }),
        new TimeModel("일곱", new Point[]{ new Point(2,1), new Point(3,1) }),
        new TimeModel("여덟", new Point[]{ new Point(4,1), new Point(5,1) }),
        new TimeModel("아홉", new Point[]{ new Point(0,2), new Point(1,2) }),
        new TimeModel("열", new Point[]{ new Point(2,2) }),
        new TimeModel("열한아", new Point[]{ new Point(2,2), new Point(3,2) })
    };

    readonly TimeModel[] MinutesMap = {
        TimeModel.Make("정", new string[]{"0,4"}),
        TimeModel.Make("일", new string[]{"1,4"}),
        TimeModel.Make("이", new string[]{"2,4"}),
        TimeModel.Make("삼", new string[]{"3,4"}),
        TimeModel.Make("사", new string[]{"4,4"}),
        TimeModel.Make("오", new string[]{"0,5"}),
        TimeModel.Make("육", new string[]{"5,4"}),
        TimeModel.Make("칠", new string[]{"2,5"}),
        TimeModel.Make("팔", new string[]{"3,5"}),
        TimeModel.Make("구", new string[]{"4,5"}),
        TimeModel.Make("십", new string[]{"5,3"}),

        TimeModel.Make("십일", new string[]{"5,3", "1,4"}),
        TimeModel.Make("십이", new string[]{"5,3", "2,4"}),
        TimeModel.Make("십삼", new string[]{"5,3", "3,4"}),
        TimeModel.Make("십사", new string[]{"5,3", "4,4"}),
        TimeModel.Make("십오", new string[]{"5,3", "5,4"}),
        TimeModel.Make("십육", new string[]{"5,3", "1,5"}),
        TimeModel.Make("십칠", new string[]{"5,3", "2,5"}),
        TimeModel.Make("십팔", new string[]{"5,3", "3,5"}),
        TimeModel.Make("십구", new string[]{"5,3", "4,5"}),

        //    "이십", "이십일", "이십이", "이십삼", "이십사", "이십오", "이십육", "이십칠", "이십팔", "이십구" ,
        TimeModel.Make("이십", new string[]{"1,3", "5,3"}),
        TimeModel.Make("이십일", new string[]{"1,3", "5,3", "1,4"}),
        TimeModel.Make("이십이", new string[]{"1,3", "5,3", "2,4"}),
        TimeModel.Make("이십삼", new string[]{"1,3", "5,3", "3,4"}),
        TimeModel.Make("이십사", new string[]{"1,3", "5,3", "4,4"}),
        TimeModel.Make("이십오", new string[]{"1,3", "5,3", "5,4"}),
        TimeModel.Make("이십육", new string[]{"1,3", "5,3", "1,5"}),
        TimeModel.Make("이십칠", new string[]{"1,3", "5,3", "2,5"}),
        TimeModel.Make("이십팔", new string[]{"1,3", "5,3", "3,5"}),
        TimeModel.Make("이십구", new string[]{"1,3", "5,3", "4,5"}),

        //    "삼십", "삼십일", "삼십이", "삼십삼", "삼십사", "삼십오", "삼십육", "삼십칠", "삼십팔", "삼십구",
        TimeModel.Make("삼십", new string[]{"2,3", "5,3"}),
        TimeModel.Make("삼십일", new string[]{"2,3", "5,3", "1,4"}),
        TimeModel.Make("삼십이", new string[]{"2,3", "5,3", "2,4"}),
        TimeModel.Make("삼십삼", new string[]{"2,3", "5,3", "3,4"}),
        TimeModel.Make("삼십사", new string[]{"2,3", "5,3", "4,4"}),
        TimeModel.Make("삼십오", new string[]{"2,3", "5,3", "5,4"}),
        TimeModel.Make("삼십육", new string[]{"2,3", "5,3", "1,5"}),
        TimeModel.Make("삼십칠", new string[]{"2,3", "5,3", "2,5"}),
        TimeModel.Make("삼십팔", new string[]{"2,3", "5,3", "3,5"}),
        TimeModel.Make("삼십구", new string[]{"2,3", "5,3", "4,5"}),

        //    "사십", "사십일", "사십이", "사십삼", "사십사", "사십오", "사십육", "사십칠", "사십팔", "사십구",
        TimeModel.Make("사십", new string[]{"3,3", "5,3"}),
        TimeModel.Make("사십일", new string[]{"3,3", "5,3", "1,4"}),
        TimeModel.Make("사십이", new string[]{"3,3", "5,3", "2,4"}),
        TimeModel.Make("사십삼", new string[]{"3,3", "5,3", "3,4"}),
        TimeModel.Make("사십사", new string[]{"3,3", "5,3", "4,4"}),
        TimeModel.Make("사십오", new string[]{"3,3", "5,3", "5,4"}),
        TimeModel.Make("사십육", new string[]{"3,3", "5,3", "1,5"}),
        TimeModel.Make("사십칠", new string[]{"3,3", "5,3", "2,5"}),
        TimeModel.Make("사십팔", new string[]{"3,3", "5,3", "3,5"}),
        TimeModel.Make("사십구", new string[]{"3,3", "5,3", "4,5"}),

        //    "오십", "오십일", "오십이", "오십삼", "오십사", "오십오", "오십육", "오십칠", "오십팔", "오십구"
        TimeModel.Make("오십", new string[]{"4,3", "5,3"}),
        TimeModel.Make("오십일", new string[]{"4,3", "5,3", "1,4"}),
        TimeModel.Make("오십이", new string[]{"4,3", "5,3", "2,4"}),
        TimeModel.Make("오십삼", new string[]{"4,3", "5,3", "3,4"}),
        TimeModel.Make("오십사", new string[]{"4,3", "5,3", "4,4"}),
        TimeModel.Make("오십오", new string[]{"4,3", "5,3", "5,4"}),
        TimeModel.Make("오십육", new string[]{"4,3", "5,3", "1,5"}),
        TimeModel.Make("오십칠", new string[]{"4,3", "5,3", "2,5"}),
        TimeModel.Make("오십팔", new string[]{"4,3", "5,3", "3,5"}),
        TimeModel.Make("오십구", new string[]{"4,3", "5,3", "4,5"}),

    };



    private System.Timers.Timer _timer;

    private double _windowMargin = (DeviceInfo.Idiom == DeviceIdiom.Phone) ? 15 : 30;
    private Label pageTitle;

    enum Row { Title, First, Second, Third, Fourth, Fifth, Sixth }
    enum Column { First, Second, Third, Fourth, Fifth, Sixth }
    void Build() => Content =
        new Grid
        {
            BackgroundColor = Color.FromArgb("#101010"),
            Children = {
            (tileGrid = new Grid {
                RowDefinitions = Rows.Define (
                    (Row.Title, Auto),
                    (Row.First  , Star),
                    (Row.Second , Star),
                    (Row.Third  , Star),
                    (Row.Fourth , Star),
                    (Row.Fifth  , Star),
                    (Row.Sixth  , Star)
                ),

                ColumnDefinitions = Columns.Define (
                    (Column.First   , Star),
                    (Column.Second , Star),
                    (Column.Third  , Star),
                    (Column.Fourth , Star),
                    (Column.Fifth   , Star),
                    (Column.Sixth   , Star)
                ),

                Children = {
					// Row 1
					new TitleLabel{ Text = "한"}.Row(Row.First),
                    new TitleLabel{ Text = "두"}.Row(Row.First).Column(Column.Second),
                    new TitleLabel{ Text = "세"}.Row(Row.First).Column(Column.Third),
                    new TitleLabel{ Text = "네"}.Row(Row.First).Column(Column.Fourth),
                    new TitleLabel{ Text = "다"}.Row(Row.First).Column(Column.Fifth),
                    new TitleLabel{ Text = "섯"}.Row(Row.First).Column(Column.Sixth),

					// Row 2
					new TitleLabel{ Text = "여"}.Row(Row.Second),
                    new TitleLabel{ Text = "섯"}.Row(Row.Second).Column(Column.Second),
                    new TitleLabel{ Text = "일"}.Row(Row.Second).Column(Column.Third),
                    new TitleLabel{ Text = "곱"}.Row(Row.Second).Column(Column.Fourth),
                    new TitleLabel{ Text = "여"}.Row(Row.Second).Column(Column.Fifth),
                    new TitleLabel{ Text = "덟"}.Row(Row.Second).Column(Column.Sixth),

					// Row 3
					new TitleLabel{ Text = "아"}.Row(Row.Third),
                    new TitleLabel{ Text = "홉"}.Row(Row.Third).Column(Column.Second),
                    new TitleLabel{ Text = "열"}.Row(Row.Third).Column(Column.Third),
                    new TitleLabel{ Text = "한"}.Row(Row.Third).Column(Column.Fourth),
                    new TitleLabel{ Text = "두"}.Row(Row.Third).Column(Column.Fifth),
                    new TitleLabel{ Text = "시"}.Row(Row.Third).Column(Column.Sixth),

					// Row 4
					new TitleLabel{ Text = "자"}.Row(Row.Fourth),
                    new TitleLabel{ Text = "이"}.Row(Row.Fourth).Column(Column.Second),
                    new TitleLabel{ Text = "삼"}.Row(Row.Fourth).Column(Column.Third),
                    new TitleLabel{ Text = "사"}.Row(Row.Fourth).Column(Column.Fourth),
                    new TitleLabel{ Text = "오"}.Row(Row.Fourth).Column(Column.Fifth),
                    new TitleLabel{ Text = "십"}.Row(Row.Fourth).Column(Column.Sixth),

					// Row 5
					new TitleLabel{ Text = "정"}.Row(Row.Fifth),
                    new TitleLabel{ Text = "일"}.Row(Row.Fifth).Column(Column.Second),
                    new TitleLabel{ Text = "이"}.Row(Row.Fifth).Column(Column.Third),
                    new TitleLabel{ Text = "삼"}.Row(Row.Fifth).Column(Column.Fourth),
                    new TitleLabel{ Text = "사"}.Row(Row.Fifth).Column(Column.Fifth),
                    new TitleLabel{ Text = "육"}.Row(Row.Fifth).Column(Column.Sixth),

					// Row 6
					new TitleLabel{ Text = "오"}.Row(Row.Sixth),
                    new TitleLabel{ Text = "오"}.Row(Row.Sixth).Column(Column.Second),
                    new TitleLabel{ Text = "칠"}.Row(Row.Sixth).Column(Column.Third),
                    new TitleLabel{ Text = "팔"}.Row(Row.Sixth).Column(Column.Fourth),
                    new TitleLabel{ Text = "구"}.Row(Row.Sixth).Column(Column.Fifth),
                    new TitleLabel{ Text = "분"}.Row(Row.Sixth).Column(Column.Sixth),

                    // title
                    new Label{Text = "지금 몇 시예요?", TextColor = Colors.White, FontSize = 44, HorizontalTextAlignment = TextAlignment.Center }
                        .Row(Row.Title).ColumnSpan(6)
                        .Center()
                        .Assign(out pageTitle)

                }
            })
            .Margins(top: _windowMargin, bottom: _windowMargin, left: _windowMargin, right: _windowMargin)
            //.CenterHorizontal().CenterVertical()
        }
        };

    class TitleLabel : Label
    {
        public TitleLabel()
        {
            FontSize = (DeviceInfo.Idiom == DeviceIdiom.Phone) ? 32 : 64;
            TextColor = Colors.White;
            Opacity = 0.4;

            VerticalTextAlignment = TextAlignment.Center;
            HorizontalTextAlignment = TextAlignment.Center;

            Padding = new Thickness(10, 10);
        }
    }

    public MainPage()
    {
        this.Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = Color.FromRgba("#000000"),
            StatusBarStyle = StatusBarStyle.DarkContent
        });

        Build();

        _timer = new System.Timers.Timer()
        { Interval = 1000, Enabled = true };
        _timer.Elapsed += timer_Handler;

#if DEBUG
        HotReloadService.UpdateApplicationEvent += (obj) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Build();
            });

        };
#endif
    }

    private void timer_Handler(object sender, System.Timers.ElapsedEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            ResetGrid();
            UpdateGrid();
        });

    }

    private void UpdateGrid()
    {
        var d = DateTime.Now;

        var hourModel = HoursMap[d.Hour % 12];
        int cnt = tileGrid.Children.Where(c =>
        {
            foreach (var t in hourModel.Tiles)
            {
                // Add 1 to t.Y to account for Title row at row 0
                if ((tileGrid.GetRow(c) == t.Y + 1 && tileGrid.GetColumn(c) == t.X) || ((Label)c).Text == "시")
                {
                    ((Label)c).Opacity = 1;
                    ((Label)c).TextColor = Colors.LawnGreen;
                    return true;
                }

            }
            return false;
        }).Count();

        var minuteModel = MinutesMap[d.Minute];
        int cnt2 = tileGrid.Children.Where(c =>
        {
            foreach (var t in minuteModel.Tiles)
            {
                // Add 1 to t.Y to account for Title row at row 0
                if ((tileGrid.GetRow(c) == t.Y + 1 && tileGrid.GetColumn(c) == t.X) || ((Label)c).Text == "분")
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
        foreach (var l in tileGrid.Children)
        {
            ((Label)l).Opacity = 0.4;
            ((Label)l).TextColor = Colors.Black;
        }

        pageTitle.Opacity = 1;
    }
}