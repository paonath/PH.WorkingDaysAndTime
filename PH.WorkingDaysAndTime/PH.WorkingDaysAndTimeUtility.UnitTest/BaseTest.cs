using System;
using System.Collections.Generic;
using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    
    public class BaseTest
    {

        [Fact]
        public void EmptyWeek_Fail_On_Instantiate()
        {
            Exception f0 = null;
            Exception f1 = null;

            try
            {
                var fake = new WeekDaySpan();
                var utility = new WorkingDaysAndTimeUtility(fake, new List<HoliDay>());
            }
            catch (ArgumentException ex)
            {
                f0 = ex;
            }
            try
            {
                var fake = new WeekDaySpan() { WorkDays = new Dictionary<DayOfWeek, WorkDaySpan>() };
                var utility = new WorkingDaysAndTimeUtility(fake, new List<HoliDay>());
            }
            catch (ArgumentException ex)
            {
                f1 = ex;
            }

            Assert.NotNull(f0);
            Assert.NotNull(f1);

        }

        [Fact]
        public void Check_Symmetrical_And_NotSummetrical_Week()
        {
            var symmetrical = GetSimpleWeek();
            var notSymm = GetAWeek();

            Assert.True(symmetrical.Symmetrical);
            Assert.False(notSymm.Symmetrical);
        }

        protected WeekDaySpan GetSimpleWeek()
        {
            var wts1 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(13, 0, 0) };
            var wts2 = new WorkTimeSpan() { Start = new TimeSpan(14, 0, 0), End = new TimeSpan(18, 0, 0) };
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
            return week;
        }

        /// <summary>
        /// In Italy we have this list of Holidays plus 1 day different on each province.
        /// For mine is Dec. 1.
        /// </summary>
        /// <returns></returns>
        protected List<HoliDay> GetItalianHolidays()
        {
            var italians = new List<HoliDay>()
            {
                new EasterMonday(),
                new HoliDay(1, 1),
                new HoliDay(6, 1),
                new HoliDay(25, 4),
                new HoliDay(1, 5),
                new HoliDay(2, 6),
                new HoliDay(15, 8),
                new HoliDay(1, 11),
                new HoliDay(8, 12),
                new HoliDay(25, 12),
                new HoliDay(26, 12)
            };

            italians.Add(new HoliDay(1, 12));
            return italians;
        }


        protected WeekDaySpan GetAWeek()
        {
            var wts1 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(13, 0, 0) };
            var wts2 = new WorkTimeSpan() { Start = new TimeSpan(14, 0, 0), End = new TimeSpan(16, 0, 0) };
            var wts3 = new WorkTimeSpan() { Start = new TimeSpan(16, 30, 0), End = new TimeSpan(17, 0, 0) };

            var wtsA = new List<WorkTimeSpan>() { wts1, wts2 };
            var wtsB = new List<WorkTimeSpan>() { wts1, wts2, wts3 };
            var week = new WeekDaySpan()
            {
                WorkDays = new Dictionary<DayOfWeek, WorkDaySpan>()
                {
                    {DayOfWeek.Monday, new WorkDaySpan() {TimeSpans = wtsA}}
                    ,
                    {DayOfWeek.Tuesday, new WorkDaySpan() {TimeSpans = wtsB}}
                    ,
                    {DayOfWeek.Wednesday, new WorkDaySpan() {TimeSpans = wtsA}}
                    ,
                    {DayOfWeek.Thursday, new WorkDaySpan()}
                    ,
                    {DayOfWeek.Friday, new WorkDaySpan() {TimeSpans = wtsB}}
                    ,
                    {DayOfWeek.Saturday, new WorkDaySpan() {TimeSpans = wtsA}}

                }
            };
            return week;
        }

        protected List<HoliDay> GetCrazyListForStressTest()
        {
            DateTime st = new DateTime(2015, 1, 1);
            List<HoliDay> l = new List<HoliDay>();
            for (int i = 0; i < 365; i++)
            {
                var d = st.AddDays(i);
                l.Add(new HoliDay(d.Day, d.Month));
            }
            return l;
        }

        protected WeekDaySpan Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H()
        {
            var wts1 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(10, 0, 0) };
            var wts2 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(10, 30, 0) };
            var wtsd1 = new List<WorkTimeSpan>() { wts1};
            var wtsd2 = new List<WorkTimeSpan>() { wts2 };

            var week = new WeekDaySpan()
            {
                WorkDays = new Dictionary<DayOfWeek, WorkDaySpan>()
                {
                    {DayOfWeek.Monday, new WorkDaySpan() {TimeSpans = wtsd1}}
                    ,
                    {DayOfWeek.Tuesday, new WorkDaySpan() {TimeSpans = wtsd2}}
                }
            };
            return week;
        }
    }
}
