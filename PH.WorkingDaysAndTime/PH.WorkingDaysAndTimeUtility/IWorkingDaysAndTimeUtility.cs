using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.WorkingDaysAndTimeUtility
{
    /// <summary>
    /// A tiny utility for calculating work days and work time.
    /// 
    /// Can add n work-days to a DateTime;
    /// Can add n work-hours to a DateTime;
    /// Can get a List of work-DateTime between 2 dates;
    /// </summary>
    public interface IWorkingDaysAndTimeUtility
    {
        /// <summary>
        /// The method add <param name="days">n days</param> to given <param name="start">start Date</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting Date</param>
        /// <param name="days">Numer of days to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>First working-day after n occurences from given date</returns>
        DateTime AddWorkingDays(DateTime start, int days);

        /// <summary>
        /// The method add <param name="hours">n hours</param> to given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="hours">Number of hours to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>Result DateTime</returns>
        DateTime AddWorkingHours(DateTime start, double hours);

        /// <summary>
        /// The method add <param name="minutes">n minutes</param> to 
        /// given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="minutes">Number of hours to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>Result DateTime</returns>
        DateTime AddWorkingMinutes(DateTime start, double minutes);



        /// <summary>
        /// The method add <param name="timeSpan">n minutes</param> to 
        /// given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="timeSpan">Time Span - use <see cref="TimeSpan.TotalMinutes"/></param>
        // <returns>Result DateTime</returns>
        DateTime AddWorkingTimeSpan(DateTime start, TimeSpan timeSpan);


        /// <summary>
        /// The method get list of working-days between <param name="start">start</param> and <param name="end">end</param>.
        /// </summary>
        /// <param name="start">Start working Date</param>
        /// <param name="end">End working Date</param>
        /// <returns>List of Working DateTime</returns>
        List<DateTime> GetWorkingDaysBetweenTwoDateTimes(DateTime start, DateTime end);

        /// <summary>Gets the working days between two working date times.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="includeStartAndEnd">if set to <c>true</c> start and end are included in list (Default: <c>true</c>).</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns></returns>
        List<DateTime> GetWorkingDaysBetweenTwoWorkingDateTimes(DateTime start, DateTime end,
                                                                bool includeStartAndEnd = true);

        /// <summary>
        /// The method get True if given DateTime is a WorkingMoment and calculate previous Working-DateTime
        /// by minutes interval
        /// </summary>
        /// <param name="date">Argument to check if WorkingMoment</param>
        /// <param name="previousWorkingMoment">Previous Working-DateTime</param>
        /// <param name="minutesInterval">minutes to add for next and previous working moment</param>
        /// <returns>True if WorkingInstant</returns>
        bool IfWorkingMomentGettingPrevious(DateTime date, out DateTime previousWorkingMoment, double minutesInterval = 1);

        /// <summary>
        /// The method get True if given DateTime is a WorkingMoment and calculate next Working-DateTime
        /// by minutes interval
        /// </summary>
        /// <param name="date">Argument to check if WorkingMoment</param>
        /// <param name="nextWorkingMoment">Next Working-DateTime</param>
        /// <param name="minutesInterval">minutes to add for next and previous working moment</param>
        /// <returns>True if WorkingInstant</returns>
        bool IfWorkingMomentGettingNext(DateTime date, out DateTime nextWorkingMoment, double minutesInterval = 1);


        /// <summary>
        /// The method get True if given DateTime is a WorkingMoment and calculate next and previous Working-DateTime
        /// by minutes interval
        /// </summary>
        /// <param name="date">Argument to check if WorkingMoment</param>
        /// <param name="nextWorkingMoment">Next Working-DateTime</param>
        /// <param name="previousWorkingMoment">Previous Working-DateTime</param>
        /// <param name="minutesInterval">minutes to add for next and previous working moment</param>
        /// <returns>True if WorkingInstant</returns>
        bool IfWorkingMoment(DateTime date, out DateTime nextWorkingMoment, out DateTime previousWorkingMoment, double minutesInterval = 1);

        /// <summary>
        /// Determines whether is a work day the specified day(do not check for Hours/Minutes, jut Day).
        /// </summary>
        /// <param name="day">The day.</param>
        /// <returns>
        ///   <c>true</c> if is a work day the specified day; otherwise, <c>false</c>.
        /// </returns>
        bool IsAWorkDay(DateTime day);

    }
}
