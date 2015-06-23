using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PH.WorkingDaysAndTimeUtility.Configuration;

namespace PH.WorkingDaysAndTimeUtility
{
    public class WorkingDaysAndTimeUtility : IWorkingDaysAndTimeUtility
    {
        private readonly WeekDaySpan _workWeekConfiguration;
        private readonly List<DayOfWeek> _workingDaysInWeek;
        private readonly List<HoliDay> _holidays;
        
       
        /// <summary>
        /// Create a new instance of the utility with given configuration.
        /// </summary>
        /// <param name="workWeekConfiguration">work-week configuration</param>
        /// <param name="holidays">List of holidays</param>
        /// <exception cref="ArgumentNullException">Trown if null workWeekConfiguration</exception>
        /// <exception cref="ArgumentException">Thrown if workWeekConfiguration without working days defined</exception>
        public WorkingDaysAndTimeUtility(WeekDaySpan workWeekConfiguration
            , List<HoliDay> holidays
            )
        {
            if(null == workWeekConfiguration)
                throw new ArgumentNullException("workWeekConfiguration","Week configuration mandatory");
            try
            {
                CheckWeek(workWeekConfiguration);

                _workWeekConfiguration = workWeekConfiguration;
                _holidays = holidays;

                _workingDaysInWeek =_workWeekConfiguration.WorkDays
                    .Where(x => x.Value.IsWorkingDay == true)
                    .Select(x => x.Key).ToList();

            }
            catch (ArgumentException agEx)
            {

                throw new ArgumentException("Invalid workWeekConfiguration",agEx);
            }
            
        }

        /// <summary>
        /// The method add <param name="days">n days</param> to given <param name="start">start Date</param>.
        /// 
        /// <see cref="WorkingDateTimeExtension.AddWorkingDays"/>
        /// </summary>
        /// <param name="start">Starting Date</param>
        /// <param name="days">Numer of days to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>First working-day after n occurences from given date</returns>
        public DateTime AddWorkingDays(DateTime start, int days)
        {
            try
            {
                CheckWorkDayStart(start);
                List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);
                return start.AddWorkingDays(days, toExclude, _workingDaysInWeek);
            }
            catch (ArgumentException checkException)
            {
                
                throw new ArgumentException("Invalid DateTime", "start",checkException);
            }
            
        }

        
        public DateTime AddWorkingHours(DateTime start, double hours)
        {
            CheckWorkDayStart(start);
            DateTime r = start;
            var totMinutes = CheckWorkTimeStartandGetTotalWorkingHoursForTheDay(start);
            List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);
            double hh = hours * 60;

            if (totMinutes <= hh && _workWeekConfiguration.Symmetrical )
            {
                #region Just for "Symmetrical" week

                
                var days = (int) (hh/totMinutes);
                var otherMinutes = hh % totMinutes;
                r = r.AddWorkingDays(days, toExclude, _workingDaysInWeek);
                    
                if (otherMinutes > (double) 0)
                {
                    r = AddWorkingMinutes(r, otherMinutes, toExclude);
                }
                

                #endregion
            }
            else
            {
                r = AddWorkingMinutes(r, totMinutes, toExclude);

            }
            
            return r;
        }

        public List<DateTime> GetWorkingDaysBetweenTwoDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true)
        {
            CheckWorkDayStart(start);
            List<DateTime> result = new List<DateTime>() {};
            if (includeStartAndEnd)
            {
                result.Add(start);
                result.Add(end);
            }
            List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);

            while (start.Date < end.Date)
            {
                start = start.AddWorkingDays(1, toExclude, _workingDaysInWeek);   //AddOneDay(start, ref toExclude);
                if (start.Date < end.Date || includeStartAndEnd)
                    result.Add(start);
            }
            return result.Distinct().OrderByDescending(x => x.Date).ToList();
        }


        #region private methods

        //private DateTime AddWorkingDays(DateTime start, int days, out List<DateTime> resultListOfHoliDays)
        //{
        //    CheckWorkDayStart(start);

        //    List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);

        //    DateTime end = start;
        //    for (int i = 0; i < days; i++)
        //    {
        //        end = AddOneDay(end, ref toExclude);
        //    }
        //    resultListOfHoliDays = toExclude;
        //    return end;
        //}

        private DateTime AddWorkingMinutes(DateTime start, double otherMinutes, List<DateTime> toExclude)
        {
            DateTime r = start;
            while (otherMinutes > 0)
            {
                r = AddOneMinute(r, ref toExclude);
                otherMinutes--;
            }
            return r;
        }

        private DateTime AddOneMinute(DateTime start, ref List<DateTime> toExclude)
        {
            DateTime r = start.AddMinutes(1);
            
            //check if in work-interval
            WorkTimeSpan nextInterval;
            bool isInWorkInterval = CheckIfWorkTime(r, out nextInterval);
            if (!isInWorkInterval)
            {
  
                if (null != nextInterval)
                {
                    var ts = nextInterval.Start;
                    r = new DateTime(r.Year, r.Month, r.Day, ts.Hours,ts.Minutes,ts.Seconds);
                }
                else
                {
                    r = r.AddWorkingDays(1, toExclude, _workingDaysInWeek);  //AddOneDay(r, ref toExclude);
                    var ts = GetFirstTimeSpanOfTheWorkingDay(r);
                    r = new DateTime(r.Year, r.Month, r.Day, ts.Hours, ts.Minutes, ts.Seconds);
                }
            }

            return r;
        }

        private TimeSpan GetFirstTimeSpanOfTheWorkingDay(DateTime d)
        {
            var workDaySpan = _workWeekConfiguration.WorkDays[d.DayOfWeek];
            return workDaySpan.TimeSpans
                .OrderBy(x => x.Start).Select(x => x.Start).FirstOrDefault();
        }

        private bool CheckIfWorkTime(DateTime d, out WorkTimeSpan nextInterval)
        {
            var workDaySpan = _workWeekConfiguration.WorkDays[d.DayOfWeek];
            bool r = false;
            nextInterval = null;

            if (null == workDaySpan)
            {
                
                return false;
            }
            else
            {
                var orderdTimes = (from t in workDaySpan.TimeSpans
                    orderby t.Start ascending, t.End ascending
                    select t
                    ).ToArray();
                int counter = -1;
                foreach (var ts in orderdTimes)
                {
                    counter++;

                    var s = new DateTime(d.Year, d.Month, d.Day, ts.Start.Hours, ts.Start.Minutes,
                        ts.Start.Seconds);
                    var e = new DateTime(d.Year, d.Month, d.Day, ts.End.Hours, ts.End.Minutes,
                        ts.End.Seconds);
                    if (s <= d && d <= e)
                    {
                        r = true;
                        nextInterval = counter == orderdTimes.Length - 1 ? null : orderdTimes[counter + 1];
                        break;
                    }
                }
            }
            
            return r;
        }

        /// <summary>
        /// Check if given DateTime is valid WorkDay.
        /// </summary>
        /// <param name="start">DateTime to Check</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        private void CheckWorkDayStart(DateTime start)
        {
            if (!(_workWeekConfiguration.WorkDays.ContainsKey(start.DayOfWeek)))
            {
                var err = "Invalid DateTime start given: give a workingday for start or check your configuration";
                throw new ArgumentException(err, "start");
            }
        }


        private double GetTotalWorkingHoursForTheDay(DateTime d)
        {
            double r = (double) 0;
            var workDaySpan = _workWeekConfiguration.WorkDays[d.DayOfWeek];
            workDaySpan.TimeSpans.OrderBy(x => x.Start).ToList()
                .ForEach(ts =>
                {
                    var s = new DateTime(d.Year, d.Month, d.Day, ts.Start.Hours, ts.Start.Minutes,
                        ts.Start.Seconds);
                    var e = new DateTime(d.Year, d.Month, d.Day, ts.End.Hours, ts.End.Minutes,
                        ts.End.Seconds);
                    if (s <= d && d <= e)
                    {
                        
                        r = workDaySpan.WorkingMinutesPerDay;

                    }
                });
            return r;
        }

        private double CheckWorkTimeStartandGetTotalWorkingHoursForTheDay(DateTime start)
        {
            double ret = GetTotalWorkingHoursForTheDay(start);
            var inError = ret == (double)0;
            
            if (inError)
            {
                var err = "Invalid DateTime start given: give a valid time for start or check your configuration";
                throw new ArgumentException(err, "start");
            }
            else
            {
                return ret;
            }
        }

        private void CheckWeek(WeekDaySpan weekDaySpan)
        {
            bool throwMy = true;
            if (null != weekDaySpan.WorkDays)
            {
                foreach (DayOfWeek dow in Enum.GetValues(typeof (DayOfWeek)))
                {
                    if (weekDaySpan.WorkDays.ContainsKey(dow))
                        throwMy = false;
                }
            }
            if (throwMy)
            {
                var err = "WeekDaySpan without working days defined, check your configuration";
                throw new ArgumentException(err, "weekDaySpan");
            }
        }

        //private DateTime AddOneDay(DateTime d,ref List<DateTime> toExclude)
        //{
        //    bool moreAdd = true;
        //    DateTime r = d;

        //    while (moreAdd)
        //    {
        //        int y = d.Year;
        //        r = r.AddDays(1);
        //        if (y < r.Year)
        //        {
        //            toExclude.AddRange(CalculateDaysForExclusions(r.Year));
        //        }

        //        //check if current is a workingDay
        //        if (_workWeekConfiguration.WorkDays.ContainsKey(r.DayOfWeek))
        //        {
        //            if ((_workWeekConfiguration.WorkDays[r.DayOfWeek].IsWorkingDay))
        //            {
        //                if (null != toExclude && toExclude.Count > 0)
        //                {
        //                    var holiDayInList = toExclude.FirstOrDefault(x => x.Date == r.Date);
        //                    if (holiDayInList > DateTime.MinValue)
        //                    {
        //                        toExclude.Remove(holiDayInList);
        //                    }
        //                    else
        //                    {
        //                        moreAdd = false;
        //                    }
        //                }
        //                else
        //                {
        //                    moreAdd = false;
        //                }
                        
        //            }
        //        }
        //    }

        //    return r;
        //}


        private List<DateTime> CalculateDaysForExclusions(int year)
        {
            List<DateTime> r = new List<DateTime>();
            _holidays.ForEach(day =>
            {
                r.Add(day.Calculate(year));
            });
            return r.OrderByDescending(x => x.Date).ToList();
        }


        #endregion

    }
}
