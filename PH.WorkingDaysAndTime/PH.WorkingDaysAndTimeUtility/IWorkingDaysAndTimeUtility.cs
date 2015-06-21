using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.WorkingDaysAndTimeUtility
{
    public interface IWorkingDaysAndTimeUtility
    {
        /// <summary>
        /// The method add <param name="days">n days</param> to given <param name="start">start Date</param>.
        /// 
        /// </summary>
        /// <param name="start">Starting Date</param>
        /// <param name="days">Numer of days to add</param>
        /// <returns>First working-day after n occurences from given date</returns>
        DateTime AddWorkingDays(DateTime start, int days);

        /// <summary>
        /// The method add <param name="hours">n hours</param> to given <param name="start">start DateTime</param>
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="hours">Number of hours to add</param>
        /// <returns>Result DateTime</returns>
        DateTime AddWorkingHours(DateTime start, double hours);

        /// <summary>
        /// The method get list of working-days between <param name="start">start</param> and <param name="end">end</param>.
        /// </summary>
        /// <param name="start">Start working Date</param>
        /// <param name="end">End working Date</param>
        /// <param name="includeStartAndEnd">True if start and end are included in list (Default: True)</param>
        /// <returns>List of Working DateTime</returns>
        List<DateTime> GetWorkingDaysBetweenTwoDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true);
    }
}
