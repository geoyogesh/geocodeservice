using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeocodeService.Models
{
    public class ReverseGeocode
    {
        public ReverseGeocode()
        {
            HasException = false;
            HasResult = false;
        }

        public string Location { get; set; }

        public int Distance { get; set; }

        public int SpatialReference { get; set; }

        public String Format { get; set; }

        public string Result { get; set; }

        public string Exception { get; set; }

        public bool HasException { get; set; }

        public bool HasResult { get; set; }
    }
}