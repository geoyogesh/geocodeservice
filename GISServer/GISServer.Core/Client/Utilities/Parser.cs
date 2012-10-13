using System;
using System.Collections.Generic;
using GISServer.Core.Client.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GISServer.Core.Client.Utilities
{
    public static class Parser
    {
        public static GeometryArray GetGeometies(string GeometryString)
        {
            GeometryArray geometryarray = new GeometryArray();
            try
            {
                JObject geometryjobject = JObject.Parse(GeometryString);
                geometryarray.GeometryType = (string)geometryjobject["geometryType"];
                JArray geometriesjarray = (JArray)geometryjobject["geometries"];
                switch (geometryarray.GeometryType)
                {
                    case "GeometryPoint":
                    case "esriGeometryPoint":
                        {
                            geometryarray.GeometryType = "esriGeometryPoint";
                            geometryarray.Geometries = new List<Point>();
                            for (int i = 0; i < geometriesjarray.Count; i++)
                            {
                                var point = geometriesjarray[i].ToObject<Point>();
                                geometryarray.Geometries.Add(new Point(point.X, point.Y));
                            }
                            break;
                        }
                    case "GeometryMultipoint":
                    case "esriGeometryMultipoint":
                        {
                            geometryarray.GeometryType = "esriGeometryMultipoint";
                            geometryarray.Geometries = new List<MultiPoint>();
                            int convertertype = 0;
                            for (int i = 0; i < geometriesjarray.Count; i++)
                            {
                                MultiPoint multipoint = null;
                                try
                                {
                                    if (convertertype == 0)
                                    {
                                        multipoint = JsonConvert.DeserializeObject<MultiPoint>(geometriesjarray[i].ToString(), new RawPointConverter());
                                    }
                                    else if (convertertype == 1)
                                    {
                                        multipoint = geometriesjarray[i].ToObject<MultiPoint>();
                                    }
                                }
                                catch (System.Exception)
                                {
                                    multipoint = geometriesjarray[i].ToObject<MultiPoint>();
                                    convertertype = 1;
                                }

                                geometryarray.Geometries.Add(multipoint);
                            }
                            break;
                        }
                    case "GeometryPolyline":
                    case "esriGeometryPolyline":
                        {
                            geometryarray.GeometryType = "esriGeometryPolyline";
                            geometryarray.Geometries = new List<Polyline>();
                            int convertertype = 0;
                            for (int i = 0; i < geometriesjarray.Count; i++)
                            {
                                Polyline polyline = null;
                                try
                                {
                                    if (convertertype == 0)
                                    {
                                        polyline = JsonConvert.DeserializeObject<Polyline>(geometriesjarray[i].ToString(), new RawPointConverter());
                                    }
                                    else if (convertertype == 1)
                                    {
                                        polyline = geometriesjarray[i].ToObject<Polyline>();
                                    }
                                }
                                catch (System.Exception)
                                {
                                    polyline = geometriesjarray[i].ToObject<Polyline>();
                                    convertertype = 1;
                                }
                                geometryarray.Geometries.Add(polyline);
                            }
                            break;
                        }
                    case "GeometryPolygon":
                    case "esriGeometryPolygon":
                        {
                            geometryarray.GeometryType = "esriGeometryPolygon";
                            geometryarray.Geometries = new List<Polygon>();
                            int convertertype = 0;
                            for (int i = 0; i < geometriesjarray.Count; i++)
                            {
                                Polygon polygon = null;
                                try
                                {
                                    if (convertertype == 0)
                                    {
                                        polygon = JsonConvert.DeserializeObject<Polygon>(geometriesjarray[i].ToString(), new RawPointConverter());
                                    }
                                    else if (convertertype == 1)
                                    {
                                        polygon = geometriesjarray[i].ToObject<Polygon>();
                                    }
                                }
                                catch (System.Exception)
                                {
                                    polygon = geometriesjarray[i].ToObject<Polygon>();
                                    convertertype = 1;
                                }
                                geometryarray.Geometries.Add(polygon);
                            }
                            break;
                        }
                    case "GeometryEnvelope":
                    case "esriGeometryEnvelope":
                        {
                            geometryarray.GeometryType = "esriGeometryEnvelope";
                            geometryarray.Geometries = new List<Envelope>();
                            for (int i = 0; i < geometriesjarray.Count; i++)
                            {
                                var envelope = geometriesjarray[i].ToObject<Envelope>();
                                geometryarray.Geometries.Add(envelope);
                            }
                            break;
                        }
                    default:
                        {
                            //geometry not implimented
                            break;
                        }
                }
                return geometryarray;
            }
            catch (System.Exception)
            {


                var xy = GeometryString.Trim().Split(new Char[] { ',' });

                if (xy.Length % 2 == 0)
                {
                    geometryarray.GeometryType = "esriGeometryPoint";
                    geometryarray.Geometries = new List<Point>();

                    for (int i = 0; i < xy.Length; i++)
                    {
                        var x = Convert.ToDouble(xy[i]);
                        i++;
                        var y = Convert.ToDouble(xy[i]);
                        geometryarray.Geometries.Add(new Point(x, y));
                    }
                    return geometryarray;
                }
                else
                {
                    throw;
                }
            }

        }

        public static List<Polygon> getServerPolygonArray(string polygonJsonString)
        {
            var polygonJarray = (JArray)JsonConvert.DeserializeObject(polygonJsonString);
            List<Polygon> polygonlist = new List<Polygon>();
            int convertertype = 0;
            for (int i = 0; i < polygonJarray.Count; i++)
            {
                Polygon polygon = null;
                try
                {
                    if (convertertype == 0)
                    {
                        polygon = JsonConvert.DeserializeObject<Polygon>(polygonJarray[i].ToString(), new RawPointConverter());
                    }
                    else if (convertertype == 1)
                    {
                        polygon = polygonJarray[i].ToObject<Polygon>();
                    }
                }
                catch (System.Exception)
                {
                    polygon = polygonJarray[i].ToObject<Polygon>();
                    convertertype = 1;
                }
                polygonlist.Add(polygon);
            }

            return polygonlist;
        }

        public static List<Polyline> getServerPolylineArray(string polylineJsonString)
        {
            var polylinejarray = (JArray)JsonConvert.DeserializeObject(polylineJsonString);
            List<Polyline> polylinelist = new List<Polyline>();
            int convertertype = 0;
            for (int i = 0; i < polylinejarray.Count; i++)
            {
                Polyline polyline = null;
                try
                {
                    if (convertertype == 0)
                    {
                        polyline = JsonConvert.DeserializeObject<Polyline>(polylinejarray[i].ToString(), new RawPointConverter());
                    }
                    else if (convertertype == 1)
                    {
                        polyline = polylinejarray[i].ToObject<Polyline>();
                    }
                }
                catch (System.Exception)
                {
                    polyline = polylinejarray[i].ToObject<Polyline>();
                    convertertype = 1;
                }
                polylinelist.Add(polyline);
            }
            return polylinelist;

        }

        public static SingleGeometry getSingleGeometry(string GeometryString)
        {
            SingleGeometry geometryarray = new SingleGeometry();
            try
            {
                JObject geometryjobject = JObject.Parse(GeometryString);
                geometryarray.GeometryType = (string)geometryjobject["geometryType"];
                JObject geometriesjarray = (JObject)geometryjobject["geometry"];
                switch (geometryarray.GeometryType)
                {
                    case "GeometryPoint":
                    case "esriGeometryPoint":
                        {
                            geometryarray.GeometryType = "esriGeometryPoint";
                            var point = geometriesjarray.ToObject<Point>();
                            geometryarray.geometry = new Point(point.X, point.Y);
                            break;
                        }
                    case "GeometryMultipoint":
                    case "esriGeometryMultipoint":
                        {
                            geometryarray.GeometryType = "esriGeometryMultipoint";

                            int convertertype = 0;

                            MultiPoint multipoint = null;
                            try
                            {
                                if (convertertype == 0)
                                {
                                    multipoint = JsonConvert.DeserializeObject<MultiPoint>(geometriesjarray.ToString(), new RawPointConverter());
                                }
                                else if (convertertype == 1)
                                {
                                    multipoint = geometriesjarray.ToObject<MultiPoint>();
                                }
                            }
                            catch (System.Exception)
                            {
                                multipoint = geometriesjarray.ToObject<MultiPoint>();
                                convertertype = 1;
                            }

                            geometryarray.geometry = multipoint;

                            break;
                        }
                    case "GeometryPolyline":
                    case "esriGeometryPolyline":
                        {
                            geometryarray.GeometryType = "esriGeometryPolyline";

                            int convertertype = 0;

                            Polyline polyline = null;
                            try
                            {
                                if (convertertype == 0)
                                {
                                    polyline = JsonConvert.DeserializeObject<Polyline>(geometriesjarray.ToString(), new RawPointConverter());
                                }
                                else if (convertertype == 1)
                                {
                                    polyline = geometriesjarray.ToObject<Polyline>();
                                }
                            }
                            catch (System.Exception)
                            {
                                polyline = geometriesjarray.ToObject<Polyline>();
                                convertertype = 1;
                            }
                            geometryarray.geometry = polyline;

                            break;
                        }
                    case "GeometryPolygon":
                    case "esriGeometryPolygon":
                        {
                            geometryarray.GeometryType = "esriGeometryPolygon";

                            int convertertype = 0;

                            Polygon polygon = null;
                            try
                            {
                                if (convertertype == 0)
                                {
                                    polygon = JsonConvert.DeserializeObject<Polygon>(geometriesjarray.ToString(), new RawPointConverter());
                                }
                                else if (convertertype == 1)
                                {
                                    polygon = geometriesjarray.ToObject<Polygon>();
                                }
                            }
                            catch (System.Exception)
                            {
                                polygon = geometriesjarray.ToObject<Polygon>();
                                convertertype = 1;
                            }
                            geometryarray.geometry = polygon;

                            break;
                        }
                    case "GeometryEnvelope":
                    case "esriGeometryEnvelope":
                        {
                            geometryarray.GeometryType = "esriGeometryEnvelope";


                            var envelope = geometriesjarray.ToObject<Envelope>();
                            geometryarray.geometry = envelope;

                            break;
                        }
                    default:
                        {
                            //geometry not implimented
                            break;
                        }
                }
                return geometryarray;
            }
            catch (System.Exception)
            {
                var xy = GeometryString.Trim().Split(new Char[] { ',' });

                if (xy.Length % 2 == 0)
                {
                    geometryarray.GeometryType = "esriGeometryPoint";
                    var x = Convert.ToDouble(xy[0]);
                    var y = Convert.ToDouble(xy[1]);
                    geometryarray.geometry = new Point(x, y);
                    return geometryarray;
                }
                else
                {
                    throw;
                }
            }

        }
    }
}
