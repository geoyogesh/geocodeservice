using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISServer.Core.Client.Geometry;
using Newtonsoft.Json;

namespace GISServer.Core.Client.GeocodeService
{
    public class Root
    {
        [JsonProperty("serviceDescription")]
        public string ServiceDescription { get; set; }
        [JsonProperty("addressFields")]
        public List<AddressField> AddressFields { get; set; }
        [JsonProperty("singleLineAddressField")]
        public AddressField SingleLineAddressField { get; set; }
        [JsonProperty("candidateFields")]
        public List<AddressField> CandidateFields { get; set; }
        [JsonProperty("intersectionCandidateFields")]
        public List<AddressField> IntersectionCandidateFields { get; set; }
        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
        [JsonProperty("locatorProperties")]
        public KeyValuePairs LocatorProperties { get; set; }
    }
}