/* 
 * Copyright (c) 2015, paonath
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of PH.WorkingDaysAndTime nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PH.WorkingDaysAndTimeUtility.Configuration;

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

        [TestMethod]
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

            Assert.AreEqual(e, r);
        }

        [TestMethod]
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

            Assert.AreEqual(e, r);
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
