using System;
using PH.WorkingDaysAndTimeUtility.Configuration;

namespace PH.WorkingDaysAndTimeUtility.Extensions
{
    public static class WorkDaySpanExtensions
    {
        internal static bool IsWorkDateTime(this WorkDaySpan span, DateTime d)
        {
            bool r = false;
            foreach (var spanTimeSpan in span.TimeSpans)
            {
                r = spanTimeSpan.IsWorkInstant(d.Hour, d.Minute);
                if(r)
                    break;
            }

            return r;
        }
    }
}