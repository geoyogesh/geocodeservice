using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Configuration;

namespace GeocodeService.Modules
{
    public class MapQuestFindAddressCandidatesModule : Nancy.NancyModule
    {
        //http://localhost:1979/rest/services/MapQuest/GeocodeServer/findAddressCandidates?Address=Lancaster&City=PA&State=&Zip=&outFields=&outSR=0&f=HTML
        public MapQuestFindAddressCandidatesModule()
            : base("rest/services/MapQuest/GeocodeServer/")
        {
            Get["/findAddressCandidates"] = parameters =>
            {
                var model = new Models.Geocode();

                var exception = new GISServer.Core.Exception();
                exception.Error = new GISServer.Core.Error
                {
                    Code = 400,
                    Message = "Unable to perform find Address Candidates operation."
                };

                #region Parameter


                if (Request.Query["outSR"].Value != null)
                {

                    try
                    {
                        var p = new GISServer.Core.Client.Geometry.SpatialReference((string)Request.Query["outSR"].Value);
                        model.SpatialReference = p.Wkid;
                       
                    }
                    catch (Exception)
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
                }


                if (Request.Query["Address"].Value != null)
                {
                    model.Address = Request.Query["Address"].Value;
                }
                else
                {
                    //Default Value
                    model.Address = "";
                }

                if (Request.Query["City"].Value != null)
                {
                    model.City = Request.Query["City"].Value;
                }
                else
                {
                    //Default Value
                    model.City = "";
                }

                if (Request.Query["State"].Value != null)
                {
                    model.State = Request.Query["State"].Value;
                }
                else
                {
                    //Default Value
                    model.State = "";
                }

                if (Request.Query["Zip"].Value != null)
                {
                    model.Zip = Request.Query["Zip"].Value;
                }
                else
                {
                    //Default Value
                    model.Zip = "";
                }

                if (Request.Query["outFields"].Value != null)
                {
                    model.OutFields = Request.Query["outFields"].Value;
                }
                else
                {
                    //Default Value
                    model.OutFields = "";
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


                if ((Request.Query["outSR"].Value == null) && (Request.Query["Address"].Value == null) && (Request.Query["City"].Value == null) && (Request.Query["State"].Value == null) && (Request.Query["Zip"].Value == null) && (Request.Query["outFields"].Value == null) && !model.Format.Equals("json"))
                {
                    model.Exception = "";
                    return View["FindAddressCandidates", model];
                }


                var addressCandidates = new GISServer.Core.Client.GeocodeService.AddressCandidate();

                if (!model.HasException)
                {
                    try
                    {
                        string url = "http://www.mapquestapi.com/geocoding/v1/address?key=" + ConfigurationManager.AppSettings["MapQuestKey"].ToString() + "&inFormat=kvp&outFormat=json&location=" + model.Address + "," + model.City + "," + model.State + "," + model.Zip;
                        
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);

                        //myReq.Credentials=new 
                        var response = myReq.GetResponse();
                        var receiveStream = response.GetResponseStream();
                        // Pipes the stream to a higher level stream reader with the required encoding format. 
                        var readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8);
                        var result = readStream.ReadToEnd();

                        var ser = GISServer.Core.Client.GeocodeService.Mapquest.Util.Parse(result);

                        addressCandidates.Location = new GISServer.Core.Client.Geometry.Point(ser.results[0].locations[0].latLng.lng, ser.results[0].locations[0].latLng.lat, 4326);

                        addressCandidates.Address = ser.results[0].locations[0].adminArea1 + ", " + ser.results[0].locations[0].adminArea3 + ", " + ser.results[0].locations[0].adminArea4 + ", " + ser.results[0].locations[0].adminArea5;

                    }
                    catch (Exception)
                    {
                        if (!model.HasException)
                        {
                            model.HasException = true;
                        }
                        exception.Error.Details.Add("Unable to perform find Address Candidates operation");
                    }
                }


                


                if (!model.HasException)
                {
                   model.Result = addressCandidates.toJson();
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
                    return View["FindAddressCandidates", model];
                }
            };
        }
    }
}