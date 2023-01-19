using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PH.WorkingDaysAndTimeUtility.Configuration;
using PH.WorkingDaysAndTimeUtility.Extensions;

namespace PH.WorkingDaysAndTimeUtility
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PH.WorkingDaysAndTimeUtility.IWorkingDaysAndTimeUtility" />
    /// <seealso cref="PH.WorkingDaysAndTimeUtility.ISplitTimes" />
    public class WorkingDaysAndTimeUtility : IWorkingDaysAndTimeUtility, ISplitTimes
    {
        public TimeSlotConfig TimeSlotConfig;

        public readonly WeekDaySpan WorkWeekConfiguration;

        public readonly List<DayOfWeek> WorkingDaysInWeek;
        public readonly List<AHolyDay> Holidays;


        /// <summary>
        /// Create a new instance of the utility with given configuration.
        /// </summary>
        /// <param name="cfg">configuration class</param>
        public WorkingDaysAndTimeUtility(WorkingDaysConfig cfg)
            :this(cfg.WorkWeekConfiguration, cfg.Holidays)
        {
            
        }

       
        /// <summary>
        /// Create a new instance of the utility with given configuration.
        /// </summary>
        /// <param name="workWeekConfiguration">work-week configuration</param>
        /// <param name="holidays">List of holidays</param>
        /// <exception cref="ArgumentNullException">Trown if null workWeekConfiguration</exception>
        /// <exception cref="ArgumentException">Thrown if workWeekConfiguration without working days defined</exception>
        public WorkingDaysAndTimeUtility(WeekDaySpan workWeekConfiguration
            , List<AHolyDay> holidays
            )
        {
            if(null == workWeekConfiguration)
            {
                throw new ArgumentNullException(nameof(workWeekConfiguration),"Week configuration mandatory");
            }

            try
            {
                CheckWeek(workWeekConfiguration);

                WorkWeekConfiguration = workWeekConfiguration;
                Holidays = holidays;

                WorkingDaysInWeek =WorkWeekConfiguration.WorkDays
                    .Where(x => x.Value.IsWorkingDay)
                    .Select(x => x.Key).ToList();

            }
            catch (ArgumentException agEx)
            {

                throw new ArgumentException("Invalid workWeekConfiguration",agEx);
            }

            TimeSlotConfig = new TimeSlotConfig();
        }

        public void SetTimeSlotConfig(TimeSlotConfig config)
        {
            //TODO: check

            TimeSlotConfig = config;

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

                if (!IfWorkingMomentGettingNext(start, out DateTime nextStart))
                {
                    start = nextStart;
                }


                return start.AddWorkingDays(days, toExclude, WorkingDaysInWeek);
            }
            catch (ArgumentException checkException)
            {
                
                throw new ArgumentException("Invalid DateTime", nameof(start),checkException);
            }
            
        }

        /// <summary>
        /// The method add <param name="hours">n hours</param> to given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="hours">Number of hours to add</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns>Result DateTime</returns>
        public DateTime AddWorkingHours(DateTime start, double hours)
        {
	        var r = new DateTime(start.Ticks);

            CheckWorkDayStart(r);

            while (!IfWorkingMomentGettingNext(r, out DateTime nextStart))
            {
	            r = r.AddMinutes(1);
            }

            int            year      = start.Year;
            List<DateTime> toExclude = CalculateDaysForExclusions(year);
            for (int i = 0; i < 60 * hours; i++)
            {
	            r= AddWorkingMinutes(r, 1, toExclude);
	            if (r.Year > year)
	            {
		            toExclude = CalculateDaysForExclusions(r.Year);
	            }
            }

            return r;
            //start = AddWorkingMinutes(start, 60 * hours)

            //if (!IfWorkingMomentGettingNext(start, out DateTime nextStart))
            //{
            //    start = nextStart;
            //}


            //DateTime r = start;
            //var totMinutes = CheckWorkTimeStartandGetTotalWorkingHoursForTheDay(start);
            //List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);
            //double hh = hours * 60;

            //if (totMinutes <= hh && WorkWeekConfiguration.Symmetrical )
            //{
            //    #region Just for "Symmetrical" week


            //    var days = (int) (hh/totMinutes);
            //    var otherMinutes = hh % totMinutes;
            //    r = r.AddWorkingDays(days, toExclude, WorkingDaysInWeek);

            //    if (otherMinutes > (double) 0)
            //    {
            //        r = AddWorkingMinutes(r, otherMinutes, toExclude);
            //    }


            //    #endregion
            //}
            //else
            //{
            //    r = AddWorkingMinutes(r, totMinutes, toExclude);

        //}
            
        //    return r;
        }

        private DateTime AddWorkingMinutesNoCheck(DateTime start, double minutes)
        {

            List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);
            var            r         = AddWorkingMinutes(start, minutes, toExclude);
            return r;
        }

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
        public DateTime AddWorkingMinutes(DateTime start, double minutes)
        {
            CheckWorkDayStart(start);

            if (!IfWorkingMomentGettingNext(start, out DateTime nextStart))
            {
                start = nextStart;
            }

            return AddWorkingMinutesNoCheck(start, minutes);
        }


        /// <summary>
        /// The method add <param name="timeSpan">n minutes</param> to 
        /// given <param name="start">start DateTime</param>.
        /// 
        /// Counting works only forward.
        /// </summary>
        /// <param name="start">Starting DateTime</param>
        /// <param name="timeSpan">Time Span - use <see cref="TimeSpan.TotalMinutes"/></param>
        // <returns>Result DateTime</returns>
        public DateTime AddWorkingTimeSpan(DateTime start, TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes <= 0)
            {
                throw new ArgumentException("Invalid TimeSpan: need TotalMinutes > 0", paramName: nameof(timeSpan));
            }

            if (!IfWorkingMomentGettingNext(start, out DateTime nextStart))
            {
                start = nextStart;
            }


            var minutes = timeSpan.TotalMinutes;
            return AddWorkingMinutes(start, minutes);
        }


        
        public List<DateTime> GetWorkingDaysBetweenTwoDateTimes(DateTime start, DateTime end)
        {
            DateTime sStart = start;
            DateTime sEnd   = end;
            if (start > end)
            {
                start = sEnd;
                end   = sStart;
            }

            DateTime realStart = start;
            DateTime realEnd = end;
            if (!IsAWorkDay(start))
            {
                this.IfWorkingMomentGettingNext(start, out DateTime realCalculated, 1);
                realStart = new DateTime(realCalculated.Year, realCalculated.Month, realCalculated.Day);
            }
            if (!IsAWorkDay(end))
            {
                IfWorkingMomentGettingPrevious(end, out DateTime realCalculated2, 1);
                realEnd = new DateTime(realCalculated2.Year, realCalculated2.Month, realCalculated2.Day);
            }

            return GetWorkingDaysBetweenTwoWorkingDateTimes(realStart, realEnd, true);

        }

        /// <summary>Gets the working days between two working date times.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="includeStartAndEnd">if set to <c>true</c> start and end are included in list (Default: <c>true</c>).</param>
        /// <exception cref="ArgumentException">Thrown if given DateTime is not a WorkDay.</exception>
        /// <returns></returns>
        public List<DateTime> GetWorkingDaysBetweenTwoWorkingDateTimes(DateTime start, DateTime end, bool includeStartAndEnd = true)
        {
            DateTime sStart = start;
            DateTime sEnd   = end;
            if (start > end)
            {
                start = sEnd;
                end   = sStart;
            }


            CheckWorkDayStart(start);
            List<DateTime> result = new List<DateTime>();
            if (includeStartAndEnd)
            {
                result.Add(start);
                result.Add(end);
            }
            List<DateTime> toExclude = CalculateDaysForExclusions(start.Year);

            while (start.Date < end.Date)
            {
                start = start.AddWorkingDays(1, toExclude, WorkingDaysInWeek);
                if (start.Date < end.Date || includeStartAndEnd)
                {
                    result.Add(start);
                }
            }
            return result.Distinct().OrderByDescending(x => x.Date).ToList();
        }

        public bool IfWorkingMomentGettingPrevious(DateTime date, out DateTime previousWorkingMoment, double minutesInterval = 1)
        {
            
            var dt = new DateTime(date.Ticks);
            
            PrvIfWorkingMomentByRef(ref dt, minutesInterval);



            previousWorkingMoment = dt;

            if (date == dt)
            {
                return true;
            }
            

                double interval = Math.Abs(minutesInterval);
                return date.AddMinutes(-interval) == previousWorkingMoment;
            



        }

        public bool IfWorkingMomentGettingNext(DateTime date, out DateTime nextWorkingMoment, double minutesInterval = 1)
        {
            double interval = Math.Abs(minutesInterval);
            var chk = IfWorkingMomentGettingPrevious(date, out var p, minutesInterval);
            
            nextWorkingMoment = chk ? this.AddWorkingMinutesNoCheck(date, interval) : this.AddWorkingMinutesNoCheck(p, interval);

            return chk;
        }

        public bool IfWorkingMoment(DateTime date, out DateTime nextWorkingMoment, out DateTime previousWorkingMoment,
                                    double minutesInterval = 1)
        {
            var result = IfWorkingMomentGettingPrevious(date, out previousWorkingMoment, minutesInterval);
            
            nextWorkingMoment = result ? this.AddWorkingMinutesNoCheck(date, minutesInterval) : this.AddWorkingMinutesNoCheck(previousWorkingMoment, minutesInterval);



            return result;

           
        }

        /// <summary>
        /// Determines whether is a work day the specified day(do not check for Hours/Minutes, jut Day).
        /// </summary>
        /// <param name="day">The day.</param>
        /// <returns>
        ///   <c>true</c> if is a work day the specified day; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAWorkDay(DateTime day)
        {

            var check = WorkWeekConfiguration.WorkDays.ContainsKey(day.DayOfWeek);
            if (!check)
            {
                return false;
            }

            var workTime = WorkWeekConfiguration.WorkDays[day.DayOfWeek];
            if (!workTime.IsWorkingDay)
            {
                return false;
            }

            var holyDays = this.CalculateDaysForExclusions(day.Year);
            if (holyDays.Any(x => x.Year == day.Year && x.Month == day.Month && x.Day == day.Day))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether if given <see cref="DateTime"/> is holy day(do not check for Hours/Minutes, jut Day)..
        /// </summary>
        /// <param name="day">The day.</param>
        /// <returns>
        ///   <c>true</c> if given <see cref="DateTime"/> is holy day; otherwise, <c>false</c>.
        /// </returns>
        public bool IsHolyDay(DateTime day)
        {
            var holyDays = this.CalculateDaysForExclusions(day.Year);
            return holyDays.Any(x => x.Year == day.Year && x.Month == day.Month && x.Day == day.Day); 
            

        }


        public static bool TryGetFromConfig(WorkingDaysConfig cfg, out WorkingDaysAndTimeUtility u)
        {
            try
            {
                u = new WorkingDaysAndTimeUtility(cfg);
                return true;
            }
            catch 
            {
                u = null;
                return false;
            }
        }

        public static bool TryParseConfig(string config, out IWorkingDaysAndTimeUtility u)
        {


            try
            {
                var cfg = JsonConvert.DeserializeObject<WorkingDaysConfig>(config);
                if (TryGetFromConfig(cfg, out WorkingDaysAndTimeUtility uu))
                {
                    u = uu;
                    return true;
                }
                else
                {
                    u = null;
                    return false;
                }
            }
            catch 
            {
                u = null;
                return false;
            }
        }


        #region WorkeTimeSliceResult

        
        internal class InternalElapsedFactor
        {
            public DateTime Begin { get; set; }
            public bool IfWorkingTime { get; set; }
            public double SecondsAmount { get; set; }
            
        }

        private List<DateTimeSlot> BuildDateTimeSlot(List<TimeSlot> slots, DateTime start, DateTime end)
        {
            var list = slots.Select(x => DateTimeSlot.Build(x, start)).ToList();
            var mid  = list.Where(x => x.DateTimeEnd >= start && x.DateTimeStart <= end).ToList();

            return mid
                       .OrderBy(x => x.DateTimeStart)
                       .ThenBy(x => x.DateTimeEnd).ToList();
        }


        private bool IfWorkingDateTimeForSplit(DateTime d)
        {
            var b           = WorkWeekConfiguration.WorkDays.ContainsKey(d.DayOfWeek);
            if (!b)
            {
                return false;
            }

            var  workDaySpan = WorkWeekConfiguration.WorkDays[d.DayOfWeek];
            bool final       = false;
            foreach (var workTimeSpan in workDaySpan.TimeSpans)
            {
                if (workTimeSpan.IsStrictWorkInstant(d.Hour, d.Minute,d.Second))
                {
                    final = true;
                    break;
                    
                }
            }

            return final;
        }

        /// <summary>Splits the worked time in factors.</summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// start - End Time '{end:O}' must be greather than Start Time '{start:O}'
        /// or
        /// end - End must be on same Date of Start
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// WorkWeekConfiguration - WorkWeekConfiguration Config mandatory
        /// or
        /// WorkWeekConfiguration - TimeSlotConfig Config mandatory
        /// or
        /// WorkWeekConfiguration - TimeSlotConfig Config not found for '{start.DayOfWeek}'
        /// </exception>
        public WorkedTimeSliceResult SplitWorkedTimeInFactors(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start,
                                                      $"End Time '{end:O}' must be greather than Start Time '{start:O}'");
            }

            if (end.Date != start.Date)
            {
                throw new ArgumentOutOfRangeException(nameof(end), end, $"End must be on same Date of Start");
            }

            if (null == WorkWeekConfiguration)
            {
                throw new ArgumentNullException(nameof(WorkWeekConfiguration), "WorkWeekConfiguration Config mandatory");
            }

            if (null == TimeSlotConfig)
            {
                throw new ArgumentNullException(nameof(WorkWeekConfiguration), "TimeSlotConfig Config mandatory");
            }

            if (!TimeSlotConfig.TimesDictionary.ContainsKey(start.DayOfWeek))
            {
                throw new ArgumentNullException(nameof(WorkWeekConfiguration), $"TimeSlotConfig Config not found for '{start.DayOfWeek}'");
            }

            TimeSpan totalDuration = end - start;

            WorkedTimeSliceResult result = new WorkedTimeSliceResult()
            {
                Start         = start,
                End           = end,
                TotalDuration = totalDuration
            };



            List<TimeSlot> slotsi     = TimeSlotConfig.TimesDictionary[start.DayOfWeek];

            bool isAWorkDay = !this.IsHolyDay(start.Date);

            if (!isAWorkDay && null != TimeSlotConfig.HolyDaySlots && TimeSlotConfig.HolyDaySlots.Any())
            {
                slotsi = TimeSlotConfig.HolyDaySlots;
            }

            var slots = BuildDateTimeSlot(slotsi, start, end);

            var s = start;

            Dictionary<TimeSlot, InternalElapsedFactor> secsPerSlots = new Dictionary<TimeSlot, InternalElapsedFactor>();
            List<WorkeTimeSlice> sliceResults = new List<WorkeTimeSlice>();


            for (int i = 0; i < totalDuration.TotalSeconds; i++)
            {

                foreach (var timeSlot in slots)
                {
                    if (timeSlot.InSlot(s))
                    {
                        if (secsPerSlots.ContainsKey(timeSlot))
                        {
                            InternalElapsedFactor ifa = secsPerSlots[timeSlot];
                            ifa.SecondsAmount      += 1;
                            secsPerSlots[timeSlot] =  ifa;
                        }
                        else
                        {
                            bool ifWorkTime = false;
                            if (isAWorkDay)
                            {
                                ifWorkTime = IfWorkingDateTimeForSplit(s);
                            }
                            InternalElapsedFactor ifa = new InternalElapsedFactor() {Begin = s, SecondsAmount = 1, IfWorkingTime = ifWorkTime};
                            secsPerSlots.Add(timeSlot, ifa);
                        }
                        break;
                    }

                }

                s = s.AddSeconds(1);
            }

            //while (s < end)
            //{
            //    foreach (var timeSlot in slots)
            //    {
            //        if (timeSlot.InSlot(s))
            //        {
            //            if (secsPerSlots.ContainsKey(timeSlot))
            //            {
            //                double a = secsPerSlots[timeSlot];
            //                a                           += 1;
            //                secsPerSlots[timeSlot] =  a;
            //            }
            //            else
            //            {
            //                secsPerSlots.Add(timeSlot,1);
            //            }
            //            break;
            //        }
                    
            //    }

            //    s = s.AddSeconds(1);
            //}


           
            foreach (var keyValuePair in secsPerSlots.OrderBy(x => x.Value.Begin))
            {
                

                sliceResults.Add(new WorkeTimeSlice()
                {
                    Start      = keyValuePair.Value.Begin,
                    Duration   = TimeSpan.FromSeconds(keyValuePair.Value.SecondsAmount),
                    Factor     = keyValuePair.Key.Factor,
                    OnHolyDay  = !isAWorkDay,
                    OnWorkTime = keyValuePair.Value.IfWorkingTime,
                    TimeSlot   = keyValuePair.Key
                });


            }


            result.WorkSlices = sliceResults.ToArray();

            return result;


        }

       

        #endregion

      

        #region private methods

        private bool PrvIfWorkingMomentByRef(ref DateTime d, double minutesInterval = 1, bool recurring = false)
        {
            double interval = Math.Abs(minutesInterval);
            bool r = false;

            if (WorkWeekConfiguration.WorkDays.ContainsKey(d.DayOfWeek))
            {
                if (!Holidays.Contains(new HoliDay(d.Day, d.Month)))
                {
                    if (WorkWeekConfiguration.IsWorkDateTime(d))
                    {
                        if (!recurring)
                        {
                            d = d.AddMinutes(-interval);
                        }

                        r = true;
                    }
                }
                else
                {
                    d = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                }

                
            }
            else
            {
                d = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
            }

            do
            {
                if(r)
                {
                    break;
                }


                d = d.AddMinutes(-interval);
                r = PrvIfWorkingMomentByRef(ref d, minutesInterval, true);

            } while (!r);

            return true;
        }

        //private bool PrivateIfWorkingMomentGettingPrevious(DateTime date, out bool realResult, out DateTime previousWorkingMoment, double minutesInterval = 1, bool recurring = false)
        //{
        //    realResult = false;
        //    bool   r        = false;
        //    double interval = Math.Abs(minutesInterval);

        //    if ((_workWeekConfiguration.WorkDays.ContainsKey(date.DayOfWeek)))
        //    {
        //        //is a work day
        //        r = _workWeekConfiguration.IsWorkDateTime(date);
        //        if (r)
        //        {
        //            if (!recurring)
        //            {
        //                realResult = true;
        //                previousWorkingMoment = this.AddWorkingMinutes(date, -interval);
        //            }
        //            else
        //            {
        //                previousWorkingMoment = date;
        //            }

                    
        //            return true;
        //        }
        //        else
        //        {
        //            var intermediateDate1 = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

        //            do
        //            {
        //                intermediateDate1 = intermediateDate1.AddMinutes(-interval);
        //                r                 = PrivateIfWorkingMomentGettingPrevious(intermediateDate1, out bool b, out DateTime d, minutesInterval, true);

        //            } while (!r);
        //            //previousWorkingMoment = intermediateDate1;
                    
        //            previousWorkingMoment = date.AddMinutes(-interval);
        //            return true;
        //        }
                
        //    }

        //    var intermediateDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

        //    do
        //    {
        //        intermediateDate = intermediateDate.AddMinutes(-interval);
        //        r                = PrivateIfWorkingMomentGettingPrevious(intermediateDate,out bool b,  out DateTime d, minutesInterval,true);

        //    } while (!r);

        //    previousWorkingMoment = date;
        //    return true;

        //}

        

        private DateTime AddWorkingMinutes(DateTime start, double otherMinutes, List<DateTime> toExclude)
        {
            DateTime r = start;
            while (otherMinutes > 0)
            {

                r = r.AddMinutes(1);

                //check if in work-interval
                WorkTimeSpan nextInterval;
                bool isInWorkInterval = CheckIfWorkTime(r, out nextInterval);

                if (!isInWorkInterval)
                {
                    if (null != nextInterval)
                    {
                        
                        var ts = nextInterval.Start;
                        r = new DateTime(r.Year, r.Month, r.Day, ts.Hours, ts.Minutes, ts.Seconds);//.AddMinutes(1);
                    }
                    else
                    {
                        r = r.AddWorkingDays(1, toExclude, WorkingDaysInWeek);  //AddOneDay(r, ref toExclude);
                        var ts = GetFirstTimeSpanOfTheWorkingDay(r);
                        r = new DateTime(r.Year, r.Month, r.Day, ts.Hours, ts.Minutes, ts.Seconds);
                    }
                }
                otherMinutes--;
            }

            return r;
        }

        
        private TimeSpan GetFirstTimeSpanOfTheWorkingDay(DateTime d)
        {
            var workDaySpan = WorkWeekConfiguration.WorkDays[d.DayOfWeek];
            return workDaySpan.TimeSpans
                .OrderBy(x => x.Start).Select(x => x.Start).FirstOrDefault();
        }

        private bool CheckIfWorkTime(DateTime d, out WorkTimeSpan nextInterval)
        {
            var workDaySpan = WorkWeekConfiguration.WorkDays[d.DayOfWeek];
            var nextOutSpan    = WorkWeekConfiguration.WorkDays.Where(x => x.Key > d.DayOfWeek).Select(x => x.Value).FirstOrDefault();
            if (null == nextOutSpan)
            {
	            nextOutSpan = WorkWeekConfiguration.WorkDays.FirstOrDefault().Value;
            }

            bool r           = false;
            nextInterval = null;

            if (null == workDaySpan)
            {
                
                return false;
            }
            else
            {
	            var orderdTimes = workDaySpan.TimeSpans.OrderBy(x => x.Start).ThenBy(x => x.End).ToArray();
	            int c           = 0;
	            foreach (var ts in orderdTimes)
	            {


		            var start = new DateTime(d.Year, d.Month, d.Day, ts.Start.Hours, ts.Start.Minutes, ts.Start.Seconds);
		            var end   = new DateTime(d.Year, d.Month, d.Day, ts.End.Hours, ts.End.Minutes, ts.End.Seconds);

		            if (d < end)
		            {
			            if (d >= start)
			            {
				            return true;
			            }
			            else
			            {
				            nextInterval = ts;
			            }
		            }

		           

		            //if (start <= d && d < end)
		            //{
			           // if (c <= orderdTimes.Length)
			           // {
				          //  nextInterval = orderdTimes[c];
			           // }
			           // else
			           // {
				          //  nextInterval = nextOutSpan.TimeSpans.OrderBy(x => x.Start).First();
			           // }

			           // return true;
		            //}
	            }

	            return false;

	            /*
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
	                nextInterval = counter == orderdTimes.Length - 1 ? null : orderdTimes[counter + 1];
	                if (d == e)
	                {
	                    break;
	                }
	                else
	                {
	                    if (s <= d && d < e)
	                    {
	                        r = true;
	                        break;
	                    }
	                }
	            }
	            */
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
            if (!(WorkWeekConfiguration.WorkDays.ContainsKey(start.DayOfWeek)))
            {
                var err = "Invalid DateTime start given: give a workingday for start or check your configuration";
                throw new ArgumentException(err, nameof(start));
            }
        }


        private double GetTotalWorkingHoursForTheDay(DateTime d)
        {
            double r = (double) 0;
            var workDaySpan = WorkWeekConfiguration.WorkDays[d.DayOfWeek];
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
            var inError = ret.Equals((double)0);
            
            if (inError)
            {
                var err = "Invalid DateTime start given: give a valid time for start or check your configuration";
                throw new ArgumentException(err, nameof(start));
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
                    {
                        throwMy = false;
                    }
                }
            }
            if (throwMy)
            {
                var err = "WeekDaySpan without working days defined, check your configuration";
                throw new ArgumentException(err, nameof(weekDaySpan));
            }
        }

        
        private List<DateTime> CalculateDaysForExclusions(int year)
        {
            List<DateTime> r = new List<DateTime>();
            Holidays.ForEach(day =>
            {
                try
                {
                    if (day is MultiCalculatedHoliDay multi)
                    {
                        r.AddRange(multi.CalculateList(year));
                    }
                    else
                    {
                        r.Add(day.Calculate(year));    
                    }
                    
                }
                catch 
                {
                    //
                }
                
            });
            return r.OrderByDescending(x => x.Date).ToList();
        }


        #endregion

    }
}
