using System;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// A Slice of work-time.
    /// </summary>
    public class WorkTimeSpan
    {
        /// <summary>
        /// Starting Time for Work
        /// </summary>
        public TimeSpan Start { get; set; }
        /// <summary>
        /// End Time for Work
        /// </summary>
        public TimeSpan End { get; set; }

        public WorkTimeSpan()
        {
            
        }

        public WorkTimeSpan(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }

        
    }
}