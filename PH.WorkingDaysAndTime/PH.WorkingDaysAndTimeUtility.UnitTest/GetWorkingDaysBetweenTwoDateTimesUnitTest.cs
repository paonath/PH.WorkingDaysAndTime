using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    [TestClass]
    public class GetWorkingDaysBetweenTwoDateTimesUnitTest 
        : BaseTest
    {

        [TestMethod]
        [TestCategory("GetWorkingDaysBetweenTwoDateTimes")]
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



            Assert.AreEqual(0, differences1.Count());
            Assert.AreEqual(0, differences2.Count());


            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        [TestCategory("GetWorkingDaysBetweenTwoDateTimes")]
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
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e, false);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.AreEqual(0, differences1.Count());
            Assert.AreEqual(0, differences2.Count());

            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        [TestCategory("GetWorkingDaysBetweenTwoDateTimes")]
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
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e, false);

            var result = r.Select(x => x.Date).OrderByDescending(x => x).ToList();

            var differences1 = result.Except(expected);
            var differences2 = expected.Except(result);



            Assert.AreEqual(0, differences1.Count());
            Assert.AreEqual(0, differences2.Count());

            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        [TestCategory("GetWorkingDaysBetweenTwoDateTimes")]
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


    }
}
