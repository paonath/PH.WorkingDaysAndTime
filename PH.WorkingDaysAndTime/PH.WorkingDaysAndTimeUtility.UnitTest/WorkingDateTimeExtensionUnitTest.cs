using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    [TestClass]
    public class WorkingDateTimeExtensionUnitTest
    {
        [TestMethod]
        public void Add_1_Day_To_Jun_23_2015_With_No_Holidays_Will_Return_Jun_24()
        {
            DateTime d = new DateTime(2015,6,23);
            DateTime e = new DateTime(2015, 6, 24);
            List<DayOfWeek> week = GetWorkWeek();
            DateTime r = d.AddWorkingDays(1, new List<DateTime>(), week);

            Assert.AreEqual(e,r);
        }

        [TestMethod]
        public void Add_4_Day_To_Jun_23_2015_With_No_Holidays_Will_Return_Jun_29()
        {
            DateTime d = new DateTime(2015, 6, 23);
            DateTime e = new DateTime(2015, 6, 29);
            List<DayOfWeek> week = GetWorkWeek();
            DateTime r = d.AddWorkingDays(4, new List<DateTime>(), week);

            Assert.AreEqual(e, r);
        }


        private List<DayOfWeek> GetWorkWeek()
        {
            return new List<DayOfWeek>()
            {
                DayOfWeek.Monday , DayOfWeek.Tuesday , DayOfWeek.Wednesday
                , DayOfWeek.Thursday , DayOfWeek.Friday
            };
        }
    }
}
