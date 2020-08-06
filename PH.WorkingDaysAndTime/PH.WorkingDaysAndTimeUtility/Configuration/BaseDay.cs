using System;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    public abstract class BaseDay
    {
        private readonly int _day;

        /// <summary>
        /// Day
        /// </summary>
        public int Day => _day;

        private readonly int _month;

        /// <summary>
        /// Month
        /// </summary>
        public int Month => _month;

        protected BaseDay(int day,int mont)
        {
            _day   = day;
            _month = mont;

        }

        /// <summary>
        /// Perform check on valid Day and Month combination
        ///
        /// <exception cref="ArgumentOutOfRangeException">The date is not a valid Day and Month combination</exception>
        /// </summary>
        protected void PerformCheckOnStart()
        {
            var d = new DateTime(2016, Month,Day);
        }

        public abstract DateTime Calculate(int year);

        public override bool Equals(object obj)
        {
            return obj?.GetHashCode() == GetHashCode();
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return $"{_month}-{_day}".GetHashCode();
        }
    }
}