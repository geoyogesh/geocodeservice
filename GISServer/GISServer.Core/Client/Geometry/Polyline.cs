using System.Collections.Generic;
using Newtonsoft.Json;

namespace GISServer.Core.Client.Geometry
{
    public class Polyline : Geometry
    {
        public Polyline()
        {
            Paths = new List<PointCollection>();
        }

         [JsonProperty("paths")]
        public List<PointCollection> Paths { get; set; }

         [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
