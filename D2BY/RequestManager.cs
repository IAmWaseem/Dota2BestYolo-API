using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace D2BY
{
    public class RequestManager
    {
        private String BaseURL = "http://dota2bestyolo.com/";
        private Uri BaseURI = new Uri("http://dota2bestyolo.com/");

        public String Cookie;

        public RequestManager(String cookie)
        {
            Cookie = cookie;
        }

        public async Task<String> PerformRequestAsync(String action, FormUrlEncodedContent requestBody)
        {
            return await RequestAsync(action, requestBody, HttpMethod.Post);
        }

        private async Task<String> RequestAsync(String action, FormUrlEncodedContent requestBody, HttpMethod method)
        {
            var handler = new HttpClientHandler() { UseCookies = false };
            using(var Client = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage(method, new Uri(BaseURL + action));
                request.Content = requestBody;
                //request.Headers.Add("");
                request.Headers.Add("Cookie", Cookie);
                request.Headers.Date = DateTime.Now.AddYears(-5);
                var response = Client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
