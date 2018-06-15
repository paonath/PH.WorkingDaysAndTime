using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.WorkingDaysAndTimeUtility
{
    public static class WorkingDateTimeExtension
    {
        /// <summary>
        /// Returns a new System.DateTime that adds the <param name="days">specified number of working days</param> 
        /// to the value of this instance.
        /// Counting works only forward.
        /// </summary>
        /// <param name="dateTime">this instance of DateTime</param>
        /// <param name="days">number of working days to add</param>
        /// <param name="datesToExclude">List of holidays to skip calculating result</param>
        /// <param name="workDaysOfTheWeeks">List of weekdays that are actually working days</param>
        /// <returns>
        /// An object whose value is the sum of the date and time represented 
        /// by this instance and the number of working days represented by value.
        /// </returns>
        public static DateTime AddWorkingDays(this DateTime dateTime, int days
            , List<DateTime> datesToExclude , List<DayOfWeek> workDaysOfTheWeeks 
            )
        {
            DateTime end = dateTime;
            int y = end.Year;
            days = Math.Abs(days);
            var originalList = new List<DateTime>();
            datesToExclude.ForEach(it =>
            {
                originalList.Add(it);
            });
            var datesToExcludeList = datesToExclude;

            for (int i = 0; i < days; i++)
            {
                bool moreAdd = true;

                while (moreAdd)
                {
                    end = end.AddDays(1);
                    if (y < end.Year)
                    {
                        y = end.Year;
                        //regenerate list for new year
                        List<DateTime> newExclusions = new List<DateTime>();
                        originalList.ForEach(d =>
                        {
                            try
                            {
                                newExclusions.Add(new DateTime(y, d.Month, d.Day));

                            }
                            catch
                            {
                            }
                            
                        });

                        datesToExcludeList.AddRange(newExclusions);
                    }

                    //check if current is a workingDay
                    if (workDaysOfTheWeeks.Contains(end.DayOfWeek))
                    {
                        if (datesToExcludeList.Count == 0)
                        {
                            moreAdd = false;
                        }
                        else
                        {
                            var holiDayInList = datesToExcludeList
                                .FirstOrDefault(x => x.Date == end.Date);

                            if (holiDayInList.Date != DateTime.MinValue.Date)
                            {
                                datesToExcludeList.Remove(holiDayInList);
                            }
                            else
                            {
                                
                                    moreAdd = false;
                                
                            }
                        }
                    }
                }
            }
            return end;
        }


        
    }
}
