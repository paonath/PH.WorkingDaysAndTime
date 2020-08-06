using System;
using Newtonsoft.Json;
using PH.WorkingDaysAndTimeUtility.Converter;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
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
}