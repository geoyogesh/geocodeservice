using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeocodeService.Models
{
    public class Geocode
    {
        public Geocode()
        {
            HasException = false;
            HasResult = false;
        }

        public int Address { get; set; }

        public string City { get; set; }

        public String State { get; set; }

        public String Zip { get; set; }

        public String OutFields { get; set; }

        public int SpatialReference { get; set; }

        public String Format { get; set; }

        public string Result { get; set; }

        public string Exception { get; set; }

        public bool HasException { get; set; }

        public bool HasResult { get; set; }
    }
}