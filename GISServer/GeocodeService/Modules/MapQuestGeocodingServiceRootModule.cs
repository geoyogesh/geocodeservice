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
                          Name = "street",
                          Type = "esriFieldTypeString",
                          Alias = "street",
                          Required = false
                      });
                      root.AddressFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea5",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea5",
                          Required = false
                      });
                      root.AddressFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea4",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea4",
                          Required = false
                      });
                      root.AddressFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea3",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea3",
                          Required = false
                      });
                      root.AddressFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "adminArea1",
                          Type = "esriFieldTypeString",
                          Alias = "adminArea1",
                          Required = false
                      });
                      root.AddressFields.Add(new GISServer.Core.Client.GeocodeService.AddressField
                      {
                          Name = "postalCode",
                          Type = "esriFieldTypeString",
                          Alias = "postalCode",
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