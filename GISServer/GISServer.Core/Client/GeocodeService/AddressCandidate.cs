using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISServer.Core.Client.Geometry;
using Newtonsoft.Json;

namespace GISServer.Core.Client.GeocodeService
{
    public class AddressCandidate
    {
         [JsonProperty("address")]
        public String Address { get; set; }
         [JsonProperty("location")]
        public Point Location { get; set; }
         [JsonProperty("score")]
        public Double Score { get; set; }
         [JsonProperty("attributes")]
         public List<Dictionary<string, string>> Attributes { get; set; }
    }
}