using System;

namespace PH.WorkingDaysAndTimeUtility
{
    public class HolyDay
    {
        public int Day { get; private set; }
        public int Month { get; private set; }
        

        public HolyDay(int day,int mont)
        {
            Day = day;
            Month = mont;
            
        }

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