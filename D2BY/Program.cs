using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace D2BY
{
    class Program
    {
        static String BaseUri = "http://dota2bestyolo.com/";
        static void Main(string[] args)
        {
            CheckApi();
            //PerformRequest();
            //Console.WriteLine(GetChromeCookiePath());
        }

        private static async void CheckApi()
        {
            Dota2BestYoloClient Client = new Dota2BestYoloClient("tkz=0dcbdb65200eb858c11a8199cb5c57cc; token=ce2e1fefdabfef4c071f00817e6008a1; id=76561198172506530; __utma=74529031.759872907.1435548895.1437996670.1438003272.37; __utmz=74529031.1435548895.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); D2B_SESSION=g43a7l2ktmvutg1qm69t4ob771; __cfduid=d04770f623ebebf5d2c8679175ebbe0df1435641307; _ga=GA1.2.1905627191.1435641318; PHPSESSID=vglkac7pk3rbehve1pg91n1hf2");
            var response = await Client.SwitchTeamAsync("6409337", "8109");
            Console.WriteLine(response.Message);
        }

        private static void PerformRequest()
        {
            //var values = new Dictionary<String, String>();
            //values.Add("localCache", "false");
            //values.Add("cacheTTL", "5");
            //values.Add("cacheKey", "d2b_userStats");
            //values.Add("isCacheValid", "function(){return true;}");
            //var content = new FormUrlEncodedContent(values);

            var values = new Dictionary<String, String>();
            String id = "6409337";
            String match = "8109";
            values.Add("q", id);
            values.Add("m", match);
            var content = new FormUrlEncodedContent(values);
            PostAsync("my/switch-bet", CreateCookie(), content);
        }

        private static System.Net.CookieContainer CreateCookie()
        {
            var BaseAddress = new Uri(BaseUri);
            var cookieContainer = new System.Net.CookieContainer();
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("tkz", "0dcbdb65200eb858c11a8199cb5c57cc"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("token", "ce2e1fefdabfef4c071f00817e6008a1"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("id", "76561198172506530"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("__utma", "74529031.759872907.1435548895.1437996670.1438003272.37"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("__utmz", "74529031.1435548895.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("D2B_SESSION", "g43a7l2ktmvutg1qm69t4ob771"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("__cfduid", "d04770f623ebebf5d2c8679175ebbe0df1435641307"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("_ga", "GA1.2.1905627191.1435641318"));
            cookieContainer.Add(BaseAddress, new System.Net.Cookie("PHPSESSID", "vglkac7pk3rbehve1pg91n1hf2"));
            return cookieContainer;
        }

        public static async void PostAsync(String action, System.Net.CookieContainer cookieContainer, FormUrlEncodedContent content)
        {
            //var handler = new HttpClientHandler { CookieContainer = cookieContainer };
            var handler = new HttpClientHandler { UseCookies = false };
            using (var client = new HttpClient(handler))
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseUri + action));
                message.Content = content;
                message.Headers.Date = DateTime.Now.AddMinutes(-15);
                message.Headers.Add("Cookie", "tkz=0dcbdb65200eb858c11a8199cb5c57cc; token=ce2e1fefdabfef4c071f00817e6008a1; id=76561198172506530; __utma=74529031.759872907.1435548895.1437996670.1438003272.37; __utmz=74529031.1435548895.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); D2B_SESSION=g43a7l2ktmvutg1qm69t4ob771; __cfduid=d04770f623ebebf5d2c8679175ebbe0df1435641307; _ga=GA1.2.1905627191.1435641318; PHPSESSID=vglkac7pk3rbehve1pg91n1hf2");
                var response = client.SendAsync(message).Result;
                
                
                //var response = client.PostAsync(BaseUri + action, content).Result;
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
        }
    }
}
