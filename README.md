# PH.WorkingDaysAndTime

A tiny c# utility for calculating work days and work time.
The code is written in .NET C#.

Warning: this software is currently preview (beta): [v0.2](https://github.com/paonath/PH.WorkingDaysAndTime/releases/tag/v0.2)

## Features
- can add *n* work-days to a DateTime;
- can add *n* work-days to a DateTime using extension method;
- can add *n* work-hours to a DateTime;
- can get a List of work-DateTime between 2 dates;

## Code Examples

**AddWorkingDays(DateTime start, int days)**
```
//this is the configuration of a work-week: 8h/day from monday to friday
var wts1 = new WorkTimeSpan() 
	{ Start = new TimeSpan(9, 0, 0), End = new TimeSpan(13, 0, 0) };
var wts2 = new WorkTimeSpan() 
	{ Start = new TimeSpan(14, 0, 0), End = new TimeSpan(18, 0, 0) };
var wts = new List<WorkTimeSpan>() { wts1, wts2 };

var week = new WeekDaySpan()
{
	WorkDays = new Dictionary<DayOfWeek, WorkDaySpan>()
	{
		{DayOfWeek.Monday, new WorkDaySpan() {TimeSpans = wts}}
		,
		{DayOfWeek.Tuesday, new WorkDaySpan() {TimeSpans = wts}}
		,
		{DayOfWeek.Wednesday, new WorkDaySpan() {TimeSpans = wts}}
		,
		{DayOfWeek.Thursday, new WorkDaySpan() {TimeSpans = wts}}
		,
		{DayOfWeek.Friday, new WorkDaySpan() {TimeSpans = wts}}
	}
};

//this is the configuration for holidays: 
//in Italy we have this list of Holidays plus 1 day different on each province,
//for mine is 1 Dec (see last element of the List<HoliDay>).
var italiansHoliDays = new List<HoliDay>()
{
	new EasterMonday(),new HoliDay(1, 1),new HoliDay(6, 1),
	new HoliDay(25, 4),new HoliDay(1, 5),new HoliDay(2, 6),
	new HoliDay(15, 8),new HoliDay(1, 11),new HoliDay(8, 12),
	new HoliDay(25, 12),new HoliDay(26, 12)
	, new HoliDay(1, 12)
};

//instantiate with configuration
var utility = new WorkingDaysAndTimeUtility(week, italiansHoliDays);

//lets-go: add 3 working-days to Jun 1, 2015
var result = utility.AddWorkingDays(new DateTime(2015,6,1), 3);
//result is Jun 5, 2015 (see holidays list) 
```

**GetWorkingDaysBetweenTwoDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true)**
```
var start = new DateTime(2015, 12, 31, 9, 0, 0);
var end = new DateTime(2016, 1, 7, 9, 0, 0);

//omitted configurations and holidays...
var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

//r is a workdays List<DateTime> between Dec 31 and Jan 7.
var r = utility.GetWorkingDaysBetweenTwoDateTimes(start, end);
```

**AddWorkingDays - DateTime Extension Method**
```
DateTime d = new DateTime(2015, 6, 23);

//omitted configurations and holidays...
List<DayOfWeek> week = GetWorkWeek(); 
List<DateTime> holidays = new List<DateTime>(); 

GetItalianHolidays().ForEach(h =>
{
    holidays.Add(h.Calculate(2015));
});

DateTime result = d.AddWorkingDays(4, holidays, week);
```

