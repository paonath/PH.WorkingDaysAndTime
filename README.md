# PH.WorkingDaysAndTime - [![NuGet Badge](https://buildstats.info/nuget/PH.WorkingDaysAndTime)](https://www.nuget.org/packages/PH.WorkingDaysAndTime/)


A tiny c# utility for calculating work days and work time.
The code is written in .NET C#.

The tool is useful for calculate difference between two dates of workdays,
to plan projects excluding holidays and absences.
Is also  a simple starting-point to addons to estimate the date of the end of a job.

The application works only counting the dates forward and it is assumed that the date entered as the first parameter is a working day.

The package is available on  [nuget](https://www.nuget.org/packages/PH.WorkingDaysAndTime) 

## Features
- can add *n* work-days to a DateTime;
- can add *n* work-hours to a DateTime;
- can get a List of work-DateTime between 2 dates;

## Code Examples

**AddWorkingDays(DateTime start, int days)**
```c#
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
//for mine is 1 Dec (see last element of the List<AHolyDay>).
var italiansHoliDays = new List<AHolyDay>()
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

**GetWorkingDaysBetweenTwoWorkingDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true)**
```c#
var start = new DateTime(2015, 12, 31, 9, 0, 0);
var end = new DateTime(2016, 1, 7, 9, 0, 0);

//omitted configurations and holidays...
var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

//r is a workdays List<DateTime> between Dec 31 and Jan 7.
var r = utility.GetWorkingDaysBetweenTwoWorkingDateTimes(start, end);
```

**Testing if given date is Working-Datetime**
```c#
[Fact]
public void Get_IfWorkingDay_OnTuesday_OnSimpleWeek_ReturnTrue()
{

    //omitted configurations and holidays...
    var tuesday = new DateTime(2018, 11, 6, 11,22,33);
    var prev0 = tuesday.AddMinutes(-1);
    var next0 = tuesday.AddMinutes(1);

    var weekConf = GetSimpleWeek();
    var utility  = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());

    var r = utility.IfWorkingMoment(tuesday, out DateTime next, out DateTime previous);

    Assert.True(r);
    Assert.Equal(prev0, previous);
    Assert.Equal(next0, next);

}
```

**Testing if given date is Working-Date**
```c#
[Fact]
public void TestIfAWorkDay()
{

    //omitted configurations and holidays(Italian Holidays)...
    var day   = new DateTime(2021, 1, 1);
    var d2w   = new DateTime(2021, 3, 30);

    var check0 = utility.IsAWorkDay(day);
    var check1 = utility.IsAWorkDay(d2w);
            

    Assert.False(check0);
    Assert.True(check1);
        

}
```

## Code Configuration Examples

**Use of WorkingDaysConfig**
```c#
//note thats w is WeekDaySpan and l is List<AHolyDay>
//cfg is Json serializable
var cfg = new WorkingDaysConfig(w, l);

```

**Map-Config Style**
```c#
var cfg = new WorkingDaysConfig()
                        .Week(new WeekDaySpan()
                        .Day(
                            DayOfWeek.Monday,
                            new WorkDaySpan()
                                .Time(new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0))
                                .Time(new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0)))
                        .Day(
                            DayOfWeek.Tuesday,
                            new WorkDaySpan()
                                .Time(new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0))
                                .Time(new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0)))
                           )
                      .Holiday(new AHolyDay(15, 8))
                      .Holiday(2, 6)
                      .Holiday(new EasterMonday());

var cfg2 = new WorkingDaysConfig()
            .Week(WeekDaySpan.CreateSymmetricalConfig(new WorkDaySpan()
                .Time(new TimeSpan(9, 0, 0),
                        new TimeSpan(13, 0, 0))
                .Time(new TimeSpan(14, 0, 0),
                        new TimeSpan(18, 0, 0)),
                new DayOfWeek[]
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday
                }));

```


## License

This software is licensed by [BSD-3-Clause](https://opensource.org/licenses/BSD-3-Clause).
Link to License: [license](https://github.com/paonath/PH.WorkingDaysAndTime/blob/master/mdLicense.md)
