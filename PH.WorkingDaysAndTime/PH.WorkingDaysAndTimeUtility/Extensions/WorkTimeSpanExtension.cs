using System;
using System.Collections.Generic;
using System.Text;
using PH.WorkingDaysAndTimeUtility.Configuration;

namespace PH.WorkingDaysAndTimeUtility.Extensions
{
    public static class WorkTimeSpanExtension
    {
        public static bool IsWorkInstant(this WorkTimeSpan span, int hours, int minutes)
        {
            DateTime d0 = new DateTime(2000,1,3,span.Start.Hours,span.Start.Minutes,0);
            DateTime d1 = new DateTime(2000,1,3,span.End.Hours,span.End.Minutes,0);
            DateTime d2 = new DateTime(2000,1,3,hours,minutes,0);

            return d0 <= d2 && d2 <= d1;
        }
    }
}
