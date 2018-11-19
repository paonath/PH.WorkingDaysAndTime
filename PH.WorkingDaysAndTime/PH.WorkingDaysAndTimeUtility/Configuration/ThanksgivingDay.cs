using System;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    public abstract class BaseThanksgivingDay : CalculatedHoliDay
    {
        protected BaseThanksgivingDay()
            : base(0, 0)
        {
        }
    }

    /// <summary>
    /// U.S. Thanksgiving Day
    ///
    /// the fourth Thursday of November
    /// </summary>
    public class ThanksgivingDay : BaseThanksgivingDay
    {
        public ThanksgivingDay():base()
           
        {
        }
        public override DateTime Calculate(int year)
        {
            var lastNovember = new DateTime(year,11,30);
            var pThursday    = lastNovember.AddDays(-7);

            if (pThursday.DayOfWeek == DayOfWeek.Thursday)
            {
                return pThursday;
            }
            else
            {
                var diff = ((int)pThursday.DayOfWeek -(int)DayOfWeek.Thursday);
		
                var fThursday = pThursday.AddDays( -(diff) );
                return fThursday;
            }
        }

        public override Type GetHolyDayType()
        {
            return typeof(ThanksgivingDay);
        }
    }

    /// <summary>
    /// U.S. Thanksgiving Day
    ///
    /// the fourth Thursday of November
    /// </summary>
    public class USThanksgivingDay : ThanksgivingDay
    {
        public USThanksgivingDay() : base()
        {
        }

        

        public override Type GetHolyDayType()
        {
            return typeof(USThanksgivingDay);
        }
    }

    /// <summary>
    ///Canadian hanksgiving Day
    /// 
    /// the second Monday of October
    /// </summary>
    public class CanadianThanksgivingDay : BaseThanksgivingDay
    {
        public CanadianThanksgivingDay() : base()
        {
        }

        public override DateTime Calculate(int year)
        {
            var oct7 = new DateTime(year,10,7);
            if (oct7.DayOfWeek == DayOfWeek.Monday)
                return oct7;
            else
            {
                var diff = ((int)oct7.DayOfWeek -(int)DayOfWeek.Thursday);
		
                var secMonday = oct7.AddDays( -(diff) );
                return secMonday;
            }
        }

        public override Type GetHolyDayType()
        {
            return typeof(CanadianThanksgivingDay);
        }
    }
}