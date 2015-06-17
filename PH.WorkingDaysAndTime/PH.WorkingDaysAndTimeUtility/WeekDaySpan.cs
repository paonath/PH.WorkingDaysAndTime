using System;
using System.Collections.Generic;
using System.Linq;

namespace PH.WorkingDaysAndTimeUtility
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