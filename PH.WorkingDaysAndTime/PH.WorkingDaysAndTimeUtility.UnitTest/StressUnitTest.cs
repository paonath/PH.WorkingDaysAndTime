using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    [TestClass]
    public class StressUnitTest : BaseTest
    {

        [TestMethod]
        [TestCategory("stress")]
        public void Stress_Test_Adding_3000_WorkDays_To_31_Dec_2015()
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
        [TestCategory("stress")]
        public void Stress_Test_Adding_5_WorkDays_To_29_Feb_2012_With_CrazyHolyDaysList_Will_Get_2032_Feb_29()
        {
            var d = new DateTime(2012, 2, 29, 9, 0, 0);
            var e = new DateTime(2032, 2, 29);

            var weekConf = GetSimpleWeek();
            var crazyList = GetCrazyListForStressTest();

            var utility = new WorkingDaysAndTimeUtility(weekConf, crazyList);
            var r = utility.AddWorkingDays(d, 5);

            Assert.IsNotNull(r);
            Assert.AreEqual(e, r);
        }

        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_No_Symmetrical_Week()
        {
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 6, 22, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 150);
            var e = new DateTime(2015, 6, 29, 9, 0, 0);
            Assert.AreEqual(e, r);

        }
        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_1500_No_Symmetrical_Week()
        {
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 6, 22, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 1500);
            var e = new DateTime(2015, 8, 31, 9, 0, 0);
            Assert.AreEqual(e, r);

        }

        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_2100_No_Symmetrical_Week()
        {
            //20 il 27 luglio - 14 --
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 7, 27, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 2100);
            var e = new DateTime(2015, 11, 2, 9, 0, 0);
            Assert.AreEqual(e, r);

        }
        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_2700_No_Symmetrical_Week()
        {
            //20 il 27 luglio - 18 --
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 7, 27, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 2700);
            var e = new DateTime(2015, 11, 30, 9, 0, 0);
            Assert.AreEqual(e, r);

        }
        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_2760_No_Symmetrical_Week()
        {
            //20 il 27 luglio - 18 --
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 7, 27, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 2760);
            var e = new DateTime(2015, 12, 7
                , 9, 0, 0);
            Assert.AreEqual(e, r);

        }

        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_2820_No_Symmetrical_Week()
        {
            //20 il 27 luglio - 18 --
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 7, 27, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 2820);
            var e = new DateTime(2015, 12, 14
                , 9, 0, 0);
            Assert.AreEqual(e, r);

        }
        [TestMethod]
        [TestCategory("stress")]
        public void Test_Minutes_2911_No_Symmetrical_Week()
        {
            //20 il 27 luglio - 18 --
            //week with 150 working minutes 
            var weekConf = Get_Week_NotSymmetrical_With_2_DaysOf1_and_1_30_H();

            var d = new DateTime(2015, 7, 27, 9, 0, 0);
            var utility = new WorkingDaysAndTimeUtility(weekConf, GetItalianHolidays());

            var r = utility.AddWorkingMinutes(d, 2911);
            var e = new DateTime(2015, 12, 15
                , 9, 31, 0);
            Assert.AreEqual(e, r);

        }
        
        

    }
}
