using System.Collections.Generic;
using Newtonsoft.Json;

namespace GISServer.Core.Client.Geometry
{
    public class MultiPoint:Geometry
    {
        public MultiPoint()
        {
            Points = new List<Point>();
        }

        [JsonProperty("points")]
        public List<Point> Points { get; set; }

        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
