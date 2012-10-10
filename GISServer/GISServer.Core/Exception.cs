using GISServer.Core.Client.Utilities;

namespace GISServer.Core
{
    public class Exception
    {
        public Error Error { get; set; }

        public string ToJson()
        {
            return Serializer.ToJson(this);
        }
    }
}
