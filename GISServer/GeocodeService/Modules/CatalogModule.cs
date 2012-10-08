using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeocodeService.Modules
{
    public class CatalogModule:Nancy.NancyModule
    {
        public CatalogModule()
        {
            Get["/"] = _ =>
                {
                    return "Hello world";
                };
        }
    }
}