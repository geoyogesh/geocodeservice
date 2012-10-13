using System;
using Newtonsoft.Json;

namespace GISServer.Core.Client.Geometry
{
    public class SingleGeometry
    {
        [JsonProperty("geometryType")]
        public String GeometryType { get; set; }

        [JsonProperty("geometry")]
        public Geometry geometry { get; set; }

        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }

        public string ToJson()
        {
            if ((GeometryType != null) && (GeometryType.Equals("esriGeometryMultipoint") || GeometryType.Equals("esriGeometryPolyline") || GeometryType.Equals("esriGeometryPolygon")))
            {
                return Utilities.Serializer.ToJson4Collection(this);
            }
            else
            {
                return Utilities.Serializer.ToJson(this);
            }
        }
    }
}
