using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH.WorkingDaysAndTimeUtility
{
    public interface IWorkingDaysAndTimeUtility
    {
        DateTime AddWorkingDays(DateTime start, int days);
        DateTime AddWorkingHours(DateTime start, double hours);
    }
}
