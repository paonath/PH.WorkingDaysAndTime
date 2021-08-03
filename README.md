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
- can split a Work-time into slices: ordinary, extraordinary, etc.

## Code Examples

### AddWorkingDays(DateTime start, int days)
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

### GetWorkingDaysBetweenTwoWorkingDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true)
```c#
var start = new DateTime(2015, 12, 31, 9, 0, 0);
var end = new DateTime(2016, 1, 7, 9, 0, 0);

//omitted configurations and holidays...
var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

//r is a workdays List<DateTime> between Dec 31 and Jan 7.
var r = utility.GetWorkingDaysBetweenTwoWorkingDateTimes(start, end);
```

### Testing if given date is Working-Datetime
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

### Testing if given date is Working-Date
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

### Split Worked Time
```c#
 public static TimeSlotConfig GetTimeSlotConfig()
        {
            var cfg = new TimeSlotConfig()
            {
                TimesDictionary = new Dictionary<DayOfWeek, List<TimeSlot>>()
            };



            cfg.TimesDictionary.Add(DayOfWeek.Monday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            });

            cfg.TimesDictionary.Add(DayOfWeek.Tuesday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
            });

            cfg.TimesDictionary.Add(DayOfWeek.Wednesday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            });

            cfg.TimesDictionary.Add(DayOfWeek.Thursday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            });

            cfg.TimesDictionary.Add(DayOfWeek.Friday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            });
            cfg.TimesDictionary.Add(DayOfWeek.Saturday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            });
            cfg.TimesDictionary.Add(DayOfWeek.Sunday, new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            });

            cfg.HolyDaySlots = new List<TimeSlot>()
            {
                new TimeSlot("Straordinario feriale notturno  ", 1.1, 2.1, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                new TimeSlot("Straordinario feriale diurno    ", 1.3, 2.3, new TimeSpan(6, 01, 0), new TimeSpan(8, 59, 59)),
                new TimeSlot("Ordinario                       ", 1.0, 2.0, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)),
                new TimeSlot("Straordinario feriale diurno   2", 1.3, 2.3, new TimeSpan(18, 01, 0), new TimeSpan(20, 0, 0)),
                new TimeSlot("Straordinario feriale notturno 2", 1.1, 2.1, new TimeSpan(20, 01, 0), new TimeSpan(23, 59, 59)),
            };

            return cfg;
        }
       
//-------------------
var weekConf   = GetSimpleWeek();
var utility    = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
var slotConfig = GetTimeSlotConfig();
utility.SetTimeSlotConfig(slotConfig);

var n           = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 59, 58);
double hoursAmount = 2;
var e           = n.AddHours(hoursAmount);

var res = utility.SplitWorkedTimeInFactors(n, e);


var hours = res.TotalDuration.TotalHours;
```

### Implements a multi-calculated holiday
```c#
    /// <summary>
    /// An example implementation of <see cref="MultiCalculatedHoliDay"/>  
    /// </summary>
    /// <seealso cref="PH.WorkingDaysAndTimeUtility.Configuration.MultiCalculatedHoliDay" />
    public class WorkingOnSaturdayIfOdd : MultiCalculatedHoliDay
    {
        internal WorkingOnSaturdayIfOdd() : base(0, 0)
        {
        }

        public override Type GetHolyDayType() => typeof(WorkingOnSaturdayIfOdd);

        /// <summary>Calculates the list of MultiCalculatedHoliDays for the given year.</summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public override List<DateTime> CalculateList(int year)
        {
            var         first        = new DateTime(year, 1, 1, 0, 0, 0);
            var         last         = new DateTime(year +1, 1, 1, 0, 0, 0);
            CultureInfo myCI         = new CultureInfo("it-IT");
            var         baseHolidays = new List<DateTime>();
            int         add          = 1;
            while (first < last)
            {
                if (first.DayOfWeek == DayOfWeek.Saturday)
                {
                    add = 7;
                    var weekNumber = myCI.Calendar.GetWeekOfYear(first, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    if (weekNumber % 2 == 0)
                    {
                        baseHolidays.Add(first);
                    }
                }

                first = first.AddDays(add);
            }

            return baseHolidays;
        }
    }
//...
[Fact]
        public void TestWorkingOnSaturdayIfOdd()
        {
            var wts1 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(13, 0, 0)  };

            var wts2 = new WorkTimeSpan() { Start = new TimeSpan(14, 0, 0), End = new TimeSpan(18, 0, 0) };
            var wts  = new List<WorkTimeSpan>() { wts1, wts2 };
            var week = new WeekDaySpan()
            {
                WorkDays = new Dictionary<DayOfWeek, WorkDaySpan>()
                {
                    {DayOfWeek.Monday, new WorkDaySpan() {TimeSpans = wts }}
                    ,
                    {DayOfWeek.Tuesday, new WorkDaySpan() {TimeSpans = wts}}
                    ,
                    {DayOfWeek.Wednesday, new WorkDaySpan() {TimeSpans = wts}}
                    ,
                    {DayOfWeek.Thursday, new WorkDaySpan() {TimeSpans = wts}}
                    ,
                    {DayOfWeek.Friday, new WorkDaySpan() {TimeSpans = wts}} ,
                    {DayOfWeek.Saturday, new WorkDaySpan() {TimeSpans = wts}}
                }
            };

            var italians = GetItalianHolidaysWithNoEasterMonday();
            italians.Add(new EasterMonday());
            italians.Add(new HoliDay(1, 12));
            italians.Add(new WorkingOnSaturdayIfOdd());

            var utility = new PH.WorkingDaysAndTimeUtility.WorkingDaysAndTimeUtility(week, italians);

            var aSaturday = new DateTime(2021, 8, 7);
            var t1        = utility.IsAWorkDay(aSaturday);
            var t2        = utility.IsAWorkDay(aSaturday.AddDays(7));
            var t3        = utility.IsAWorkDay(aSaturday.AddDays(14));
            var t4        = utility.IsAWorkDay(aSaturday.AddDays(21));

            Assert.False(t1);
            Assert.False(t3);
            Assert.True(t2);
            Assert.True(t4);

        }
    }
```

## Code Configuration Examples

### Use of WorkingDaysConfig
```c#
//note thats w is WeekDaySpan and l is List<AHolyDay>
//cfg is Json serializable
var cfg = new WorkingDaysConfig(w, l);

```

### Map-Config Style
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
