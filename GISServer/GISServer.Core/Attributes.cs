using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GISServer.Core
{
    [JsonConverter(typeof(AttributesSerializer))]
    public class Attributes:Base.JSONSerialization
    {
        public Attributes()
        {
            AttributeCollection = new List<Attribute>();
        }

        public Attributes(params string[] keyvalues)
        {
            if ((keyvalues.Length % 2)==0)
            {
                AttributeCollection = new List<Attribute>();
                for (int i = 0; i < keyvalues.Length; i=i+2)
                {
                   AttributeCollection.Add(new Attribute(keyvalues[i], keyvalues[i+1]));
                }
            }
            
        }

        public List<Attribute> AttributeCollection { get; set; }
    }

    public class AttributesSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = value as Attributes;

            writer.WriteStartObject();
            foreach (var item in name.AttributeCollection)
            {

                writer.WritePropertyName(item.Key);
                serializer.Serialize(writer, item.Value);

            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Attributes).IsAssignableFrom(objectType);
        }
    }
}
