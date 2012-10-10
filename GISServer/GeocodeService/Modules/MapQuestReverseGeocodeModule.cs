using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace GeocodeService.Modules
{
    public class MapQuestReverseGeocodeModule : Nancy.NancyModule
    {
        public MapQuestReverseGeocodeModule()
            : base("rest/services/MapQuest/GeocodeServer/")
        {
            Get["/reverseGeocode"] = parameters =>
                {
                    var model = new Models.ReverseGeocode();

                    var exception = new GISServer.Core.Exception();
                    exception.Error = new GISServer.Core.Error
                    {
                        Code = 400,
                        Message = "Unable to perform reverse geocode operation."
                    };

                    #region Parameter
                    

                    if (Request.Query["outSR"].Value != null)
                    {
                        int value;
                        var successful = int.TryParse(Request.Query["outSR"].Value, out value);
                        if (successful)
                        {
                            model.SpatialReference = value;
                        }
                        else
                        {
                            if (!model.HasException)
                            {
                                model.HasException = true;
                            }
                            exception.Error.Details.Add("'outSR' parameter is invalid");
                        }

                    }
                    if (Request.Query["distance"].Value != null)
                    {
                        int value;
                        var successful = int.TryParse(Request.Query["distance"].Value, out value);
                        if (successful)
                        {
                            model.Distance = value;
                        }
                        else
                        {
                            if (!model.HasException)
                            {
                                model.HasException = true;
                            }
                            exception.Error.Details.Add("'distance' parameter is invalid");
                        }

                    }
                    else
                    {
                        //Asigning the default value
                        model.Distance = 0;
                    }

                    if (Request.Query["location"].Value != null)
                    {
                        if (!((String)Request.Query["location"].Value).Trim().Equals(String.Empty))
                        {
                            model.Location = Request.Query["location"].Value;   
                        }
                        else
                        {
                            if (!model.HasException)
                            {
                                model.HasException = true;
                            }
                            exception.Error.Details.Add("'location' parameter is required");
                        }
                    }
                    else
                    {
                        if (!model.HasException)
                        {
                            model.HasException = true;
                        }
                        exception.Error.Details.Add("'location' parameter is required");
                    }

                    if (Request.Query["f"].Value == "json")
                    {
                        model.Format = Request.Query["f"].Value;
                    }
                    else
                    {
                        //Default Value
                        model.Format = "HTML";
                    }
                    #endregion

                    var ReverseGeocodeAddress = new GISServer.Core.Client.GeocodeService.Address();

                    //if ((model.InputGeometries != null) && (!model.HasException))
                    //{
                    //    try
                    //    {
                    //        input_geom_array = GISServer.Core.Client.Utilities.Parser.GetGeometies(model.InputGeometries);
                    //        input_geom_array.Project(model.InputSpatialReference, model.OutputSpatialReference);
                    //    }
                    //    catch (Exception)
                    //    {
                    //        if (!model.HasException)
                    //        {
                    //            model.HasException = true;
                    //        }
                    //        exception.Error.Details.Add("Unable to project the geometry");
                    //    }
                    //}

                    if (!model.HasException)
                    {
                        try
                        {
                            //WebProxy _proxy = new System.Net.WebProxy("http://proxy:8080", true);
                            //_proxy.UseDefaultCredentials = false;

                            //_proxy.Credentials = CredentialCache.DefaultCredentials;
                            //_proxy.Credentials = new NetworkCredential("nimx", "", "CT");


                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://www.mapquestapi.com/geocoding/v1/reverse?key=Fmjtd%7Cluuanu682g%2C85%3Do5-96aghf&json={location:{latLng:{lat:40.0755,lng:-76.329999}}}");
                            //myReq.Proxy = _proxy;

                            //myReq.Credentials=new 
                            var response = myReq.GetResponse();
                            var receiveStream = response.GetResponseStream();
                            // Pipes the stream to a higher level stream reader with the required encoding format. 
                            var readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8);
                            var result= readStream.ReadToEnd();

                            var ser = GISServer.Core.Client.GeocodeService.Mapquest.Util.Parse(result);


                            ReverseGeocodeAddress.Location = new GISServer.Core.Client.Geometry.Point(ser.results[0].locations[0].latLng.lng, ser.results[0].locations[0].latLng.lat, 4326);

                            var att = new GISServer.Core.Attributes("adminArea1",ser.results[0].locations[0].adminArea1,"adminArea1Type", ser.results[0].locations[0].adminArea1Type);

                            ReverseGeocodeAddress.address = att;

                        }
                        catch (Exception)
                        {
                            if (!model.HasException)
                            {
                                model.HasException = true;
                            }
                            exception.Error.Details.Add("Unable to perform reverse geocode operation");
                        }
                    }


                    if ((Request.Query["outSR"].Value == null) && (Request.Query["distance"].Value == null) && (Request.Query["location"].Value == null) && !model.Format.Equals("json"))
                    {
                        model.Exception = "";
                        return View["Reversegeocode",model];
                    }


                    if (!model.HasException)
                    {
                        model.Result = ReverseGeocodeAddress.toJson();
                    }
                    else
                    {
                        model.Exception = exception.ToJson();
                    }


                    if (model.Format.Equals("json"))
                    {
                        if (!model.HasException)
                        {
                            return Services.Utilities.GetJsonResponseObject(model.Result);
                        }
                        else
                        {
                            return Services.Utilities.GetJsonResponseObject(model.Exception);
                        }

                    }
                    else
                    {
                        return View["Reversegeocode", model];
                    }
                };
        }
    }
}