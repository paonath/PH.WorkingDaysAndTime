using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.WorkingDaysAndTimeUtility
{
    public static class WorkingDateTimeExtension
    {
        public static DateTime AddWorkingDays(this DateTime dateTime, int days
            , List<DateTime> datesToExclude , List<DayOfWeek> workDaysOfTheWeeks 
            )
        {
            DateTime end = dateTime;
            for (int i = 0; i < days; i++)
            {
                bool moreAdd = true;
                var datesToExcludeList = datesToExclude;

                while (moreAdd)
                {
                    int y = end.Year;
                    end = end.AddDays(1);
                    if (y < end.Year)
                    {
                        //regenerate list for new year
                        List<DateTime> newExclusions = new List<DateTime>();
                        datesToExclude.ForEach(d =>
                        {
                            try
                            {
                                newExclusions.Add(new DateTime(end.Year, d.Month, d.Day));

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
                            var holiDayInList = datesToExcludeList.FirstOrDefault(x => x.Date == end.Date);
                            if (holiDayInList.Date > DateTime.MinValue.Date)
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
