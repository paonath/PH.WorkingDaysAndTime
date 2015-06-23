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
                

                while (moreAdd)
                {
                    int y = end.Year;
                    end = end.AddDays(1);
                    if (y < end.Year)
                    {
                        List<DateTime> newExclusions = new List<DateTime>();
                        datesToExclude.ForEach(d =>
                        {
                            DateTime n;
                            if (DateTime.TryParse(String.Format("{0}-{1}-{2}", end.Year, d.Month, d.Day), out n))
                            {
                                newExclusions.Add(n);
                            }
                        });

                        datesToExclude.AddRange(newExclusions);
                    }

                    //check if current is a workingDay
                    if (workDaysOfTheWeeks.Contains(end.DayOfWeek))
                    {
                        if (datesToExclude.Count == 0)
                        {
                            moreAdd = false;
                        }
                        else
                        {
                            var holiDayInList = datesToExclude.FirstOrDefault(x => x.Date == end.Date);
                            moreAdd = !(holiDayInList > DateTime.MinValue);
                            
                        }

                    }
                }
            }
            return end;
        }
    }
}
