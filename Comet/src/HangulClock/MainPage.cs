using System;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Comet;

namespace HangulClock;

public class MainPage : View
{
	readonly State<DateTime> Now = new State<DateTime>();

	readonly State<bool> ShowOverlay = false;

	Grid Overlay;
	Text OverlayText;

	readonly string[] HoursMap = { "열둘", "한", "두", "세", "네", "다섯", "여섯", "일곱", "여덟", "아홉", "열", "열한아" };
	readonly string[] MinutesMap = { 
		"정", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구", 
		"십", "십일", "십이", "십삼", "십사", "십오", "십육", "십칠", "십팔", "십구",
		"이십", "이십일", "이십이", "이십삼", "이십사", "이십오", "이십육", "이십칠", "이십팔", "이십구" ,
		"삼십", "삼십일", "삼십이", "삼십삼", "삼십사", "삼십오", "삼십육", "삼십칠", "삼십팔", "삼십구",
		"사십", "사십일", "사십이", "사십삼", "사십사", "사십오", "사십육", "사십칠", "사십팔", "사십구",
		"오십", "오십일", "오십이", "오십삼", "오십사", "오십오", "오십육", "오십칠", "오십팔", "오십구"
	};
	
	private System.Timers.Timer _timer;
	
	Text HangulText(Binding<string> val, int row, int column)
	{
		var t = new Text(val)
			.VerticalTextAlignment(TextAlignment.Center)
			.HorizontalTextAlignment(TextAlignment.Center)
			.Frame(height: 64, width: 64)
			.FontSize(64)
			.Color(()=>GetColor(Now, val, row, column))
			.Opacity(()=>GetOpacity(Now, val, row, column));
		// t.Cell(row, column);
		return t;
	}

	// "다섯", "여섯"
	Color GetColor(State<DateTime> d, string s, int row, int column)
	{
		Color c  = Colors.White;
		string hourStr = HoursMap[d.Value.Hour % 12];
		if(row < 3 && (hourStr.IndexOf($"{s}") >= 0 || s == "시")){
			// there are some duplicates, so how do we know which to use?
			// 5, 6, 11, 12
			if("다섯, 여섯, 여덟, 열한아, 열둘".IndexOf(hourStr) >= 0){ // are we one of the problematic ?
				if(
					(hourStr == "다섯" && row == 0)
					|| (hourStr == "여섯" && row == 1 && column < 1)
					|| (hourStr == "여덟" && row == 1 && column > 1)
					|| (hourStr == "열한아" && row == 2)
					|| (hourStr == "열둘" && row == 2)
				){
					c = Colors.LawnGreen;	
				}//skips the rest
			}else{
				c = Colors.LawnGreen;
			}
		}else if(row > 2 && (MinutesMap[d.Value.Minute].IndexOf($"{s}") >= 0 || s == "분")){
			if(row == 3){
				if((MinutesMap[d.Value.Minute].IndexOf("십") == 1 || MinutesMap[d.Value.Minute].IndexOf("십") == 0) 
					&& MinutesMap[d.Value.Minute].IndexOf($"{s}") <= 1)
					c = Colors.OrangeRed;
			}else{
				if((MinutesMap[d.Value.Minute].IndexOf("십") == 1 || MinutesMap[d.Value.Minute].IndexOf("십") == 0)){ 
					if(MinutesMap[d.Value.Minute].LastIndexOf($"{s}") > 0)
						c = Colors.OrangeRed;
				}else{
					c = Colors.OrangeRed;
				}
			}
		}
		return c;
	}

	double GetOpacity(State<DateTime> d, string s, int row, int column)
	{
		double o  = 0.2;
		string hourStr = HoursMap[d.Value.Hour % 12];
		if(row < 3 && (hourStr.IndexOf($"{s}") >= 0 || s == "시")){
			if("다섯, 여섯, 여덟, 열한아, 열둘".IndexOf(hourStr) >= 0){ // are we one of the problematic ?
				if(
					(hourStr == "다섯" && row == 0)
					|| (hourStr == "여섯" && row == 1 && column < 1)
					|| (hourStr == "여덟" && row == 1 && column > 1)
					|| (hourStr == "열한아" && row == 2)
					|| (hourStr == "열둘" && row == 2)
				){
					o = 1.0;
				}//skips the rest
			}else{
				o = 1.0;
			}
		}else if(row > 2 && (MinutesMap[d.Value.Minute].IndexOf($"{s}") >= 0 || s == "분")){
			if(row == 3){
				if((MinutesMap[d.Value.Minute].IndexOf("십") == 1 || MinutesMap[d.Value.Minute].IndexOf("십") == 0) 
					&& MinutesMap[d.Value.Minute].IndexOf($"{s}") <= 1)
					o = 1.0;
			}else{
				if((MinutesMap[d.Value.Minute].IndexOf("십") == 1 || MinutesMap[d.Value.Minute].IndexOf("십") == 0)){ 
					if(MinutesMap[d.Value.Minute].LastIndexOf($"{s}") > 0)
						o = 1.0;
				}else{
					o = 1.0;
				}
			}
		}
		return o;

	}

	string GetHangulTime(DateTime d)
	{
		string hangulString = string.Empty;

		hangulString = $"{HoursMap[d.Hour % 12]} 시 {MinutesMap[d.Minute]} 분"; 

		return new State<string>(hangulString);
	}

	public MainPage() {
		_timer = new System.Timers.Timer() 
			{ Interval= 1000, Enabled = true }; 
		_timer.Elapsed += timer_Handler;
	}

	private void timer_Handler(object sender, System.Timers.ElapsedEventArgs e)
	{
		Now.Value = DateTime.Now;
	}

	[Body]
	View body()
		=> new Grid(rows: new object[] { "50", "*"}){
				new VGrid (6)
				{
					HangulText("한", row:0, column: 0),
					HangulText("두", row:0, column: 1),
					HangulText("세", row:0, column: 2),
					HangulText("네", row:0, column: 3),
					HangulText("다", row:0, column: 4),
					HangulText("섯", row:0, column: 5),

					HangulText("여", row:1, column: 0),
					HangulText("섯", row:1, column: 1),
					HangulText("일", row:1, column: 2),
					HangulText("곱", row:1, column: 3),
					HangulText("여", row:1, column: 4),
					HangulText("덟", row:1, column: 5),

					HangulText("아", row:2, column: 0),
					HangulText("홉", row:2, column: 1),
					HangulText("열", row:2, column: 2),
					HangulText("한", row:2, column: 3),
					HangulText("두", row:2, column: 4),
					HangulText("시", row:2, column: 5),

					HangulText("일", row:3, column: 0),//자
					HangulText("이", row:3, column: 1),
					HangulText("삼", row:3, column: 2),
					HangulText("사", row:3, column: 3),
					HangulText("오", row:3, column: 4),
					HangulText("십", row:3, column: 5),

					HangulText("정", row:4, column: 0),
					HangulText("일", row:4, column: 1),
					HangulText("이", row:4, column: 2),
					HangulText("삼", row:4, column: 3),
					HangulText("사", row:4, column: 4),
					HangulText("오", row:4, column: 5),

					HangulText("☄", row:5, column: 0),
					HangulText("육", row:5, column: 1),
					HangulText("칠", row:5, column: 2),
					HangulText("팔", row:5, column: 3),
					HangulText("구", row:5, column: 4),
					HangulText("분", row:5, column: 5),
			}
			.Cell(row: 1),
			// overlay
			// (Overlay = new Grid(){
					(OverlayText = new Text(()=>GetHangulTime(Now.Value))
						.VerticalTextAlignment(TextAlignment.Center)
						.HorizontalTextAlignment(TextAlignment.Center)
						.Color(Colors.DarkOrange)
						.FontSize(64)	
						.Background(()=> ShowOverlay.Value ? Colors.White : Colors.Transparent)
						.Opacity(()=> ShowOverlay.Value ? 1 : 0)
						.Cell(rowSpan:2)
					
				// }.Cell(rowSpan:2).Background("#CC000000").Opacity(1)
			),
			new Button("", ()=>{
				OverlayText.Animate((o)=>{
					ShowOverlay.Value = !ShowOverlay.Value;
					o.Background(()=> ShowOverlay.Value ? Colors.White : Colors.Transparent);
					o.Opacity(()=> ShowOverlay.Value ? 1 : 0);
				}, duration: 1);

			})
				.Background(Colors.Transparent)
				.Cell(rowSpan:2)
		}.Background("#101010")
		
		;
}