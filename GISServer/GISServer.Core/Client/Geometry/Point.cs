using System;
using Newtonsoft.Json;

namespace GISServer.Core.Client.Geometry
{
    public class Point : Geometry
    {
        public Point()
        {
        }

        public Point(Double X, Double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Point(Double X, Double Y, SpatialReference SpatialReference)
        {
            this.X = X;
            this.Y = Y;
            this.SpatialReference = SpatialReference;
        }

        public Point(Double X, Double Y, int WKID)
        {
            this.X = X;
            this.Y = Y;
            this.SpatialReference = new SpatialReference(WKID);
        }

        public Point(Double X, Double Y, string WKT)
        {
            this.X = X;
            this.Y = Y;
            this.SpatialReference = new SpatialReference(WKT);
        }

        [JsonProperty("x")]
        public Double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
