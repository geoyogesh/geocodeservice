using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GISServer.Core.Client.GeocodeService.Mapquest
{

    public static class Util
    {
        public static ReverseGeocodeResult Parse(string responsestring)
        {
            return JsonConvert.DeserializeObject<ReverseGeocodeResult>(responsestring);
        }
    }


    public class LatLng
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class DisplayLatLng
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class Location
    {
        public LatLng latLng { get; set; }
        public string adminArea4 { get; set; }
        public string adminArea5Type { get; set; }
        public string adminArea4Type { get; set; }
        public string adminArea5 { get; set; }
        public string street { get; set; }
        public string adminArea1 { get; set; }
        public string adminArea3 { get; set; }
        public string type { get; set; }
        public DisplayLatLng displayLatLng { get; set; }
        public int linkId { get; set; }
        public string postalCode { get; set; }
        public string sideOfStreet { get; set; }
        public bool dragPoint { get; set; }
        public string adminArea1Type { get; set; }
        public string geocodeQuality { get; set; }
        public string geocodeQualityCode { get; set; }
        public string mapUrl { get; set; }
        public string adminArea3Type { get; set; }
    }

    public class LatLng2
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class ProvidedLocation
    {
        public LatLng2 latLng { get; set; }
    }

    public class Result
    {
        public List<Location> locations { get; set; }
        public ProvidedLocation providedLocation { get; set; }
    }

    public class Options
    {
        public bool ignoreLatLngInput { get; set; }
        public int maxResults { get; set; }
        public bool thumbMaps { get; set; }
    }

    public class Copyright
    {
        public string text { get; set; }
        public string imageUrl { get; set; }
        public string imageAltText { get; set; }
    }

    public class Info
    {
        public Copyright copyright { get; set; }
        public int statuscode { get; set; }
        public List<object> messages { get; set; }
    }

    public class ReverseGeocodeResult
    {
        public List<Result> results { get; set; }
        public Options options { get; set; }
        public Info info { get; set; }
    }
}
