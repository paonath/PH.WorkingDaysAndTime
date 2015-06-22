using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    [TestClass]
    public class WorkingDaysAndTimeUtilityUnitTest
    {

        [TestMethod]
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

            Assert.IsNotNull(f0);
            Assert.IsNotNull(f1);

        }

        [TestMethod]
        public void Add_1_Day_From_NoWorkingDay_Fail_On_Calculate()
        {
            var sunday = new DateTime(2015, 6, 14);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());
            Exception f0 = null;
            try
            {
                var fake = utility.AddWorkingDays(sunday, 4);
            }
            catch (Exception ex)
            {
                f0 = ex;
            }

            Assert.IsNotNull(f0);
        }

        [TestMethod]
        public void Add_1_Day_On_Simple_Week_From_2015_06_16_With_No_Holidays()
        {
            var d = new DateTime(2015, 6, 16);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());

            var r = utility.AddWorkingDays(d, 1);
            var expected = new DateTime(2015, 6, 17);
            Assert.AreEqual(expected, r);

        }

        [TestMethod]
        public void Add_1_Day_On_Simple_Week_From_2015_06_19_With_No_Holidays()
        {
            var d = new DateTime(2015, 6, 19);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());

            var r = utility.AddWorkingDays(d, 1);
            var expected = new DateTime(2015, 6, 22);
            Assert.AreEqual(expected, r);

        }

        [TestMethod]
        public void Add_1_Day_On_Simple_With_2_HoliDays()
        {
            var d = new DateTime(2015, 6, 16);
            var holidays = new List<HoliDay>() { new HoliDay(17, 6), new HoliDay(18, 6), new EasterMonday() };
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, holidays);

            var r = utility.AddWorkingDays(d, 1);
            var expected = new DateTime(2015, 6, 19);
            Assert.AreEqual(expected, r);

        }

        [TestMethod]
        public void Invalid_Start_Time_Fail_On_Calculate()
        {
            var d = new DateTime(2015, 6, 16, 2, 3, 4);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());
            Exception f0 = null;
            try
            {
                var fake = utility.AddWorkingHours(d, 1);
            }
            catch (Exception exception)
            {

                f0 = exception;
            }

            Assert.IsNotNull(f0);
        }

        [TestMethod]
        public void Check_Symmetrical_And_NotSummetrical_Week()
        {
            var symmetrical = GetSimpleWeek();
            var notSymm = GetAWeek();

            Assert.IsTrue(symmetrical.Symmetrical);
            Assert.IsFalse(notSymm.Symmetrical);
        }

        [TestMethod]
        public void Add_16Hours_On_8workingHoursDay_Will_Add_2_Days()
        {
            var d = new DateTime(2015, 6, 16, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());
            var r = utility.AddWorkingHours(d, 16);
            var e = new DateTime(2015, 6, 18, 9, 0, 0);
            Assert.AreEqual(e, r);


        }

        [TestMethod]
        public void Add_17Hours_On_8workingHoursDay_Will_Add_2_Days_and_1h()
        {
            var d = new DateTime(2015, 6, 16, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());
            var r = utility.AddWorkingHours(d, 17);
            var e = new DateTime(2015, 6, 18, 10, 0, 0);
            Assert.AreEqual(e, r);

        }

        [TestMethod]
        public void Add_33Hours_On_8workingHoursDay_Will_Add_3_Days_and_1h()
        {
            var d = new DateTime(2015, 6, 16, 9, 45, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, new List<HoliDay>());
            var r = utility.AddWorkingHours(d, 33);
            var e = new DateTime(2015, 6, 22, 10, 45, 0);
            Assert.AreEqual(e, r);

        }

        [TestMethod]
        public void Adding_3_WotkDays_To_31_Dec_2015_Will_Return_7_Jan_2016()
        {
            var d = new DateTime(2015, 12, 31, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.AddWorkingDays(d, 3);
            var e = new DateTime(2016, 1, 7, 9, 0, 0);
            Assert.AreEqual(e, r);

        }

        [TestMethod]
        public void Get_List_Of_WorkingDays_Between_31_Dec_2015_And_7_Jan_2016_Will_Get_A_List_Of_4()
        {
            var s = new DateTime(2015, 12, 31, 9, 0, 0);
            var e = new DateTime(2016, 1, 7, 9, 0, 0);
            var expected =
                (new List<DateTime>() {s, new DateTime(2016, 1, 4), new DateTime(2016, 1, 5), e})
                    .Select(x => x.Date)
                    .OrderByDescending(
                        x => x).ToList();
            
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.AreEqual(0 , differences1.Count());
            Assert.AreEqual(0, differences2.Count());


            Assert.AreEqual(4, result.Count);
        }



        [TestMethod]
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
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e,false);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.AreEqual(0, differences1.Count());
            Assert.AreEqual(0, differences2.Count());

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
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
            var italiansHoliDays = new List<HoliDay>()
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

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Method_Used_In_Readme_Code_Example_2()
        {

            var weekConf = GetSimpleWeek();


            var start = new DateTime(2015, 12, 31, 9, 0, 0);
            var end = new DateTime(2016, 1, 7, 9, 0, 0);
            
            //omitted configurations and holidays...
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            //r is a workdays List<DateTime> between Dec 31 and Jan 7.
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(start, end);

            Assert.IsNotNull(r);
        }



        [TestMethod]
        public void Stress_Test_Adding_3000_WotkDays_To_31_Dec_2015()
        {
            var d = new DateTime(2015, 12, 31, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());
            var r = utility.AddWorkingDays(d, 3000);
            
            Assert.IsNotNull(r);
        }

        /// <summary>
        /// Warning: this is very-very-very slow test.
        /// On my machine will work...just for fun....
        /// </summary>
        [TestMethod]
        public void Stress_Test_Adding_5_WorkDays_To_29_Feb_2012_With_CrazyHolyDaysList()
        {
            var d = new DateTime(2012, 2, 29, 9, 0, 0);
            var weekConf = GetSimpleWeek();
            var crazyList = GetCrazyListForStressTest();

            var utility = new WorkingDaysAndTimeUtility(weekConf, crazyList);
            var r = utility.AddWorkingDays(d, 5);

            Assert.IsNotNull(r);
        }


        #region config ...
        private WeekDaySpan GetSimpleWeek()
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
        private List<HoliDay> GetItalianHolidays()
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


        private WeekDaySpan GetAWeek()
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

        private List<HoliDay> GetCrazyListForStressTest()
        {
            DateTime st = new DateTime(2015,1,1);
            List<HoliDay> l = new List<HoliDay>();
            for (int i = 0; i < 365; i++)
            {
                var d = st.AddDays(i);
                l.Add(new HoliDay(d.Day,d.Month));
            }
            return l;
        }

        #endregion
    }

}
