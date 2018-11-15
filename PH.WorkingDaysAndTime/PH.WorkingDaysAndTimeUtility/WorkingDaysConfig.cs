using System.Collections.Generic;
using Newtonsoft.Json;
using PH.WorkingDaysAndTimeUtility.Configuration;
using PH.WorkingDaysAndTimeUtility.Converter;

namespace PH.WorkingDaysAndTimeUtility
{
    /// <summary>
    /// Configuration for <see cref="WorkingDaysAndTimeUtility"/>
    /// </summary>
    //[JsonConverter(typeof(WorkingDaysConfigJsonConverter))]
    public class WorkingDaysConfig
    {
        /// <summary>
        /// Work-Week config based on <see cref="WeekDaySpan"/>
        /// </summary>
        public WeekDaySpan WorkWeekConfiguration { get; set; }

        /// <summary>
        /// List of <see cref="HoliDay"/>
        /// </summary>
        public List<AHolyDay> Holidays { get; set; }

        public WorkingDaysConfig()
            :this(new WeekDaySpan(), new List<AHolyDay>() )
        {
            
        }

        public WorkingDaysConfig(WeekDaySpan workWeek, List<AHolyDay> holidays)
        {
            WorkWeekConfiguration = workWeek;
            Holidays              = holidays;
        }

        public WorkingDaysConfig Week(WeekDaySpan workWeek)
        {
            WorkWeekConfiguration = workWeek;
            return this;
        }

        public WorkingDaysConfig Holiday(IEnumerable<AHolyDay> holidays)
        {
            Holidays.AddRange(holidays);
            return this;
        }

        public WorkingDaysConfig Holiday(AHolyDay holiday) => Holiday(new List<AHolyDay>() {holiday});

        public WorkingDaysConfig Holiday(int day, int month) => Holiday(new HoliDay(day, month));



    }
}