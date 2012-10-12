using System.Collections.Generic;
using GISServer.Core.Client.Geometry;
using Newtonsoft.Json;

namespace GISServer.Core.Client.GeocodeService
{
    public class Addresses : GISServer.Core.Base.JSONSerialization
    {
        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
        [JsonProperty("candidates")]
        public List<AddressCandidate> Candidates { get; set; }
    }
}