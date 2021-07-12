using System;
using System.Collections.Generic;
using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    public class SplitTimeInFactorsUnitTest : BaseTest
    {


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
       
        [Fact]
        public void GetSimpleTimeFactorInWorkDayAndWorkingTime()
        {
            var weekConf   = GetSimpleWeek();
            var utility    = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var slotConfig = GetTimeSlotConfig();
            utility.SetTimeSlotConfig(slotConfig);

            var n = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 59, 58);
            var e = n.AddHours(10);

            var res = utility.SplitWorkedTimeInFactors(n, e);


            var b = res.WorkSlices;





        }
    }
}