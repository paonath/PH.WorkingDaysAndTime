using System;
using System.Collections.Generic;
using System.Linq;

namespace PH.WorkingDaysAndTimeUtility
{
    public class WeekDaySpan
    {
        public bool Symmetrical {
            get { return GetIfsymmetrical(); }
        }

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
        public Dictionary<DayOfWeek, WorkDaySpan> WorkDays { get; set; }
        //public List<KeyValuePair<DayOfWeek, WorkDaySpan>> WorkDays { get; set; }
    }
}