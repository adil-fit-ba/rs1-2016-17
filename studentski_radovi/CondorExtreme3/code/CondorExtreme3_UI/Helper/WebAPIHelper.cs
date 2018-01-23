using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CondorExtreme3_UI.Helper
{
    public class WebAPIHelper
    {
        public HttpClient Client { get; set; }

        public string Route { get; set; }

      
        public WebAPIHelper(string Uri, string Route)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(Uri);
            this.Route = Route;          
        }

        public HttpResponseMessage GetResponse()
        {
            return Client.GetAsync(Route).Result;
        }

        public HttpResponseMessage PostResponse(object obj)
        { 
            return Client.PostAsJsonAsync(Route, obj).Result;
        }

        public HttpResponseMessage GetResponse(string parameter)
        {
            return Client.GetAsync(Route+"/"+ parameter).Result;
        }

    }
}
