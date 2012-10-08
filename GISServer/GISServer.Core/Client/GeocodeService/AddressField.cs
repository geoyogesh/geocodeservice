using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GISServer.Core.Client.GeocodeService
{
    public class AddressField
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("alias")]
        public string Alias { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("required")]
        public bool Required { get; set; }
    }
}
