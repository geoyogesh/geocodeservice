using Newtonsoft.Json;
namespace GISServer.Core.Client.Geometry
{
    public class SpatialReference
    {
        public SpatialReference()
        {
        }

        public SpatialReference(int WKID)
        {
            this.Wkid = WKID;
        }

        public SpatialReference(string WKT)
        {
            this.Wkt = WKT;
        }

        [JsonProperty("wkid")]
        public int Wkid { get; set; }

        [JsonProperty("wkt")]
        public string Wkt { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}