using System;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// Easter Monday, one day after EasterSunday.
    /// 
    /// <see cref="EasterSunday"/>
    /// </summary>
    public class EasterMonday : EasterSunday
    {

        public override DateTime Calculate(int year)
        {
            
            return base.Calculate(year).AddDays(1);

        }
    }
}