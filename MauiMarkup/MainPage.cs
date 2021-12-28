using CommunityToolkit.Maui.Markup;
using System.Reflection.Metadata;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

[assembly: MetadataUpdateHandler(typeof(HangulClock.MainPage))]
namespace HangulClock;

public class TimeModel
{
    public string Label { get;set; }
    public Point[] Tiles { get;set; }

    public TimeModel(string label, Point[] tiles)
    {
        Label = label;
        Tiles = tiles;
    }
}


public class MainPage : ContentPage
{
    public static void UpdateApplication(Type[] types)
    {
        Console.WriteLine("Updated types:");
        Console.WriteLine(types.Length == 0 ? "<empty>" : string.Join(",", types.Select(t => t.Name)));
        //MainPage.Build();
    }

    Grid tileGrid;

    readonly TimeModel[] HoursMap = {
        new TimeModel("열두", new Point[]{ new Point(2,2), new Point(2,4) }),
        new TimeModel("한", new Point[]{ new Point(0,0) }), 
        new TimeModel("두", new Point[]{ new Point(0,1) }),
        new TimeModel("세", new Point[]{ new Point(0,2) }),
        new TimeModel("네", new Point[]{ new Point(0,3) }),
        new TimeModel("다섯", new Point[]{ new Point(0,4), new Point(0,5) }),
        new TimeModel("여섯", new Point[]{ new Point(1,0), new Point(1,1) }),
        new TimeModel("일곱", new Point[]{ new Point(1,2), new Point(1,3) }),
        new TimeModel("여덟", new Point[]{ new Point(1,4), new Point(1,5) }),
        new TimeModel("아홉", new Point[]{ new Point(2,0), new Point(2,1) }),
        new TimeModel("열", new Point[]{ new Point(2,2) }),
        new TimeModel("열한아", new Point[]{ new Point(2,2), new Point(2,3) })
    };
    
    readonly string[] MinutesMap = {
        "정", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구",
        "십", "십일", "십이", "십삼", "십사", "십오", "십육", "십칠", "십팔", "십구",
        "이십", "이십일", "이십이", "이십삼", "이십사", "이십오", "이십육", "이십칠", "이십팔", "이십구" ,
        "삼십", "삼십일", "삼십이", "삼십삼", "삼십사", "삼십오", "삼십육", "삼십칠", "삼십팔", "삼십구",
        "사십", "사십일", "사십이", "사십삼", "사십사", "사십오", "사십육", "사십칠", "사십팔", "사십구",
        "오십", "오십일", "오십이", "오십삼", "오십사", "오십오", "오십육", "오십칠", "오십팔", "오십구"
    };

    

    private System.Timers.Timer _timer;

    enum Row { First, Second, Third, Fourth, Fifth, Sixth }
	enum Column { First, Second, Third, Fourth, Fifth, Sixth }
	void Build() => Content = 
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
				}
            })
            .Margins(top: 30, bottom: 30, left:30, right:30)
            .CenterHorizontal().CenterVertical().TapGesture((t) =>
            {
                t.Tapped += (s,e)=>{
                    Build();
                };
            })
        } };

		class TitleLabel : Label
		{
			public TitleLabel()
			{
				FontSize = 64;
				TextColor = Colors.White;
                Opacity = 0.2;

				VerticalTextAlignment = TextAlignment.Center;
				HorizontalTextAlignment = TextAlignment.Center;

				Padding = new Thickness(10, 10);
			}
		}

    // Set Color for hour, min, and set opacity
    // private void UpdateRealtimeFlag(bool isShow)
    // {
    //     resetOpacity();

    //     if (isShow)
    //     {
    //         var currentTime = DateTime.Now;

    //         if (currentTime.Hour == 0 || currentTime.Hour == 12)
    //         {
    //             if (currentTime.Hour == 12 && currentTime.Minute == 0)
    //             {
    //                 hangulClock_element_51.Opacity = 1;
    //                 hangulClock_element_61.Opacity = 1;
    //             }
    //             else if (currentTime.Hour == 0 && currentTime.Minute == 0)
    //             {
    //                 hangulClock_element_41.Opacity = 1;
    //                 hangulClock_element_51.Opacity = 1;
    //             }
    //             else
    //             {
    //                 hangulClock_element_33.Opacity = 1;
    //                 hangulClock_element_35.Opacity = 1;
    //                 hangulClock_element_36.Opacity = 1;
    //             }
    //         }
    //         else if (currentTime.Hour == 1 || currentTime.Hour == 13)
    //         {
    //             hangulClock_element_11.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 2 || currentTime.Hour == 14)
    //         {
    //             hangulClock_element_12.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 3 || currentTime.Hour == 15)
    //         {
    //             hangulClock_element_13.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 4 || currentTime.Hour == 16)
    //         {
    //             hangulClock_element_14.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 5 || currentTime.Hour == 17)
    //         {
    //             hangulClock_element_15.Opacity = 1;
    //             hangulClock_element_16.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 6 || currentTime.Hour == 18)
    //         {
    //             hangulClock_element_21.Opacity = 1;
    //             hangulClock_element_22.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 7 || currentTime.Hour == 19)
    //         {
    //             hangulClock_element_23.Opacity = 1;
    //             hangulClock_element_24.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 8 || currentTime.Hour == 20)
    //         {
    //             hangulClock_element_25.Opacity = 1;
    //             hangulClock_element_26.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 9 || currentTime.Hour == 21)
    //         {
    //             hangulClock_element_31.Opacity = 1;
    //             hangulClock_element_32.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 10 || currentTime.Hour == 22)
    //         {
    //             hangulClock_element_33.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }
    //         else if (currentTime.Hour == 11 || currentTime.Hour == 23)
    //         {
    //             hangulClock_element_33.Opacity = 1;
    //             hangulClock_element_34.Opacity = 1;
    //             hangulClock_element_36.Opacity = 1;
    //         }

    //         if(currentTime.Minute == 1)
    //         {
    //             hangulClock_element_52.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 2)
    //         {
    //             hangulClock_element_53.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 3)
    //         {
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 4)
    //         {
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 5)
    //         {
    //             hangulClock_element_62.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 6)
    //         {
    //             hangulClock_element_56.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 7)
    //         {
    //             hangulClock_element_63.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 8)
    //         {
    //             hangulClock_element_64.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 9)
    //         {
    //             hangulClock_element_65.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 10)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 11)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_52.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 12)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_53.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 13)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 14)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_55.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 15)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_62.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 16)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_56.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 17)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_63.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 18)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_64.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 19)
    //         {
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_65.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 20)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 21)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_52.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 22)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_53.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 23)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 24)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_55.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 25)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_61.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 26)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_56.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 27)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_63.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 28)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_64.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 29)
    //         {
    //             hangulClock_element_42.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_65.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 30)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 31)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_52.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 32)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_53.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 33)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 34)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_55.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 35)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_62.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 36)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_56.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 37)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_63.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 38)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_64.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 39)
    //         {
    //             hangulClock_element_43.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_65.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 40)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 41)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_52.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 42)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_53.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 43)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 44)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_55.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 45)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_62.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 46)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_56.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 47)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_63.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 48)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_64.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 49)
    //         {
    //             hangulClock_element_44.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_65.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 50)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 51)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_52.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 52)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_53.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 53)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_54.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 54)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_55.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 55)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_62.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 56)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_56.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 57)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_63.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 58)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_64.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //         else if (currentTime.Minute == 59)
    //         {
    //             hangulClock_element_45.Opacity = 1;
    //             hangulClock_element_46.Opacity = 1;
    //             hangulClock_element_65.Opacity = 1;
    //             hangulClock_element_66.Opacity = 1;
    //         }
    //     }
    // }

    public MainPage()
    {
        Build();
        _timer = new System.Timers.Timer()
        { Interval = 1000, Enabled = true };
        _timer.Elapsed += timer_Handler;
    }

    private void timer_Handler(object sender, System.Timers.ElapsedEventArgs e)
    {
        //Now.Value = DateTime.Now;
        ResetGrid();
        UpdateGrid();
    }

    private void UpdateGrid()
    {
        var d = DateTime.Now;
        var hourModel = HoursMap[d.Hour % 12];
        Console.WriteLine($"Hour: {d.Hour} Label:{hourModel.Label}");
        int cnt = tileGrid.Children.Where(c => {
            foreach(var t in hourModel.Tiles)
            {
                if((tileGrid.GetRow(c) == t.X && tileGrid.GetColumn(c) == t.Y) || ((Label)c).Text == "시")
                {
                    ((Label)c).Opacity = 1;
                    ((Label)c).TextColor = Colors.LawnGreen;
                    return true;
                }
                    
            }
            return false;
        }).Count();

        //if (tiles != null) { 
        //    foreach(var t in tiles) {
        //        ((Label) t).Opacity = 1;
        //        ((Label)t).TextColor = Colors.LawnGreen;
        //    }
        //}
    }

    private void ResetGrid()
    {
        foreach(var l in tileGrid.Children)
        {
            ((Label)l).Opacity = 0.2;
            ((Label)l).TextColor = Colors.White;
        }
    }
}

