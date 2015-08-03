using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace D2BY
{
    public class RequestManager
    {
        private string BaseURL = "http://dota2bestyolo.com/";
        private Uri BaseURI = new Uri("http://dota2bestyolo.com/");

        public string Cookie;

        public RequestManager(string cookie)
        {
            Cookie = cookie;
        }

        public async Task<string> PerformRequestAsync(string action, FormUrlEncodedContent requestBody)
        {
            return await RequestAsync(action, requestBody, HttpMethod.Post);
        }

        public async Task<string> PerformRequestAsync(string action, FormUrlEncodedContent requestBody, HttpMethod method)
        {
            return await RequestAsync(action, requestBody, method);
        }

        private async Task<string> RequestAsync(string action, FormUrlEncodedContent requestBody, HttpMethod method)
        {
            if (requestBody == RequestBodyFactory.EmptyRequestBody)
                requestBody = null;
            var handler = new HttpClientHandler() { UseCookies = false };

            using(var Client = new HttpClient(handler))
            {
                Client.Timeout = TimeSpan.FromMinutes(10);
                HttpRequestMessage request = new HttpRequestMessage(method, new Uri(BaseURL + action));
                request.Content = requestBody;
                request.Headers.Add("Cookie", Cookie);
                request.Headers.Date = DateTime.Now.AddYears(-5);
                var response = Client.SendAsync(request, HttpCompletionOption.ResponseContentRead).Result;
                
                response.EnsureSuccessStatusCode();

                var data = response.Content.ReadAsStringAsync();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
