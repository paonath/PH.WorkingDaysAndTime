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
        /// True if all days have the same amount of working hours.
        /// </summary>
        public bool Symmetrical => GetIfsymmetrical();

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
        /// Dictionary match Working Days and theirs Working Hours.
        /// </summary>
        public Dictionary<DayOfWeek, WorkDaySpan> WorkDays { get; set; }

        public WeekDaySpan():this(new Dictionary<DayOfWeek, WorkDaySpan>())
        {
            
        }

        public WeekDaySpan(Dictionary<DayOfWeek, WorkDaySpan> dictionary)
        {
            WorkDays = dictionary;
        }

        

        public WeekDaySpan Day(IEnumerable<KeyValuePair<DayOfWeek,WorkDaySpan>> values)
        {
            foreach (var pair in values)
            {
                WorkDays.Add(pair.Key, pair.Value);
            }

            return this;
        }

        public WeekDaySpan Day(KeyValuePair<DayOfWeek, WorkDaySpan> value) => Day(new List<KeyValuePair<DayOfWeek, WorkDaySpan>>() {value});

        public WeekDaySpan Day(DayOfWeek d, WorkDaySpan w) => Day(new KeyValuePair<DayOfWeek, WorkDaySpan>(d, w));


        public static WeekDaySpan CreateSymmetricalConfig(WorkDaySpan span, DayOfWeek[] days)
        {
            var e = days.Select(x => new KeyValuePair<DayOfWeek, WorkDaySpan>(x, span)).ToList();
            return new WeekDaySpan().Day(e);

        }

    }
}