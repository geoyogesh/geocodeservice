using System;
using Newtonsoft.Json;

namespace GISServer.Core.Catalog
{
    public class Service
    {
        public Service()
        {
        }

        public Service(String Name,String Type)
        {
            this.Name = Name;
            this.Type = Type;
        }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
