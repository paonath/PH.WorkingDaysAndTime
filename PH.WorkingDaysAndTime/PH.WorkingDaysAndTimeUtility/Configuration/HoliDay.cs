using System;
using Newtonsoft.Json;
using PH.WorkingDaysAndTimeUtility.Converter;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    //[JsonConverter(typeof(BaseDayJsonConverter))]

    //[JsonConverter(typeof(ADayJsonConverter))]
    public class ADay : BaseDay
    {
       

        public ADay(int day,int mont):base(day,mont)
        {
         
        }

        

        /// <summary>
        /// It returns an instance of the data by year provided
        /// </summary>
        /// <param name="year">year provided</param>
        /// <returns>DateTime</returns>
        public override DateTime Calculate(int year)
        {
            return new DateTime(year,this.Month,this.Day);
        }

        

        public override int GetHashCode()
        {
            return $"{ToString()} {nameof(ADay)}".GetHashCode();
        }

        public override string ToString()
        {
            return $"Day: {this.Day} Month: {this.Month}";

        }
    }

    /// <summary>
    /// A <see cref="ADay"/> override with check on valid day and month
    /// based on 2016 leap year.
    /// </summary>
    //[JsonConverter(typeof(DayJsonConverter))]
    public class Day : ADay
    {
        public Day(int day, int mont) : base(day, mont)
        {
            PerformCheckOnStart();

        }
    }

    [JsonConverter(typeof(AHolyDayJsonConverter))]
    public class AHolyDay : ADay
    {
        [JsonIgnore]
        public virtual bool Calculated { get; }

        public AHolyDay(int day, int mont) : base(day, mont)
        {
            Calculated = false;
        }
    }

    /// <summary>
    /// Holiday: a non-working day.
    /// 
    /// This is a generic-instance, with <see cref="ADay.Day">Day</see> 
    /// and <see cref="ADay.Month">Month</see>.
    /// </summary>
    //[JsonConverter(typeof(HoliDaysonConverter))]
    public class HoliDay : CalculatedHoliDay
    {
        [JsonIgnore]
        public override bool Calculated { get; }

        [JsonIgnore]
        public override Type Type => GetHolyDayType();

        public HoliDay(int day,int mont)
            :base(day,mont)
        {
            Calculated = false;
            PerformCheckOnStart();
        }


        public override string ToString()
        {
            return $"Day: {this.Day} Month: {this.Month}";
        }


        public override Type GetHolyDayType()
        {
            return typeof(HoliDay);
        }
    }
}