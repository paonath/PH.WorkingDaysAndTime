using System;
using System.Collections.Generic;

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


    public class TimeSlotConfig
    {
        public Dictionary<DayOfWeek,List<TimeSlot>> TimesDictionary { get; set; }
        public List<TimeSlot> HolyDaySlots { get; set; }
        public TimeSlotConfig()
        {
            TimesDictionary = new Dictionary<DayOfWeek, List<TimeSlot>>();
            HolyDaySlots    = new List<TimeSlot>();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PH.WorkingDaysAndTimeUtility.Configuration.TimeSlot" />
    internal class DateTimeSlot : TimeSlot
    {
        /// <summary>Gets or sets the date time start.</summary>
        /// <value>The date time start.</value>
        public DateTime DateTimeStart { get; set; }

        /// <summary>Gets or sets the date time end.</summary>
        /// <value>The date time end.</value>
        public DateTime DateTimeEnd { get; set; }

        /// <summary>Builds the specified slot.</summary>
        /// <param name="slot">The slot.</param>
        /// <param name="d">The datetime.</param>
        /// <returns></returns>
        public static DateTimeSlot Build(TimeSlot slot, DateTime d)
        {
            var s = new DateTime(d.Year, d.Month, d.Day, slot.Start.Hours, slot.Start.Minutes, slot.Start.Seconds);
            var e = new DateTime(d.Year, d.Month, d.Day, slot.End.Hours, slot.End.Minutes, slot.End.Seconds);

            return new DateTimeSlot()
            {
                DateTimeEnd   = e,
                DateTimeStart = s,
                Start         = slot.Start,
                End           = slot.End,
                Factor        = slot.Factor,
                Key           = slot.Key,
                HolyDayFactor = slot.HolyDayFactor
            };
        }
        /// <summary>If Datetime in the slot.</summary>
        /// <param name="d">The DateTime.</param>
        /// <returns><c>true</c> if in slot</returns>
        internal bool InSlot(DateTime d)
        {
            
            return d <= DateTimeEnd && d >= DateTimeStart;
        }
    }

    /// <summary>
    /// A Slice of time: for calculate extra-work hours (in <see cref="TimeSpan"/>)
    /// </summary>
    public class TimeSlot
    {
        /// <summary>Gets or sets the factor if work on this time slice.</summary>
        /// <value>The factor.</value>
        public double Factor { get; set; }

        /// <summary>Gets or sets the holy day factor if work on this time slice.</summary>
        /// <value>The holy day factor.</value>
        public double HolyDayFactor { get; set; }

        /// <summary>
        /// Starting Time for No-Work time slice
        /// </summary>
        public TimeSpan Start { get; set; }
        /// <summary>
        /// End Time for No-Work time slice
        /// </summary>
        public TimeSpan End { get; set; }

        public string Key { get; set; }

        public TimeSlot()
        {
             
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSlot"/> class.
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <param name="holyDayFactor">The holy day factor.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public TimeSlot(double factor, double holyDayFactor, TimeSpan start, TimeSpan end)
            :this(null,factor,holyDayFactor,start,end)
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSlot"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="factor">The factor.</param>
        /// <param name="holyDayFactor">The holy day factor.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public TimeSlot(string key,double factor, double holyDayFactor, TimeSpan start, TimeSpan end)
        {
            Key           = key;
            Factor        = factor;
            HolyDayFactor = holyDayFactor;
            Start         = start;
            End           = end;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return GetHashCode() == obj?.GetHashCode();
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return ($"TimeSlot  {Start:c} {End:c}").GetHashCode();
        }

        /// <summary>If Datetime in the slot.</summary>
        /// <param name="d">The DateTime.</param>
        /// <returns><c>true</c> if in slot</returns>
        internal bool InSlot(DateTime d)
        {
            var s = new DateTime(d.Year, d.Month, d.Day, Start.Hours, Start.Minutes, Start.Seconds);
            var e = new DateTime(d.Year, d.Month, d.Day, End.Hours, End.Minutes, End.Seconds);
            return d <= e && d >= s;
        }
    }

    public class WorkeTimeSlice
    {
        public DateTime Start { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        public double Factor { get; internal set; }
        public bool OnWorkTime { get; internal set; }
        public bool OnHolyDay { get; internal set; }
        public TimeSlot TimeSlot { get; internal set; }
    }

    public class WorkedTimeSliceResult
    {
        public DateTime Start { get; internal set; }
        public DateTime End { get; internal set; }
        public TimeSpan TotalDuration { get; internal set; }
        public WorkeTimeSlice[] WorkSlices { get; internal set; }

        public WorkedTimeSliceResult()
        {
            WorkSlices = new WorkeTimeSlice[0];
        }
    }

}