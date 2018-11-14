using System;
using PH.WorkingDaysAndTimeUtility.Configuration;

namespace PH.WorkingDaysAndTimeUtility.Extensions
{
    public static class WeekDaySpanExtensions
    {
        public static bool IsWorkDateTime(this WeekDaySpan span, DateTime d)
        {
            if (!span.WorkDays.ContainsKey(d.DayOfWeek))
                return false;

            var wd = span.WorkDays[d.DayOfWeek];
            if (!wd.IsWorkingDay)
                return false;

            return wd.IsWorkDateTime(d);
        }
    }
}