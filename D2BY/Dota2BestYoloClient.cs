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

        public string BrowserCookie { get; set; }

        public Dota2BestYoloClient(string Cookie)
        {
            BrowserCookie = Cookie;
            Manager = new RequestManager(BrowserCookie);
        }

        public async Task<ResponseObject> SwitchTeamAsync(string id, string match)
        {
            var requestBody = RequestBodyFactory.GetRequestBody(RequestBody.SwitchTeam, id, match);
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/switch-bet/", requestBody));
        }

        public async Task<ResponseObject> SetClaimQueueAsync(string betID, string t, string code)
        {
            var requestBody = RequestBodyFactory.GetRequestBody(RequestBody.SetClaimQueue, betID, t, code);
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/claim-queue", requestBody));
        }

        public async Task<ResponseObject> SetOweQueueAsync()
        {
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/owe-queue", RequestBodyFactory.EmptyRequestBody));
        }

        public async Task<ResponseObject> SetStashQueueAsync(string mode)
        {
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/stash-queue/mode/" + mode, RequestBodyFactory.EmptyRequestBody));
        }

        public async Task<ResponseObject> SetOfferStashQueueAsync(List<string> items, List<string> originalIds, string mode)
        {
            var requestBody = RequestBodyFactory.GetRequestBody(RequestBody.SetOfferStashQueue, items, originalIds);
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/stash-add/mode/" + mode, requestBody));
        }

        public async Task<ResponseObject> CancelBetDataAsync(string id, string match)
        {
            var requestBody = RequestBodyFactory.GetRequestBody(RequestBody.CancelBetData, id, match);
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/cancel-bet", requestBody));
        }

        public async Task<ResponseObject> ConfirmAsSolvedAsync(string itemId)
        {
            var requestBody = RequestBodyFactory.GetRequestBody(RequestBody.ConfirmAsSolved, itemId);
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("my/missing-item-solved", requestBody));
        }

        public async Task<ResponseObject> GetQueueStatusAsync(string botId)
        {
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("match/get-queue-status/qid/" + botId, RequestBodyFactory.EmptyRequestBody, HttpMethod.Get));
        }

        public async Task<ResponseObject> GetMatchDetailsAsync(string matchId)
        {
            return JsonConvert.DeserializeObject<ResponseObject>(await Manager.PerformRequestAsync("match/index-right/id/" + matchId, RequestBodyFactory.EmptyRequestBody, HttpMethod.Get));
        }

        public async Task<UserStats> GetUserStatsAsync()
        {
            return JsonConvert.DeserializeObject<UserStats>(await Manager.PerformRequestAsync("my/get-user-stats", RequestBodyFactory.EmptyRequestBody, HttpMethod.Get));
        }

        public async Task<ResponseObject> GetBetHistoryAsync()
        {
            var htmlBets = await Manager.PerformRequestAsync("my/get-bet-history", RequestBodyFactory.EmptyRequestBody, HttpMethod.Get);
            HTMLParser.ParseBetsFromResponse(htmlBets);
            return null;
        }
    }
}
