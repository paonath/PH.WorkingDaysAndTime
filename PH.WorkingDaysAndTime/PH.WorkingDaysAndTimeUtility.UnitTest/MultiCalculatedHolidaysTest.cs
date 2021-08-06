using System;
using System.Collections.Generic;
using System.Globalization;
using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    public class MultiCalculatedHolidaysTest : BaseTest
    {
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
}