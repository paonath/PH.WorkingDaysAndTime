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
    public static class WorkingDateTimeExtension
    {
        /// <summary>
        /// Returns a new System.DateTime that adds the <param name="days">specified number of working days</param> 
        /// to the value of this instance.
        /// Counting works only forward.
        /// </summary>
        /// <param name="dateTime">this instance of DateTime</param>
        /// <param name="days">number of working days to add</param>
        /// <param name="datesToExclude">List of holidays to skip calculating result</param>
        /// <param name="workDaysOfTheWeeks">List of weekdays that are actually working days</param>
        /// <returns>
        /// An object whose value is the sum of the date and time represented 
        /// by this instance and the number of working days represented by value.
        /// </returns>
        public static DateTime AddWorkingDays(this DateTime dateTime, int days
            , List<DateTime> datesToExclude , List<DayOfWeek> workDaysOfTheWeeks 
            )
        {
            DateTime end = dateTime;
            int y = end.Year;
            days = Math.Abs(days);
            var originalList = new List<DateTime>();
            datesToExclude.ForEach(it =>
            {
                originalList.Add(it);
            });
            var datesToExcludeList = datesToExclude;

            for (int i = 0; i < days; i++)
            {
                bool moreAdd = true;

                while (moreAdd)
                {
                    end = end.AddDays(1);
                    if (y < end.Year)
                    {
                        y = end.Year;
                        //regenerate list for new year
                        List<DateTime> newExclusions = new List<DateTime>();
                        originalList.ForEach(d =>
                        {
                            try
                            {
                                newExclusions.Add(new DateTime(y, d.Month, d.Day));

                            }
                            catch
                            {
                            }
                            
                        });

                        datesToExcludeList.AddRange(newExclusions);
                    }

                    //check if current is a workingDay
                    if (workDaysOfTheWeeks.Contains(end.DayOfWeek))
                    {
                        if (datesToExcludeList.Count == 0)
                        {
                            moreAdd = false;
                        }
                        else
                        {
                            var holiDayInList = datesToExcludeList
                                .FirstOrDefault(x => x.Date == end.Date);

                            if (holiDayInList.Date != DateTime.MinValue.Date)
                            {
                                datesToExcludeList.Remove(holiDayInList);
                            }
                            else
                            {
                                
                                    moreAdd = false;
                                
                            }
                        }
                    }
                }
            }
            return end;
        }


        
    }
}
