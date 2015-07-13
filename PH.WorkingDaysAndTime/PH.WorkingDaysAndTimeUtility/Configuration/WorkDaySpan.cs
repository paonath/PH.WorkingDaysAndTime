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
using System.Collections.Generic;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// Representation of a Work Day.
    /// </summary>
    public class WorkDaySpan
    {
        /// <summary>
        /// Work times slices.
        /// </summary>
        public List<WorkTimeSpan> TimeSpans { get; set; }

        /// <summary>
        /// Get Working Minutes Per Day
        /// </summary>
        public double WorkingMinutesPerDay {
            get { return GetWorkingMinutesPerDay(); }
        }

        /// <summary>
        /// True if working day.
        /// </summary>
        public bool IsWorkingDay {
            get { return WorkingMinutesPerDay > (double)0; }
        }

        /// <summary>
        /// Cycle working-time slices and get total minutes.
        /// </summary>
        /// <returns></returns>
        private double GetWorkingMinutesPerDay()
        {
            double totalMinutes = 0;
            if (null != TimeSpans &&  TimeSpans.Count > 0)
            {
                TimeSpans.ForEach(t =>
                {
                    totalMinutes += t.End.Subtract(t.Start).TotalMinutes;
                });
            }
            return totalMinutes;
        }
    }
}