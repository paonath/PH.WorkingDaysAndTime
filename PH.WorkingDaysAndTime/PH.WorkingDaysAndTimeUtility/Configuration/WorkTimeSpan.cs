using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    public class WeekConfigFactory
    {
        public static WeekConfig BuildSymmetrical(IEnumerable<DayOfWeek> daysOfTheWeek, IEnumerable<TimeSlice> timeSlices)
        {
            var dd = daysOfTheWeek.OrderBy(x => x).ToArray();
            var tt = timeSlices.OrderBy(x => x.Begin).ThenBy(x => x.End).ToList();
            var dict = new Dictionary<DayOfWeek, List<TimeSlice>>();
            foreach (var ofWeek in dd)
            {
                dict.Add(ofWeek, tt);
            }
            return new WeekConfig(dict);
        }
    }
    
    public class WeekConfig
    {
        public Dictionary<DayOfWeek,List<TimeSlice>> DaysDictionary { get; set; }

        public WeekConfig()
        {
            DaysDictionary = new Dictionary<DayOfWeek, List<TimeSlice>>
            {
                {DayOfWeek.Monday, new List<TimeSlice>()},
                {DayOfWeek.Tuesday, new List<TimeSlice>()},
                {DayOfWeek.Wednesday, new List<TimeSlice>()},
                {DayOfWeek.Thursday, new List<TimeSlice>()},
                {DayOfWeek.Friday, new List<TimeSlice>()},
                {DayOfWeek.Saturday, new List<TimeSlice>()},
                {DayOfWeek.Sunday, new List<TimeSlice>()}
            };
        }

        public WeekConfig(Dictionary<DayOfWeek,List<TimeSlice>> configDaysDictionary)
            :this()
        {
            
            foreach (DayOfWeek value in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (configDaysDictionary.ContainsKey(value))
                {
                    DaysDictionary[value] = configDaysDictionary[value] ;
                }
                

            }
        }

       
    }

    public class HolydaysConfig
    {
        public List<AHolyDay> Holidays { get; set; }
        public List<OffWorkTimeSlice> OffWorkTimeSlices { get; set; }
    }
    
    
   

    /// <summary>
    /// A Slice of work-time.
    /// </summary>
    [Obsolete("",false)]
    public class WorkTimeSpan
    {
        /// <summary>
        /// Starting Time for Work
        /// </summary>
        public TimeSpan Start { get; set; }
        /// <summary>
        /// End Time for Work
        /// </summary>
        public TimeSpan End { get; set; }

        public WorkTimeSpan()
        {
            
        }

        public WorkTimeSpan(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }

        
    }
}