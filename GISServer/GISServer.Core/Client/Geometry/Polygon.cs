using System.Collections.Generic;
using Newtonsoft.Json;

namespace GISServer.Core.Client.Geometry
{
    public class Polygon : Geometry
    {
        public Polygon()
        {
            Rings = new List<PointCollection>();
        }

        [JsonProperty("rings")]
        public List<PointCollection> Rings { get; set; }

        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }

    }
}
