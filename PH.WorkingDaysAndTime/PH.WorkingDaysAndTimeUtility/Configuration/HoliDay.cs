using System;
using Newtonsoft.Json;
using PH.WorkingDaysAndTimeUtility.Converter;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    //[JsonConverter(typeof(BaseDayJsonConverter))]
    public abstract class BaseDay
    {
        private readonly int _day;

        /// <summary>
        /// Day
        /// </summary>
        public int Day => _day;

        private readonly int _month;

        /// <summary>
        /// Month
        /// </summary>
        public int Month => _month;

        protected BaseDay(int day,int mont)
        {
            _day = day;
            _month = mont;

        }

        /// <summary>
        /// Perform check on valid Day and Month combination
        ///
        /// <exception cref="ArgumentOutOfRangeException">The date is not a valid Day and Month combination</exception>
        /// </summary>
        protected void PerformCheckOnStart()
        {
            var d = new DateTime(2016, Month,Day);
        }

        public abstract DateTime Calculate(int year);

        public override bool Equals(object obj)
        {
            return obj?.GetHashCode() == GetHashCode();
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return $"{_month}-{_day}".GetHashCode();
        }
    }

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
    /// A Calculated-runtime Day, with no checks.
    /// </summary>
    [JsonConverter(typeof(CalculatedHoliDaysonConverter))]
    public abstract class CalculatedHoliDay : AHolyDay
    {

        public virtual Type Type => GetHolyDayType();

        public override bool Calculated { get; }

        protected CalculatedHoliDay(int day, int mont) 
            : base(day, mont)
        {
            Calculated = true;
        }

        public override string ToString()
        {
            return AHolyDayToString();
        }

        public override int GetHashCode()
        {
            return $"{AHolyDayToString()}  Day: {this.Day} Month: {this.Month}".GetHashCode();
        }

        public abstract Type GetHolyDayType();

        public string AHolyDayToString()
        {
            return $"{GetHolyDayType().Name}" ;
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