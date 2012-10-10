using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeocodeService.Modules
{
    public class MapQuestGeocodingServiceRootModule : Nancy.NancyModule
    {
        public MapQuestGeocodingServiceRootModule()
            : base("rest/services/MapQuest/GeocodeServer/")
        {
            Get["/"] = parameters =>
              {
                  string format = null;

                  #region Optional Parameter
                  if ((Request.Query["f"].Value != null))
                  {
                      format = Request.Query["f"].Value;
                  }
                  else
                  {
                      format = "HTML";
                  }
                  #endregion


                  if (format.Equals("json"))
                  {
                      var root = new GISServer.Core.Client.GeocodeService.Root();
                      root.ServiceDescription = "This is geocode service service";

                      root.AddressFields = new List<GISServer.Core.Client.GeocodeService.AddressField>();
                      root.AddressFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "Address",
                          Type = "esriFieldTypeString",
                          Alias = "Address",
                          Required = false
                      });


                      root.CandidateFields = new List<GISServer.Core.Client.GeocodeService.AddressField>();
                      root.CandidateFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea4",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea4",
                          Required = false
                      });
                      root.CandidateFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea5Type",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea5Type",
                          Required = false
                      });
                      root.CandidateFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea4Type",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea4Type",
                          Required = false
                      });
                      root.CandidateFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea5",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea5",
                          Required = false
                      });



                      root.IntersectionCandidateFields = new List<GISServer.Core.Client.GeocodeService.AddressField>();
                      root.IntersectionCandidateFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "Loc_name",
                          Type = "esriFieldTypeString",
                          Alias = "Loc_name",
                          Required = false
                      });


                      root.LocatorProperties = new GISServer.Core.Attributes("MinimumCandidateScore" , "10", "SideOffsetUnits" , "ReferenceDataUnits",  "UICLSID" , "{3D486637-6BCF-4A0C-83DB-A02D437FB8FC}",  "SpellingSensitivity" , "80", "MinimumMatchScore" , "60", "EndOffset" , "3", "IntersectionConnectors" , "& | @", "MatchIfScoresTie" , "True", "SideOffset" , "0", "SuggestedBatchSize" , "10", "WriteXYCoordFields" , "TRUE", "WriteStandardizedAddressField" , "FALSE", "WriteReferenceIDField" , "FALSE", "WritePercentAlongField" , "FALSE");
                      root.SpatialReference = new GISServer.Core.Client.Geometry.SpatialReference(4326);



                      var resultstring = root.toJson();
                      return Services.Utilities.GetJsonResponseObject(resultstring);
                  }
                  else
                  {
                      return View["GeocodeService"];
                  }
              };
        }
    }
}