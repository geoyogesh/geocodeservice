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


                var addressCandidates = new GISServer.Core.Client.GeocodeService.Addresses();

                if (!model.HasException)
                {
                    string url = null;
                        try
                        {
                            if ((Request.Query["SingleLine"].Value == null))
                            {
                                url = "http://www.mapquestapi.com/geocoding/v1/address?key=" + ConfigurationManager.AppSettings["MapQuestKey"].ToString() + "&inFormat=kvp&outFormat=json&location=" + model.Address + "," + model.City + "," + model.State + "," + model.Zip;
                            }
                            else
                            {
                                url = "http://www.mapquestapi.com/geocoding/v1/address?key=" + ConfigurationManager.AppSettings["MapQuestKey"].ToString() + "&inFormat=kvp&outFormat=json&location=" + (string)Request.Query["SingleLine"].Value;
                            }
                            var result = HttpReq(url);

                            var ser = GISServer.Core.Client.GeocodeService.Mapquest.Util.Parse(result);

                            addressCandidates.SpatialReference = new GISServer.Core.Client.Geometry.SpatialReference(model.SpatialReference);
                            addressCandidates.Candidates = new List<GISServer.Core.Client.GeocodeService.AddressCandidate>();

                            var projpoint = new List<GISServer.Core.Client.Geometry.Point>();
                            var s = new GISServer.Core.Client.Geometry.GeometryArray();
                            s.GeometryType = "esriGeometryPoint";

                            var gp =new List<GISServer.Core.Client.Geometry.Point>();
                            
                            foreach (var item in ser.results[0].locations)
                            {
                                gp.Add(new GISServer.Core.Client.Geometry.Point(item.displayLatLng.lng,item.displayLatLng.lat));
                            }
                            s.Geometries=gp;

                            var gpurl = String.Format("{0}/project?inSR=4326&outSR={1}&geometries={2}&f=pjson", ConfigurationManager.AppSettings["GeocodeServer"], model.SpatialReference, s.ToJson());


                            var res = HttpReq(gpurl);

                            var geomarray= GISServer.Core.Client.Utilities.Parser.GetGeometies(res);

                            for (int i = 0; i < geomarray.Geometries.Count; i++)
                            {
                                var ac = new GISServer.Core.Client.GeocodeService.AddressCandidate();

                                ac.Location = new GISServer.Core.Client.Geometry.Point(geomarray.Geometries[i].X, geomarray.Geometries[i].Y);

                                ac.Score = 100;
                                ac.Attributes = new GISServer.Core.Attributes("street", ser.results[0].locations[i].street, "adminArea5", ser.results[0].locations[i].adminArea5, "adminArea4", ser.results[0].locations[i].adminArea4, "adminArea3", ser.results[0].locations[i].adminArea3, "adminArea1", ser.results[0].locations[i].adminArea1, "postalCode", ser.results[0].locations[i].postalCode);
                                addressCandidates.Candidates.Add(ac);
                            }

                            

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

        private static string HttpReq(string url)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);

            //myReq.Credentials=new 
            var response = myReq.GetResponse();
            var receiveStream = response.GetResponseStream();
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            var readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8);
            var result = readStream.ReadToEnd();
            return result;
        }
    }
}