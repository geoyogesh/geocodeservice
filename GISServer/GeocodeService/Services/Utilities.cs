using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Nancy;

namespace GeocodeService.Services
{
    public static class Utilities
    {
        public static Nancy.Response GetJsonResponseObject(string resultstring)
        {
            var jsonBytes = Encoding.UTF8.GetBytes(resultstring);
            return new Response
            {
                ContentType = "text/html",
                StatusCode = HttpStatusCode.OK,
                Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
            };
            // application/json
        }
    }
}