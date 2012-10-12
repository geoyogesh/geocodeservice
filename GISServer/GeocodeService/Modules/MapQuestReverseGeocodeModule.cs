using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Configuration;

namespace GeocodeService.Modules
{
    public class MapQuestReverseGeocodeModule : Nancy.NancyModule
    {
        //http://localhost:1979/rest/services/MapQuest/GeocodeServer/reverseGeocode?location=-76.329999%2C40.0755&distance=0&outSR=0&f=HTML
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
                            try
                            {
                                var p = new GISServer.Core.Client.Geometry.Point((string)Request.Query["location"].Value);

                                model.Location = Request.Query["location"].Value;
                                model.ParsedLocation = new String[] { p.X.ToString(), p.Y.ToString() };

                            }
                            catch (Exception)
                            {
                                model.Location = Request.Query["location"].Value;

                                model.ParsedLocation = model.Location.Split(new Char[] { ',' });
                                if (model.ParsedLocation.Length != 2)
                                {
                                    throw new Exception();
                                }

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


                    if (!model.HasException)
                    {
                        try
                        {
                            string url = "http://www.mapquestapi.com/geocoding/v1/reverse?key=" + ConfigurationManager.AppSettings["MapQuestKey"].ToString() + "&json={location:{latLng:{lat:" + model.ParsedLocation[1] + ",lng:" + model.ParsedLocation[0] + "}}}";
                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);

                            var response = myReq.GetResponse();
                            var receiveStream = response.GetResponseStream();
                            // Pipes the stream to a higher level stream reader with the required encoding format. 
                            var readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8);
                            var result = readStream.ReadToEnd();

                            var ser = GISServer.Core.Client.GeocodeService.Mapquest.Util.Parse(result);


                            ReverseGeocodeAddress.Location = new GISServer.Core.Client.Geometry.Point(ser.results[0].locations[0].latLng.lng, ser.results[0].locations[0].latLng.lat, 4326);

                            var att = new GISServer.Core.Attributes("street", ser.results[0].locations[0].street, "adminArea5", ser.results[0].locations[0].adminArea5, "adminArea4", ser.results[0].locations[0].adminArea4, "adminArea3", ser.results[0].locations[0].adminArea3, "adminArea1", ser.results[0].locations[0].adminArea1, "postalCode", ser.results[0].locations[0].postalCode);

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
                        return View["Reversegeocode", model];
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
                            if (Request.Query["callback"].Value != null)
                            {
                                return Services.Utilities.GetJsonResponseObject(String.Format("{0}({1});", (string)Request.Query["callback"].Value, model.Result));
                            }
                            else
                            {
                                return Services.Utilities.GetJsonResponseObject(model.Result);
                            }

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