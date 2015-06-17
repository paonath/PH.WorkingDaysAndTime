using System;

namespace PH.WorkingDaysAndTimeUtility
{
    /// <summary>
    /// Holiday: a non-working day.
    /// 
    /// This is a generic-instance, with <see cref="HoliDay.Day">Day</see> 
    /// and <see cref="HoliDay.Month">Month</see>.
    /// </summary>
    public class HoliDay
    {
        /// <summary>
        /// Day
        /// </summary>
        public int Day { get; private set; }
        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; private set; }
        

        public HoliDay(int day,int mont)
        {
            Day = day;
            Month = mont;
            
        }

        /// <summary>
        /// It returns an instance of the data by year provided
        /// </summary>
        /// <param name="year">year provided</param>
        /// <returns>Holiday DateTime</returns>
        public virtual DateTime Calculate(int year)
        {
            return new DateTime(year,this.Month,this.Day);
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Day: {0} Month: {1}", this.Day, this.Month);

        }
    }
}