using System;
using GISServer.Core.Client.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GISServer.Core.Client.Utilities
{
    public class RawPointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Point))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray jarray = new JArray();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray) break;
                jarray.Add(reader.Value);
            }
            Point point = new Point();
            point.X = Double.Parse(jarray[0].ToString());
            point.Y = Double.Parse(jarray[1].ToString());
            return point;
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteRawValue(String.Format("{0},{1}", ((Point)value).X, ((Point)value).Y));
            writer.WriteEndArray();
        }
    }
}
