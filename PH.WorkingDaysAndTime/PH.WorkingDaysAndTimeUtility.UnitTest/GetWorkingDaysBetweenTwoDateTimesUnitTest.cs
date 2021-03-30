using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    
    public class GetWorkingDaysBetweenTwoDateTimesUnitTest 
        : BaseTest
    {

        [Fact]
        public void Get_List_Of_WorkingDays_Between_31_Dec_2015_And_7_Jan_2016_Will_Get_A_List_Of_4()
        {
            var s = new DateTime(2015, 12, 31, 9, 0, 0);
            var e = new DateTime(2016, 1, 7, 9, 0, 0);
            var expected =
                (new List<DateTime>() { s, new DateTime(2016, 1, 4), new DateTime(2016, 1, 5), e })
                    .Select(x => x.Date)
                    .OrderByDescending(
                        x => x).ToList();

            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.Empty(differences1);
            Assert.Empty(differences2);


            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void Get_List_Of_WorkingDays_Between_31_Dec_2015_And_7_Jan_2016_Excluding_startAndEnd_Will_Get_A_List_Of_2()
        {
            var s = new DateTime(2015, 12, 31, 9, 0, 0);
            var e = new DateTime(2016, 1, 7, 9, 0, 0);
            var expected =
                (new List<DateTime>() { new DateTime(2016, 1, 4), new DateTime(2016, 1, 5) })
                    .Select(x => x.Date)
                    .OrderByDescending(
                        x => x).ToList();

            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.GetWorkingDaysBetweenTwoWorkingDateTimes(s, e, false);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.Empty(differences1);
            Assert.Empty(differences2);

            Assert.Equal(2, result.Count);
        }
        
        [Fact]
        public void Get_List_Of_WorkingDays_Between_7_Jan_2016_And_31_Dec_2015_Excluding_startAndEnd_Will_Get_A_List_Of_2()
        {
            var s = new DateTime(2016, 1, 7, 9, 0, 0);
            var e = new DateTime(2015, 12, 31, 9, 0, 0);
            var expected =
                (new List<DateTime>() { new DateTime(2016, 1, 4), new DateTime(2016, 1, 5) })
                    .Select(x => x.Date)
                    .OrderByDescending(
                        x => x).ToList();

            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.GetWorkingDaysBetweenTwoWorkingDateTimes(s, e, false);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.Empty(differences1);
            Assert.Empty(differences2);

            Assert.Equal(2, result.Count);
        }
        
        [Fact]
        public void Method_Used_In_Readme_Code_Example_2()
        {

            var weekConf = GetSimpleWeek();


            var start = new DateTime(2015, 12, 31, 9, 0, 0);
            var end = new DateTime(2016, 1, 7, 9, 0, 0);

            //omitted configurations and holidays...
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            //r is a workdays List<DateTime> between Dec 31 and Jan 7.
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(start, end);

            Assert.NotNull(r);
        }



        [Fact]
        public void More_Get_WorkingDays()
        {
            var start = new DateTime(2021, 1, 1);
            var end = new DateTime(2021, 12, 31);
            
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

            var workDays = utility.GetWorkingDaysBetweenTwoDateTimes(start, end);


            var dbg = workDays.LastOrDefault();
            Assert.NotEqual(start, dbg);
            Assert.Equal(new DateTime(2021,1,4).Date, dbg.Date);

        }
        [Fact]
        public void TestIfAWorkDay()
        {
            var start = new DateTime(2021, 1, 1);
            var end   = new DateTime(2021, 12, 31);
            
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


            var day   = new DateTime(2021, 1, 1);
            var d2w   = new DateTime(2021, 3, 30);

            var check0 = utility.IsAWorkDay(day);
            var check1 = utility.IsAWorkDay(d2w);
            

            Assert.False(check0);
            Assert.True(check1);
        }
    }
}
