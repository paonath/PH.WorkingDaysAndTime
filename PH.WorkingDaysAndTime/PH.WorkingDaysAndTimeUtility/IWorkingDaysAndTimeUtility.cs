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

namespace PH.WorkingDaysAndTimeUtility
{
    /// <summary>
    /// A tiny utility for calculating work days and work time.
    /// 
    /// Can add n work-days to a DateTime;
    /// Can add n work-hours to a DateTime;
    /// Can get a List of work-DateTime between 2 dates;
    /// </summary>
    public interface IWorkingDaysAndTimeUtility
    {
        /// <summary>
        /// The method add <param name="days">n days</param> to given <param name="start">start Date</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting Date</param>
        /// <param name="days">Numer of days to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>First working-day after n occurences from given date</returns>
        DateTime AddWorkingDays(DateTime start, int days);

        /// <summary>
        /// The method add <param name="hours">n hours</param> to given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="hours">Number of hours to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>Result DateTime</returns>
        DateTime AddWorkingHours(DateTime start, double hours);

        /// <summary>
        /// The method add <param name="minutes">n minutes</param> to 
        /// given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="minutes">Number of hours to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>Result DateTime</returns>
        DateTime AddWorkingMinutes(DateTime start, double minutes);

        /// <summary>
        /// The method get list of working-days between <param name="start">start</param> and <param name="end">end</param>.
        /// </summary>
        /// <param name="start">Start working Date</param>
        /// <param name="end">End working Date</param>
        /// <param name="includeStartAndEnd">True if start and end are included in list (Default: True)</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>List of Working DateTime</returns>
        List<DateTime> GetWorkingDaysBetweenTwoDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true);
    }
}
