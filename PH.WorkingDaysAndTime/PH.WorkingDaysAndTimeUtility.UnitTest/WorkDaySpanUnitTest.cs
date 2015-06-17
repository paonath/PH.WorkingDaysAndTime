using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    [TestClass]
    public class WorkDaySpanUnitTest
    {
        [TestMethod]
        public void TestMethod_Monday()
        {
            var wts1 = new WorkTimeSpan() { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(13, 30, 0) };
            var wts2 = new WorkTimeSpan() { Start = new TimeSpan(14, 30, 0), End = new TimeSpan(18, 0, 0) };
            var lMonday = new List<WorkTimeSpan>() { wts1, wts2 };

            var monday = new WorkDaySpan() { TimeSpans = lMonday };

            var totalMinutesForWork = monday.WorkingMinutesPerDay;

            double minutesIn8Hday = (double)(8 * 60);

            Assert.IsTrue(monday.IsWorkingDay);

            Assert.AreEqual(minutesIn8Hday, totalMinutesForWork);
        }

        [TestMethod]
        public void TestMethod_Sunday_NotWorkingDay()
        {
            var sunday = new WorkDaySpan() { };
            Assert.IsFalse(sunday.IsWorkingDay);

        }
    }

}
