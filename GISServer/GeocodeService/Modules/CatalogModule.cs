using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeocodeService.Modules
{
    public class CatalogModule:Nancy.NancyModule
    {
        public CatalogModule()
            : base("/rest/services/")
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
                    var catalog = new GISServer.Core.Catalog.Catalog
                    {
                        CurrentVersion = 1.0,
                        SpecVersion = 1.0,
                    };
                    catalog.Services.Add(new GISServer.Core.Catalog.Service("MapQuest", "GeocodeServer"));
                    var resultstring = catalog.toJson();
                    return Services.Utilities.GetJsonResponseObject(resultstring);
                }
                else
                {
                    return View["Catalog"];
                }
            };

        }
    }
}