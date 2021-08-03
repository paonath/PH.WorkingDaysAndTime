using System;
using System.Collections.Generic;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// A Calculated-runtime Holidays: return a list og holidays based on method <see cref="CalculateList"/>
    /// </summary>
    /// <seealso cref="PH.WorkingDaysAndTimeUtility.Configuration.CalculatedHoliDay" />
    public abstract class MultiCalculatedHoliDay : CalculatedHoliDay
    {
        
        /// <summary>
        /// It returns an instance of the data by year provided
        /// </summary>
        /// <param name="year">year provided</param>
        /// <returns>DateTime</returns>
        [Obsolete("For MultiCalculatedHoliDay use 'List<DateTime> CalculateList(int year);'", true)]
        #pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override DateTime Calculate(int year)
            #pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            throw new
                NotSupportedException($"For {nameof(MultiCalculatedHoliDay)} use 'List<DateTime> CalculateList(int year);'");
        }

        /// <summary>Calculates the list of HoliDays for the given year.</summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public abstract List<DateTime> CalculateList(int year);

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiCalculatedHoliDay"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="mont">The mont.</param>
        protected MultiCalculatedHoliDay(int day, int mont) : base(day, mont)
        {
        }
    }
}