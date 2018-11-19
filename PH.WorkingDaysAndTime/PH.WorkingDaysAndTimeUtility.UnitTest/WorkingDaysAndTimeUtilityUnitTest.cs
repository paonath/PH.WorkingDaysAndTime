using System;
using System.Collections.Generic;
using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    public class WorkingDaysAndTimeUtilityUnitTest
        : BaseTest
    {


        [Fact]
        public void Add_1_Day_From_NoWorkingDay_Fail_On_Calculate()
        {
            var sunday = new DateTime(2015, 6, 14);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());
            Exception f0 = null;
            try
            {
                var fake = utility.AddWorkingDays(sunday, 4);
            }
            catch (Exception ex)
            {
                f0 = ex;
            }

            Assert.NotNull(f0);
        }

        [Fact]
        public void Add_1_Day_On_Simple_Week_From_2015_06_16_With_No_Holidays()
        {
            var d = new DateTime(2015, 6, 16,9,0,0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());

            var r = utility.AddWorkingDays(d, 1);
            var expected = new DateTime(2015, 6, 17,9,0,0);
            Assert.Equal(expected, r);

        }

        

        [Fact]
        public void Add_1_Day_On_Simple_With_2_HoliDays()
        {
            var d = new DateTime(2015, 6, 16,9,0,0);
            var holidays = new List<AHolyDay>() { new HoliDay(17, 6), new HoliDay(18, 6), new EasterMonday() };
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, holidays);

            var r = utility.AddWorkingDays(d, 1);
            var expected = new DateTime(2015, 6, 19,9,0,0);

            Assert.Equal(expected, r);

        }

        
        
        [Fact]
        public void Add_16Hours_On_8workingHoursDay_Will_Add_2_Days()
        {
            var d = new DateTime(2015, 6, 16, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());
            var r = utility.AddWorkingHours(d, 16);
            var e = new DateTime(2015, 6, 18, 9, 0, 0);
            Assert.Equal(e, r);


        }

        [Fact]
        public void Add_17Hours_On_8workingHoursDay_Will_Add_2_Days_and_1h()
        {
            var d = new DateTime(2015, 6, 16, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());
            var r = utility.AddWorkingHours(d, 17);
            var e = new DateTime(2015, 6, 18, 10, 0, 0);
            Assert.Equal(e, r);

        }

        [Fact]
        public void Add_33Hours_On_8workingHoursDay_Will_Add_3_Days_and_1h()
        {
            var d = new DateTime(2015, 6, 16, 9, 45, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());
            var r = utility.AddWorkingHours(d, 33);
            var e = new DateTime(2015, 6, 22, 10, 45, 0);
            Assert.Equal(e, r);

        }

        [Fact]
        public void Adding_3_WorkDays_To_31_Dec_2015_Will_Return_7_Jan_2016()
        {
            var d = new DateTime(2015, 12, 31, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.AddWorkingDays(d, 3);
            var e = new DateTime(2016, 1, 7, 9, 0, 0);
            Assert.Equal(e, r);

        }

        [Fact]
        public void Method_Used_In_Readme_Code_Example_1()
        {
            //this is the configuration of a work-week: 8h/day from monday to friday
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

            //this is the configuration for holidays: 
            //in Italy we have this list of Holidays plus 1 day different on each province, for mine is 8 Dec.
            var italiansHoliDays = new List<AHolyDay>()
            {
                new EasterMonday(),new HoliDay(1, 1),new HoliDay(6, 1),
                new HoliDay(25, 4),new HoliDay(1, 5),new HoliDay(2, 6),
                new HoliDay(15, 8),new HoliDay(1, 11),new HoliDay(8, 12),
                new HoliDay(25, 12),new HoliDay(26, 12)
                , new HoliDay(8, 12)
            };

            //instantiate with configuration
            var utility = new WorkingDaysAndTimeUtility(week, italiansHoliDays);

            //lets-go: add 3 working-days to Jun 1, 2015
            var result = utility.AddWorkingDays(new DateTime(2015,6,1), 3);
            //result is Jun 5, 2015 (see holidays list) 

            Assert.NotNull(result);

        }

        [Fact]
        public void Add_15_Minutes_To_24_Jun_12_55_Will_Get_14_10()
        {
            var d = new DateTime(2015, 6, 24, 12, 55, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.AddWorkingMinutes(d, 15);
            var e = new DateTime(2015, 6, 24, 14, 10, 0);
            Assert.Equal(e, r);

        }

        [Fact]
        public void Add_15_Minutes_AsTimeSpan_To_24_Jun_12_55_Will_Get_14_10()
        {
            var d        = new DateTime(2015, 6, 24, 12, 55, 0);
            var weekConf = GetSimpleWeek();
            var utility  = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.AddWorkingTimeSpan(d, new TimeSpan(0, 15, 0));
            var e        = new DateTime(2015, 6, 24, 14, 10, 0);
            Assert.Equal(e, r);

        }

        [Fact]
        public void Add_Invalid_TimeSpan_Throw()
        {
            Exception my = null;
            try
            {
                var d        = new DateTime(2015, 6, 24, 12, 55, 0);
                var weekConf = GetSimpleWeek();
                var utility  = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
                var r        = utility.AddWorkingTimeSpan(d, new TimeSpan(0, 0, 0));
                var e        = new DateTime(2015, 6, 24, 14, 10, 0);
            }
            catch (Exception e)
            {
                my = e;
            }

            Assert.NotNull(my);
            Assert.True(my is ArgumentException);
        }




        [Fact]
        public void Add_15_Minutes_To_24_Jun_17_55_Will_Get_25_Jun_9_10()
        {
            var d = new DateTime(2015, 6, 24, 17, 55, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.AddWorkingMinutes(d, 15);
            var e = new DateTime(2015, 6, 25, 9, 10, 0);
            Assert.Equal(e, r);

        }

        [Fact]
        public void Get_IfWorkingDay_OnSunday_OnSimpleWeek_ReturnFalse()
        {
            var sunday  = new DateTime(2018, 11, 4, 11,22,33);
            var weekConf = GetSimpleWeek();
            var utility  = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());

            var r = utility.IfWorkingMoment(sunday, out DateTime next, out DateTime previous);

            var r2 = utility.IfWorkingMomentGettingPrevious(sunday, out DateTime prev2);
            var r3 = utility.IfWorkingMomentGettingNext(sunday, out DateTime next2);

            Assert.False(r);

            Assert.Equal(next, next2);
            Assert.Equal(previous, prev2);



        }
        [Fact]
        public void Get_IfWorkingDay_OnTuesday_OnHolyDay_OnSimpleWeek_ReturnFalse()
        {
            var tuesday = new DateTime(2018, 11, 6, 11,22,33);
            var prev0 = new DateTime(2018, 11, 5, 18,0,0);
            var next0 = new DateTime(2018, 11, 7, 9,0,0);
           
            var weekConf = GetSimpleWeek();
            var utility  = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>(){new HoliDay(6,11)});

            var r = utility.IfWorkingMoment(tuesday, out DateTime next, out DateTime previous);

            Assert.False(r);
           
            Assert.Equal(prev0, previous);
            Assert.Equal(next0, next);

        }
        [Fact]
        public void Get_IfWorkingDay_OnTuesday_OnSimpleWeek_ReturnTrue()
        {
            var tuesday = new DateTime(2018, 11, 6, 11,22,33);
            var prev0 = tuesday.AddMinutes(-1);
            var next0 = tuesday.AddMinutes(1);

            var weekConf = GetSimpleWeek();
            var utility  = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());

            var r = utility.IfWorkingMoment(tuesday, out DateTime next, out DateTime previous);

            Assert.True(r);
            Assert.Equal(prev0, previous);
            Assert.Equal(next0, next);

        }

        [Fact]
        public void Test_LeapYear()
        {
            int leap_year = 2016;

            var prevFriday = new DateTime(leap_year,2,26);
            var expected = new DateTime(leap_year, 2, 29);
            var expectedTuesday = expected.AddDays(1);


            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>());

            var monday = utility.AddWorkingDays(prevFriday, 1);

            //
            var utility2 = new WorkingDaysAndTimeUtility(weekConf, new List<AHolyDay>(){ new HoliDay(29,2)});
            var tuesday = utility2.AddWorkingDays(prevFriday, 1);

            var untouchedDay = new DateTime(2014, 2, 28,15,12,34);


            var tParam = utility2.IfWorkingMomentGettingNext(tuesday, out var nextWork);
            var tTrue = utility2.IfWorkingMomentGettingNext(untouchedDay, out var nextWork2);

            var finalTest = utility2.AddWorkingDays(untouchedDay, 1);


            Assert.Equal($"{expected:yy-MM-dd}" , $"{monday:yy-MM-dd}" );
            Assert.Equal($"{expectedTuesday:yy-MM-dd}" , $"{tuesday:yy-MM-dd}" );
            Assert.True(tParam);
            Assert.True(tTrue);



        }

        [Fact]
        public void CalculateThanksGivingDay()
        {
            //var l = new List<AHolyDay>() {new ThanksgivingDay()};
            var thanksgiving = new ThanksgivingDay();
            var day = thanksgiving.Calculate(2018);

            Assert.Equal(new DateTime(2018,11,22).ToString("d"), day.ToString("d"));
        }
    }

}
