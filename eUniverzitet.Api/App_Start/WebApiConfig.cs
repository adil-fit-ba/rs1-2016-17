using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;

namespace eUniverzitet.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //dodaje GZIP kompresuju. Potrebno je instalirati "Microsoft.AspNet.WebApi.MessageHandlers.Compression" preko NuGET-a
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            //deaktivira XML, a aktivira JSON
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
