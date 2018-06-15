
using System;
using System.Collections.Generic;

using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    
    public class WorkingDateTimeExtensionUnitTest
    {
        [Fact]
        public void Add_1_Day_To_Jun_23_2015_With_No_Holidays_Will_Return_Jun_24()
        {
            DateTime d = new DateTime(2015,6,23);
            DateTime e = new DateTime(2015, 6, 24);
            List<DayOfWeek> week = GetWorkWeek();
            DateTime r = d.AddWorkingDays(1, new List<DateTime>(), week);

            Assert.Equal(e,r);
        }

        [Fact]
        public void Add_4_Day_To_Jun_23_2015_With_No_Holidays_Will_Return_Jun_29()
        {
            DateTime d = new DateTime(2015, 6, 23);
            DateTime e = new DateTime(2015, 6, 29);
            List<DayOfWeek> week = GetWorkWeek();
            DateTime r = d.AddWorkingDays(4, new List<DateTime>(), week);

            Assert.Equal(e, r);
        }

        [Fact]
        public void Add_4_Day_To_Jun_23_2015_With_Holidays_Will_Return_Jun_29()
        {
            DateTime d = new DateTime(2015, 6, 23);
            DateTime e = new DateTime(2015, 6, 29);

            List<DayOfWeek> week = GetWorkWeek();
            List<DateTime> holidays = new List<DateTime>();
            
            GetItalianHolidays().ForEach(h =>
            {
                holidays.Add(h.Calculate(2015));
            });

            DateTime r = d.AddWorkingDays(4, holidays, week);

            Assert.Equal(e, r);
        }

        [Fact]
        public void Add_4_Day_To_Jun_1_2015_With_Holidays_Will_Return_Jun_8()
        {
            DateTime d = new DateTime(2015, 6, 1);
            DateTime e = new DateTime(2015, 6, 8);

            List<DayOfWeek> week = GetWorkWeek();
            List<DateTime> holidays = new List<DateTime>();

            GetItalianHolidays().ForEach(h =>
            {
                holidays.Add(h.Calculate(2015));
            });

            DateTime r = d.AddWorkingDays(4, holidays, week);

            Assert.Equal(e, r);
        }


        #region prv...

        private List<DayOfWeek> GetWorkWeek()
        {
            return new List<DayOfWeek>()
            {
                DayOfWeek.Monday , DayOfWeek.Tuesday , DayOfWeek.Wednesday
                , DayOfWeek.Thursday , DayOfWeek.Friday
            };
        }

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
        
        #endregion
    }
}
