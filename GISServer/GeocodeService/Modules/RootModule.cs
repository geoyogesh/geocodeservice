using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Nancy;

namespace GeocodeService.Modules
{
    public class RootModule : Nancy.NancyModule
    {
        public RootModule()
            : base("/")
        {
            Get["/"] = parameters =>
            {
                return Response.AsRedirect("/rest/services");
            };
            Get["/clientaccesspolicy.xml"] = parameters =>
            {
                string policystring = "<?xml version='1.0' encoding='utf-8'?><access-policy><cross-domain-access><policy><allow-from http-request-headers='SOAPAction'><domain uri='*'/></allow-from><grant-to><resource path='/' include-subpaths='true'/></grant-to></policy></cross-domain-access></access-policy>";
                var XmlBytes = Encoding.UTF8.GetBytes(policystring);
                return new Response
                {
                    ContentType = "text/xml",
                    StatusCode = HttpStatusCode.OK,
                    Contents = s => s.Write(XmlBytes, 0, XmlBytes.Length)
                };
            };

            Get["/crossdomain.xml"] = parameters =>
            {
                string policystring = "<?xml version='1.0'?><!DOCTYPE cross-domain-policy SYSTEM 'http://www.macromedia.com/xml/dtds/cross-domain-policy.dtd'><cross-domain-policy><allow-http-request-headers-from domain='*' headers='SOAPAction,Content-Type'/></cross-domain-policy>";
                var XmlBytes = Encoding.UTF8.GetBytes(policystring);
                return new Response
                {
                    ContentType = "text/xml",
                    StatusCode = HttpStatusCode.OK,
                    Contents = s => s.Write(XmlBytes, 0, XmlBytes.Length)
                };
            };

            Get["/rest"] = parameters =>
            {
                return Response.AsRedirect("/rest/services");
            };
        }
    }
}