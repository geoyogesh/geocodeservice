﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GISServer.Core.Client.Utilities
{
    public static class Serializer
    {
    public static string ToJsonWithoutFormatting(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None
                });
        }

        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        public static string ToJson4Collection(dynamic obj)
        {
            var jsonconverters = new JsonConverter[]
            {
                new RawPointConverter()
            };
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = jsonconverters,
                Formatting = Formatting.None
            });
        }
    }
}
