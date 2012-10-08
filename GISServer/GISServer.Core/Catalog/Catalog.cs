using System.Collections.Generic;
using Newtonsoft.Json;

namespace GISServer.Core.Catalog
{
    public class Catalog
    {
        public Catalog()
        {
            Services = new List<Service>();
            Folders = new List<string>();
        }
        [JsonProperty("specVersion")]
        public double SpecVersion { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("currentVersion")]
        public double CurrentVersion { get; set; }

        [JsonProperty("folders")]
        public List<string> Folders { get; set; }

        [JsonProperty("services")]
        public List<Service> Services { get; set; }
    }
}
