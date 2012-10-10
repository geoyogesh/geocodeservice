using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeocodeService.Modules
{
    public class MapQuestFindAddressCandidatesModule : Nancy.NancyModule
    {
        public MapQuestFindAddressCandidatesModule()
            : base("rest/services/MapQuest/GeocodeServer/")
        {
            Get["/findAddressCandidates"] = _ =>
            {
                return "findAddressCandidates";
            };
        }
    }
}