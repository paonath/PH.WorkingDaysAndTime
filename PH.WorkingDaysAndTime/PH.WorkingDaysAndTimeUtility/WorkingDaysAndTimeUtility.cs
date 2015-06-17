using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PH.WorkingDaysAndTimeUtility
{
    public class WorkingDaysAndTimeUtility : IWorkingDaysAndTimeUtility
    {
        private WeekDaySpan _weekDaySpan;
        private List<HolyDay> _daysToExcludeList;
        
       
        public WorkingDaysAndTimeUtility(WeekDaySpan weekDaySpan
            , List<HolyDay> daysToExcludeList
            )
        {
            CheckWeek(weekDaySpan);

            _weekDaySpan = weekDaySpan;
            _daysToExcludeList = daysToExcludeList;
            
        }

        public DateTime AddWorkingDays(DateTime start, int days)
        {
            CheckWorkDayStart(start);

            List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);

            DateTime end = start;
            for (int i = 0; i < days; i++)
            {
                end = AddOneDay(end,ref toExclude);
            }
            return end;
        }
        public DateTime AddWorkingDays(DateTime start, int days, out List<DateTime> resultListOfHolyDays)
        {
            CheckWorkDayStart(start);

            List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);

            DateTime end = start;
            for (int i = 0; i < days; i++)
            {
                end = AddOneDay(end, ref toExclude);
            }
            resultListOfHolyDays = toExclude;
            return end;
        }
           

        public DateTime AddWorkingHours(DateTime start, double hours)
        {
            CheckWorkDayStart(start);
            DateTime r = start;
            var totMinutes = CheckWorkTimeStartandGetTotalWorkingHoursForTheDay(start);
            List<DateTime> toExclude;
            if (totMinutes <= hours*60 && _weekDaySpan.Symmetrical )
            {
                //questa cosa la devo fare solo per una settimana "simmetrica"

                double hh = hours*60;
                var days = (int) (hh/totMinutes);
                var otherMinutes = hh % totMinutes;
                r = AddWorkingDays(r, days, out toExclude);
                r = AddWorkingMinutes(r, otherMinutes, toExclude);
                //ancora roba da fare qui...ho il resto dei minuti
            }
            //ancora roba da fare qui...
            throw new
            NotImplementedException();
            return r;
        }

        private DateTime AddWorkingMinutes(DateTime r, double otherMinutes, List<DateTime> toExclude)
        {
            throw new NotImplementedException();
        }

        private void CheckWorkDayStart(DateTime start)
        {
            if (!(_weekDaySpan.WorkDays.ContainsKey(start.DayOfWeek)))
            {
                var err = "Invalid DateTime start given: give a workingday for start or check your configuration";
                throw new ArgumentException(err, "start");
            }
        }

        private double CheckWorkTimeStartandGetTotalWorkingHoursForTheDay(DateTime start)
        {
            var inError = true;
            double ret = 0;
            var workDaySpan = _weekDaySpan.WorkDays[start.DayOfWeek];
            workDaySpan.TimeSpans.OrderBy(x => x.Start).ToList()
                .ForEach(ts =>
                {
                    var s = new DateTime(start.Year, start.Month, start.Day, ts.Start.Hours, ts.Start.Minutes,
                        ts.Start.Seconds);
                    var e = new DateTime(start.Year, start.Month, start.Day, ts.End.Hours, ts.End.Minutes,
                        ts.End.Seconds);
                    if (s <= start && start <= e)
                    {
                        inError = false;
                        ret = workDaySpan.WorkingMinutesPerDay;
                        
                    }
                });
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

        private DateTime AddOneDay(DateTime d,ref List<DateTime> toExclude)
        {
            DateTime r = d.AddDays(1);
            bool addAnother = false;
            //check if current is a workingDay
            if (_weekDaySpan.WorkDays.ContainsKey(r.DayOfWeek))
            {
                if (!(_weekDaySpan.WorkDays[r.DayOfWeek].IsWorkingDay))
                {
                    addAnother = true;
                }
            }
            else
            {
                addAnother = true;
            }

            if(addAnother)
                r = AddOneDay(r,ref toExclude);
            

            if (null != toExclude && toExclude.Count > 0 &&  toExclude.Contains(r))
            {
                toExclude.Remove(r);
                r = AddOneDay(r,ref toExclude);
            }
            
            return r;
        }


        private List<DateTime> CalculateDaysForExclusions(int year)
        {
            List<DateTime> r = new List<DateTime>();
            _daysToExcludeList.ForEach(day =>
            {
                r.Add(day.Calculate(year));
            });
            return r;
        }




    }
}
