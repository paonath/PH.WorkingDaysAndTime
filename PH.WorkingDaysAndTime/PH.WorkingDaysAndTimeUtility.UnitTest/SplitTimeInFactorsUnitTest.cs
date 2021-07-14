using System;
using System.Collections.Generic;
using System.Linq;
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

            var n           = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 59, 58);
            double hoursAmount = 2;
            var e           = n.AddHours(hoursAmount);

            var res = utility.SplitWorkedTimeInFactors(n, e);

            

            var hours = res.TotalDuration.TotalHours;

            Assert.Equal(hoursAmount, hours);



        }


        [Fact]
        public void CheckValues()
        {
             #region config

                var wts1 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(13, 0, 0)  };

                var wts2 = new WorkTimeSpan() { Start = new TimeSpan(14, 0, 0), End = new TimeSpan(18, 0, 0) };
                var wts  = new List<WorkTimeSpan>() { wts1, wts2 };
                var weekConfig = new WeekDaySpan()
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
                        {DayOfWeek.Friday, new WorkDaySpan() {TimeSpans = wts}}
                    }
                };
                
                var italianHolydays = new List<AHolyDay>()
                {
                    new HoliDay(1, 1),
                    new HoliDay(6, 1),
                    new HoliDay(25, 4),
                    new HoliDay(1, 5),
                    new HoliDay(2, 6),
                    new HoliDay(15, 8),
                    new HoliDay(1, 11),
                    new HoliDay(8, 12),
                    new HoliDay(25, 12),
                    new HoliDay(26, 12),
                    new EasterMonday()
                };

                #region time config

                

                var timecfg = new TimeSlotConfig()
                {
                    TimesDictionary = new Dictionary<DayOfWeek, List<TimeSlot>>()
                };
            
                var slots = new List<TimeSlot>()
                {
                    new TimeSlot($"STRAORD_FERIALE_NOTTURNO"  , 2.0, 2.0, new TimeSpan(0, 0, 0), new TimeSpan(5, 59, 59)),
                    new TimeSlot($"STRAORD_FERIALE_DIURNO"    , 1.4, 1.4, new TimeSpan(6, 00, 0), new TimeSpan(8, 59, 59)),
                    new TimeSlot($"ORDINARIO"                 , 1.0, 1.4, new TimeSpan(9, 0, 0), new TimeSpan(17, 59, 59)),
                    new TimeSlot($"STRAORD_FERIALE_DIURNO"    , 1.4, 1.4, new TimeSpan(18, 00, 0), new TimeSpan(19,59, 59)),
                    new TimeSlot($"STRAORD_FERIALE_NOTTURNO"  , 2.0, 2.0, new TimeSpan(20, 00, 0), new TimeSpan(23, 59, 59))
                };
                timecfg.TimesDictionary.Add(DayOfWeek.Monday,slots);
                timecfg.TimesDictionary.Add(DayOfWeek.Tuesday,slots);
                timecfg.TimesDictionary.Add(DayOfWeek.Wednesday,slots);
                timecfg.TimesDictionary.Add(DayOfWeek.Thursday,slots);
                timecfg.TimesDictionary.Add(DayOfWeek.Friday,slots);


                timecfg.TimesDictionary.Add(DayOfWeek.Saturday, new List<TimeSlot>()
                {
                    new TimeSlot($"STRAORD_FERIALE_NOTTURNO"  , 2.0, 2.0, new TimeSpan(0, 0, 0), new TimeSpan(5, 59, 59)),
                    new TimeSlot($"STRAORD_FERIALE_DIURNO"    , 1.4, 1.4, new TimeSpan(6, 00, 0), new TimeSpan(19, 59, 59)),
                    new TimeSlot($"STRAORD_FERIALE_NOTTURNO"  , 2.0, 2.0, new TimeSpan(20, 00, 0), new TimeSpan(23, 59, 59))
                });

                var sunDay =   new List<TimeSlot>()
                {
                    new TimeSlot($"STRAORD_FESTIVO_NOTTURNO"  , 2.0, 2.0, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)),
                    new TimeSlot($"STRAORD_FESTIVO_DIURNO"    , 1.4, 1.4, new TimeSpan(6, 00, 0), new TimeSpan(19, 59, 59)),
                    new TimeSlot($"STRAORD_FESTIVO_NOTTURNO"  , 2.0,2.0, new TimeSpan(20, 00, 01), new TimeSpan(23, 59, 59))
                };

                timecfg.TimesDictionary.Add(DayOfWeek.Sunday, sunDay);

                timecfg.HolyDaySlots = sunDay;


                #endregion

                #endregion


                DateTime start = new DateTime(2021, 7, 13, 9, 0, 0);
                DateTime end = new DateTime(2021, 7, 13, 18, 30, 0);

                var u = new WorkingDaysAndTimeUtility(weekConfig, italianHolydays);
                u.SetTimeSlotConfig(timecfg);
                var splitTimes = u.SplitWorkedTimeInFactors(start, end);

                double ordinario = splitTimes.WorkSlices
                                             .Where(x => x.TimeSlot.Key == "ORDINARIO")
                                             .ToList()
                                             .Select(x => x.Duration.TotalHours).Sum();
                

                double sordinarioferialediurno = splitTimes.WorkSlices
                                                           .Where(x => x.TimeSlot.Key == "STRAORD_FERIALE_DIURNO")
                                                           .ToList()
                                                           .Select(x => x.Duration.TotalHours).Sum();

                double sordinarioferialenott = splitTimes.WorkSlices
                                                         .Where(x => x.TimeSlot.Key == "STRAORD_FERIALE_NOTTURNO")
                                                         .ToList()
                                                         .Select(x => x.Duration.TotalHours).Sum();

                double sordinariofestDiurno = splitTimes.WorkSlices
                                                        .Where(x => x.TimeSlot.Key == "STRAORD_FESTIVO_DIURNO")
                                                        .ToList()
                                                        .Select(x => x.Duration.TotalHours).Sum(); 
            
                double sordinariofestNotturno = splitTimes.WorkSlices
                                                          .Where(x => x.TimeSlot.Key == "STRAORD_FESTIVO_NOTTURNO")
                                                          .ToList()
                                                          .Select(x => x.Duration.TotalHours).Sum();


                var aHoly = u.SplitWorkedTimeInFactors(new DateTime(2021, 6, 2, 0, 0, 0),
                                                       new DateTime(2021, 6, 2, 23, 59, 59));


            Assert.Equal((double)9, ordinario);
            Assert.Equal((double)9, ordinario);
        }
    }
}