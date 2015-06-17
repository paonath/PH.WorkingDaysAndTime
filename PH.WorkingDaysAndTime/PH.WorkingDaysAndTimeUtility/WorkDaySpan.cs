using System.Collections.Generic;

namespace PH.WorkingDaysAndTimeUtility
{
    public class WorkDaySpan
    {
        public List<WorkTimeSpan> TimeSpans { get; set; }
        public double WorkingMinutesPerDay {
            get { return GetWorkingMinutesPerDay(); }
        }

        public bool IsWorkingDay {
            get { return WorkingMinutesPerDay > (double)0; }
        }

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