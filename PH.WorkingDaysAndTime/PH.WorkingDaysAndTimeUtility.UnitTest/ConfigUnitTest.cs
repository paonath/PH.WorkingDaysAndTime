using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PH.WorkingDaysAndTimeUtility.Configuration;
using Xunit;

namespace PH.WorkingDaysAndTimeUtility.UnitTest
{
    public class ConfigUnitTest : BaseTest
    {
        [Fact]
        public void SerializeTest()
        {
            var w = GetSimpleWeek();
            var l = GetItalianHolidays();

            var easterMonday = new EasterMonday();
            var ser = JsonConvert.SerializeObject(easterMonday);

            var easter2 = JsonConvert.DeserializeObject<EasterMonday>(ser);



            var s = new AHolyDay(15, 8);
            var ss = JsonConvert.SerializeObject(s);


            var ww = JsonConvert.SerializeObject(l);


            var deserializeddList = JsonConvert.DeserializeObject<List<AHolyDay>>(ww);



            var cfg = new WorkingDaysConfig(w, l);


            var serialized = JsonConvert.SerializeObject(cfg, Formatting.Indented);

            var d = JsonConvert.DeserializeObject<WorkingDaysConfig>(serialized);

            var u = new WorkingDaysAndTimeUtility(d);

            var e = WorkingDaysAndTimeUtility.TryParseConfig(serialized, out IWorkingDaysAndTimeUtility uu);

            Assert.True(e);
            Assert.NotNull(uu);
            Assert.NotNull(u);
            Assert.True(uu is IWorkingDaysAndTimeUtility utility);

            Assert.True(uu is WorkingDaysAndTimeUtility utility2);

        }

        [Fact]
        public void SerializeTest2()
        {
            var w = GetSimpleWeek();
            var l = GetItalianHolidays();

            var cfg = new WorkingDaysConfig(w, l);


            var serialized = JsonConvert.SerializeObject(cfg, Formatting.Indented);

            var d = JsonConvert.DeserializeObject<WorkingDaysConfig>(serialized);

            var u = new WorkingDaysAndTimeUtility(d);

            var e = WorkingDaysAndTimeUtility.TryParseConfig(serialized, out IWorkingDaysAndTimeUtility uu);

            Assert.True(e);
            Assert.NotNull(uu);
            Assert.NotNull(u);
            Assert.True(uu is IWorkingDaysAndTimeUtility utility);

            Assert.True(uu is WorkingDaysAndTimeUtility utility2);

        }

        [Fact]
        public void MappedTest()
        {
            var cfg = new WorkingDaysConfig()
                      .Week(new WeekDaySpan().Day(DayOfWeek.Monday,
                                                  new WorkDaySpan()
                                                      .Time(new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0))
                                                      .Time(new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0)))
                                             .Day(DayOfWeek.Tuesday,
                                                  new WorkDaySpan()
                                                      .Time(new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0))
                                                      .Time(new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0)))
                           )
                      .Holiday(new AHolyDay(15, 8))
                      .Holiday(2, 6)
                      .Holiday(new EasterMonday());

            var cfg2 = new WorkingDaysConfig().Week(WeekDaySpan.CreateSymmetricalConfig(new WorkDaySpan()
                                                                                        .Time(new TimeSpan(9, 0, 0),
                                                                                              new TimeSpan(13, 0, 0))
                                                                                        .Time(new TimeSpan(14, 0, 0),
                                                                                              new TimeSpan(18, 0, 0)),
                                                                                        new DayOfWeek[]
                                                                                        {
                                                                                            DayOfWeek.Monday,
                                                                                            DayOfWeek.Tuesday,
                                                                                            DayOfWeek.Wednesday,
                                                                                            DayOfWeek.Thursday,
                                                                                            DayOfWeek.Friday
                                                                                        }));


        }
    }
}