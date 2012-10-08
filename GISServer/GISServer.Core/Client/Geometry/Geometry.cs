using Newtonsoft.Json;
namespace GISServer.Core.Client.Geometry
{
    public abstract class Geometry
    {
        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
