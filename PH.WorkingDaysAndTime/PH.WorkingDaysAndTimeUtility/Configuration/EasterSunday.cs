using System;

namespace PH.WorkingDaysAndTimeUtility.Configuration
{
    /// <summary>
    /// Easter Sunday HoliDay 
    /// 
    /// As in <see cref="EasterSunday()">ctor</see> for Easter Sunday
    /// there are no supplied parameters for day and month.
    /// 
    /// <see cref="http://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays"/>
    /// <seealso cref="http://www.codeproject.com/Articles/11666/Dynamic-Holiday-Date-Calculator"/>
    /// </summary>
    public class EasterSunday : CalculatedHoliDay
    {
        public EasterSunday()
            : base(0, 0)
        {
        }

        /// <summary>
        /// Calculate Easter Sunday
        /// 
        /// This is the "Oskar Wieland's algorithm in C#" made by Jan Schreuder: <see cref="http://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays"/>. 
        /// </summary>
        /// <param name="year">year provided</param>
        /// <returns>Easter Sunday DateTime for provided year</returns>
        public override DateTime Calculate(int year)
        {
            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25)
                                                + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) *
                        (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            int day = i - ((year + (int)(year / 4) +
                          i + 2 - c + (int)(c / 4)) % 7) + 28;
            int month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }

        public override Type GetHolyDayType()
        {
            return typeof(EasterSunday);
        }

    }
}
