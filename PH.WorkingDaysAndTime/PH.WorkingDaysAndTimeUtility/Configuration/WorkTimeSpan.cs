using System;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// Simple Time representation
    /// </summary>
    public interface ITime
    {
        int Hours { get; set; }
        int Minutes { get; set; }
        int Seconds { get; set; }
        bool IsWorkingTime { get; }
        
    }

    public struct WorkTime : ITime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public bool IsWorkingTime => true;
    }

    public struct OffTime : ITime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public bool IsWorkingTime => false;
    }



    /// <summary>
    /// A Slice of work-time.
    /// </summary>
    [Obsolete("",true)]
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