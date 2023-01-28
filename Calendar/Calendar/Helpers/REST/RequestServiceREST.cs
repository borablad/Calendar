using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Calendar.Models;
using Calendar;
using Xamarin.Essentials;

namespace Calendar.Helpers.REST
{
    public class RequestServiceREST
    {

        public async Task<HttpResponseMessage> Post(string methodName, object requestParams, string mediaType = "application/json", bool IsImage = false)
        {
            var client = GetClient();

           
                //requestParams string

                return await client.PostAsync(string.Format(Constants.RestUrl, methodName), new StringContent((string)requestParams, Encoding.Default, mediaType));

            
        }
      
        public async Task<HttpResponseMessage> Get(string methodName)
        {
            var client = GetClient();
            return await client.GetAsync(string.Format(Constants.RestUrl, methodName));
        }


        private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var token = Preferences.Get("token", $"");
            var token_type = Preferences.Get("token_type", $"bearer");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"{token_type} {token}");
            }

            return client;
        }
    }
}
