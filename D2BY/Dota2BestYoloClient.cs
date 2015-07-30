using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace D2BY
{
    public class Dota2BestYoloClient
    {
        private RequestManager Manager;

        public String BrowserCookie { get; set; }

        public Dota2BestYoloClient(String Cookie)
        {
            BrowserCookie = Cookie;
            Manager = new RequestManager(BrowserCookie);
        }

        public async Task<ResponseObject> SwitchTeamAsync(String id, String match)
        {
            var requestBody = GetRequestBody_SwitchTeam(id, match);
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/switch-bet/", requestBody));
        }

        private FormUrlEncodedContent GetRequestBody_SwitchTeam(String id, String match)
        {
            var values = new Dictionary<String, String>();
            values.Add("q", id);
            values.Add("m", match);
            return new FormUrlEncodedContent(values);
        }
    }
}
