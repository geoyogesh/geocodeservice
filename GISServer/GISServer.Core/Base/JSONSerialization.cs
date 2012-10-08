using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISServer.Core.Base
{
    public class JSONSerialization
    {
        public string toJSON()
        {
            return GISServer.Core.Client.Utilities.Serializer.ToJson(this);
        }
    }
}
