using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
namespace D2BY
{
    public enum RequestBody
    {
        SwitchTeam,
        SetClaimQueue,
        SetOfferStashQueue,
        CancelBetData,
        ConfirmAsSolved
    }
    public class RequestBodyFactory
    {
        private static FormUrlEncodedContent _EmptyRequestBody;
        public static FormUrlEncodedContent EmptyRequestBody
        {
            get
            {
                if (_EmptyRequestBody == null)
                    _EmptyRequestBody = new FormUrlEncodedContent(new Dictionary<string, string>());
                return _EmptyRequestBody;
            }
        }
        public static FormUrlEncodedContent GetRequestBody(RequestBody type, params object[] parameters)
        {
            switch(type)
            {
                case RequestBody.SwitchTeam:
                    return GetSwitchTeamBody(parameters[0] as string, parameters[1] as string);
                case RequestBody.SetClaimQueue:
                    return GetSetClaimQueueBody(parameters[0] as string, parameters[1] as string, parameters[2] as string);
                case RequestBody.SetOfferStashQueue:
                    return GetSetOfferStashQueueBody(parameters[0] as List<string>, parameters[1] as List<string>);
                case RequestBody.CancelBetData:
                    return GetCancelBetData(parameters[0] as string, parameters[1] as string);
                case RequestBody.ConfirmAsSolved:
                    return GetConfirmAsSolvedBody(parameters[0] as string);
            }
            return null;
        }

        private static FormUrlEncodedContent GetSwitchTeamBody(string id, string match)
        {
            var values = new Dictionary<string, string>();
            values.Add("q", id);
            values.Add("m", match);
            return new FormUrlEncodedContent(values);
        }

        private static FormUrlEncodedContent GetSetClaimQueueBody(string betId, string t, string code)
        {
            var values = new Dictionary<string, string>();
            values.Add("bid", betId);
            values.Add("t", t);
            values.Add("code", code);
            return new FormUrlEncodedContent(values);
        }

        private static FormUrlEncodedContent GetSetOfferStashQueueBody(List<string> items, List<string> originalIds)
        {
            var values = new Dictionary<string, string>();
            values.Add("items", JsonConvert.SerializeObject(items));
            values.Add("original_ids", JsonConvert.SerializeObject(originalIds));
            return new FormUrlEncodedContent(values);
        }

        private static FormUrlEncodedContent GetCancelBetData(string id, string match)
        {
            var values = new Dictionary<string, string>();
            values.Add("q", id);
            values.Add("m", match);
            return new FormUrlEncodedContent(values);
        }

        private static FormUrlEncodedContent GetConfirmAsSolvedBody(string itemId)
        {
            var values = new Dictionary<string, string>();
            values.Add("i", itemId);
            return new FormUrlEncodedContent(values);
        }

    }
}
