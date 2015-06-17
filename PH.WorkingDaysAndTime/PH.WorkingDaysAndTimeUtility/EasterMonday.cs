using System;

namespace PH.WorkingDaysAndTimeUtility
{
    public class EasterMonday : EasterSunday
    {
        public override DateTime Calculate(int year)
        {
            return base.Calculate(year).AddDays(1);

        }
    }
}