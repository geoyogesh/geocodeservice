﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISServer.Core.Client.Geometry;
using Newtonsoft.Json;

namespace GISServer.Core.Client.GeocodeService
{
    public class Address
    {
         [JsonProperty("address")]
        public String address { get; set; }
         [JsonProperty("location")]
        public Point Location { get; set; }
    }
}