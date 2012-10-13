
using Newtonsoft.Json;
namespace GISServer.Core.Client.Geometry
{
    public class Envelope:Geometry
    {
        public Envelope()
        {
        }
        public Envelope(double XMin, double YMin, double XMax, double YMax)
        {
            this.XMin = XMin;
            this.YMin = YMin;
            this.XMax = XMax;
            this.YMax = YMax;
        }
        public Envelope(double XMin, double YMin, double XMax, double YMax, int spatialReference)
        {
            this.XMin = XMin;
            this.YMin = YMin;
            this.XMax = XMax;
            this.YMax = YMax;
            this.SpatialReference = new SpatialReference(spatialReference);
        }

        public Envelope(double XMin, double YMin, double XMax, double YMax, SpatialReference SpatialReference)
        {
            this.XMin = XMin;
            this.YMin = YMin;
            this.XMax = XMax;
            this.YMax = YMax;
            this.SpatialReference = SpatialReference;
        }

        [JsonProperty("xMin")]
        public double XMin { get; set; }

        [JsonProperty("yMin")]
        public double YMin { get; set; }

        [JsonProperty("xMax")]
        public double XMax { get; set; }

        [JsonProperty("yMax")]
        public double YMax { get; set; }

        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
