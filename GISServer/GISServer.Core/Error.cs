using System.Collections.Generic;

namespace GISServer.Core
{
    public class Error
    {
        public Error()
        {
            Details = new List<string>();
        }
        public int Code { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public List<string> Details { get; set; }
    }
}
