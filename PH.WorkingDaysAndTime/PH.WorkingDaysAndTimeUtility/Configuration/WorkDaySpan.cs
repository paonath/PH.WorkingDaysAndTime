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