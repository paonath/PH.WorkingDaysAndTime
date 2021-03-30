using System;
using PH.Time;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    public class TimeSlice 
    {
        public object Identifier { get; set; }

        /// <summary>
        /// <c>true</c> if working time, otherwise <c>false</c>
        /// </summary>
        public bool IsWorkingTime { get; set; }

        /// <summary>
        /// A Scale factor for not-working time (extra-time factor)
        /// </summary>
        public double Factor { get; set; }

        /// <summary>
        /// Begin Time for Time Slice
        /// </summary>
        public Time.Time Begin { get; set; }

        /// <summary>
        /// End Time for Time Slice
        /// </summary>
        public Time.Time End { get; set; }

        public TimeSlice(object identifier,int beginHours, int beginMinutes
                         , int endHours, int endMinutes, bool isWorkingTime = true, double timeFactor = 1.0)
            : this(identifier,new Time.Time(beginHours, beginMinutes), new Time.Time(endHours, endMinutes), isWorkingTime, timeFactor)
        {
        }

        public TimeSlice(int beginHours, int beginMinutes
                         , int endHours, int endMinutes, bool isWorkingTime = true, double timeFactor = 1.0)
            : this(new Time.Time(beginHours, beginMinutes), new Time.Time(endHours, endMinutes), isWorkingTime, timeFactor)
        {

        }

        public TimeSlice(Time.Time begin, Time.Time end, bool isWorkingTime = true, double factor = 1.0)
            : this($"{begin:t} to {end:t}", begin, end, isWorkingTime, factor)
        {

        }
        public TimeSlice(object identifier, Time.Time begin, Time.Time end, bool isWorkingTime = true, double factor = 1.0)
        {
            Identifier = identifier;

            if (!(begin < end))
            {
                throw new ArgumentException($"end must be greather than begin!");
            }

            
            Begin         = begin;
            End           = end;
            IsWorkingTime = isWorkingTime;
            Factor        = factor;
        }



        /// <summary>Gets the empty value: not working time from 0:00:00 to 23:59:59 with default factor.</summary>
        /// <value>not working <see cref="Time"/></value>
        public static TimeSlice Empty => new TimeSlice(Time.Time.MinValue, Time.Time.MinValue);

        //public int CompareTo(object obj)
        //{
        //    var x = (TimeSlice) obj;
        //    return CompareTo(x);
        //}

        //public int CompareTo(TimeSlice other)
        //{
        //    var beginComparison = Begin.CompareTo(other.Begin);
        //    if (beginComparison != 0)
        //    {
        //        return beginComparison;
        //    }

        //    return End.CompareTo(other.End);
        //}


        //internal double OrderdValue => 1000000000 + Begin.GetMilliseconds() + End.GetMilliseconds();
    }

    public class WorkTimeSlice : TimeSlice
    {
        public WorkTimeSlice(int beginHours, int beginMinutes, int endHours, int endMinutes, double timeFactor = 1) 
            : base(beginHours, beginMinutes, endHours, endMinutes, true, timeFactor)
        {
        }

        public WorkTimeSlice(object identifier,int beginHours, int beginMinutes, int endHours, int endMinutes, double timeFactor = 1) 
            : base(identifier,beginHours, beginMinutes, endHours, endMinutes, true, timeFactor)
        {
        }



        public WorkTimeSlice(Time.Time begin, Time.Time end, double factor = 1)
            : base(begin, end, true, factor)
        {
        }
        public WorkTimeSlice(object identifier,Time.Time begin, Time.Time end, double factor = 1)
            : base(identifier,begin, end, true, factor)
        {
        }
    }
    
    public class OffWorkTimeSlice : TimeSlice
    {
        public OffWorkTimeSlice(int beginHours, int beginMinutes, int endHours, int endMinutes, double timeFactor = 1.2) 
            : base(beginHours, beginMinutes, endHours, endMinutes, false, timeFactor)
        {
        }
        public OffWorkTimeSlice(object identifier,int beginHours, int beginMinutes, int endHours, int endMinutes, double timeFactor = 1.2) 
            : base(identifier,beginHours, beginMinutes, endHours, endMinutes, false, timeFactor)
        {
        }

        public OffWorkTimeSlice(Time.Time begin, Time.Time end, double factor = 1.2) 
            : base(begin, end, false, factor)
        {
        }

        public OffWorkTimeSlice(object identifier,Time.Time begin, Time.Time end, double factor = 1.2) 
            : base(identifier,begin, end, false, factor)
        {
        }
    }

    public class ComputedOffWorkTimeSlice : OffWorkTimeSlice
    {
        public bool Computed => true;

        internal ComputedOffWorkTimeSlice(int beginHours, int beginMinutes, int endHours, int endMinutes, double timeFactor = 1.2) : base(beginHours, beginMinutes, endHours, endMinutes, timeFactor)
        {
        }

        internal ComputedOffWorkTimeSlice(object identifier, int beginHours, int beginMinutes, int endHours, int endMinutes, double timeFactor = 1.2) : base(identifier, beginHours, beginMinutes, endHours, endMinutes, timeFactor)
        {
        }

        internal ComputedOffWorkTimeSlice(Time.Time begin, Time.Time end, double factor = 1.2) : base(begin, end, factor)
        {
        }

        internal ComputedOffWorkTimeSlice(object identifier, Time.Time begin, Time.Time end, double factor = 1.2) : base(identifier, begin, end, factor)
        {
        }
    }
}