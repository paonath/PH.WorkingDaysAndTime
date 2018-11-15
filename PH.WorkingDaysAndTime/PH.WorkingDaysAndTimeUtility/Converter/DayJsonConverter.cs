using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PH.WorkingDaysAndTimeUtility.Configuration;

namespace PH.WorkingDaysAndTimeUtility.Converter
{
    public class BaseDayJsonConverter : JsonConverter<BaseDay>
    {
        public override void WriteJson(JsonWriter writer, BaseDay value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override BaseDay ReadJson(JsonReader reader, Type objectType, BaseDay existingValue, bool hasExistingValue,
                                         JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class ADayJsonConverter : JsonConverter<ADay>
    {
        public override void WriteJson(JsonWriter writer, ADay value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override ADay ReadJson(JsonReader reader, Type objectType, ADay existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class DayJsonConverter : JsonConverter<Day>
    {
        public override void WriteJson(JsonWriter writer, Day value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(Day));
            serializer.Serialize(writer, value.ToString());
            writer.WriteEndObject();
        }

        public override Day ReadJson(JsonReader reader, Type objectType, Day existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var     properties = jsonObject.Properties().ToList();    
            //properties.Where(x => x.)

            var d = (string) properties.FirstOrDefault(x => x.Name == "Day")?.Value;
            var m = (string) properties.FirstOrDefault(x => x.Name == "Month")?.Value;
            return new Day(int.Parse(d), int.Parse(m));

        }
    }

    public class AHolyDayJsonConverter : JsonConverter<AHolyDay>
    {
        public override void WriteJson(JsonWriter writer, AHolyDay value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Day");
            serializer.Serialize(writer, value.Day);
            writer.WritePropertyName("Month");
            serializer.Serialize(writer, value.Month);
            writer.WriteEndObject();
        }

        public override AHolyDay ReadJson(JsonReader reader, Type objectType, AHolyDay existingValue, bool hasExistingValue,
                                          JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class CalculatedHoliDaysonConverter : JsonConverter<CalculatedHoliDay>
    {
        public override void WriteJson(JsonWriter writer, CalculatedHoliDay value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("CalculatedHoliDay");
            serializer.Serialize(writer, value.AHolyDayToString());
           
            writer.WriteEndObject();
        }

        public override CalculatedHoliDay ReadJson(JsonReader reader, Type objectType, CalculatedHoliDay existingValue, bool hasExistingValue,
                                                   JsonSerializer serializer)
        {
            var o = Activator.CreateInstance(objectType);
            return o as CalculatedHoliDay;
            
        }
    }

    public class HoliDaysonConverter : JsonConverter<HoliDay>
    {
        public override void WriteJson(JsonWriter writer, HoliDay value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Day");
            serializer.Serialize(writer, value.Day);
            writer.WritePropertyName("Month");
            serializer.Serialize(writer, value.Month);
            writer.WriteEndObject();

        }

        public override HoliDay ReadJson(JsonReader reader, Type objectType, HoliDay existingValue, bool hasExistingValue,
                                         JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
