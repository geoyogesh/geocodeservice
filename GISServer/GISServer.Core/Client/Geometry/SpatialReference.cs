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

        public SpatialReference(string JsonString)
        {
            var p = JsonConvert.DeserializeObject<SpatialReference>(JsonString);

            this.Wkid = p.Wkid;
            this.Wkt = p.Wkt;
        }

        [JsonProperty("wkid")]
        public int Wkid { get; set; }

        [JsonProperty("wkt")]
        public string Wkt { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}