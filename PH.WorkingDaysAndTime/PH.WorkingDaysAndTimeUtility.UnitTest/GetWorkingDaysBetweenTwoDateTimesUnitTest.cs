using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e, false);

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
            var r = utility.GetWorkingDaysBetweenTwoDateTimes(s, e, false);

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


    }
}
