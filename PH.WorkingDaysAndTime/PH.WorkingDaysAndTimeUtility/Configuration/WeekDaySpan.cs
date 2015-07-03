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

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// A Week.
    /// </summary>
    public class WeekDaySpan
    {
        /// <summary>
        /// Get if all days have the same amount of working hours.
        /// </summary>
        public bool Symmetrical {
            get { return GetIfsymmetrical(); }
        }

        /// <summary>
        /// Find differences between WorkingMinutesPerDay and return True if none.
        /// 
        /// </summary>
        /// <returns>True if no differences.</returns>
        private bool GetIfsymmetrical()
        {
            bool r = true;
            
            foreach (var wd in WorkDays)
            {
                if (wd.Value.IsWorkingDay)
                {
                    var c = (from x in WorkDays
                        where x.Key != wd.Key
                              && x.Value.IsWorkingDay == true
                              && x.Value.WorkingMinutesPerDay != wd.Value.WorkingMinutesPerDay
                        select x.Key).Count();
                    if (c > 0)
                    {
                        r = false;
                        break;
                    }
                }
                
            }
            return r;
        }

        /// <summary>
        /// Days representation of Work time
        /// </summary>
        public Dictionary<DayOfWeek, WorkDaySpan> WorkDays { get; set; }
    }
}