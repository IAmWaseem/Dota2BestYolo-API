using Newtonsoft.Json;

namespace D2BY
{
    public class ResponseObject
    {
        [JsonProperty("result")]
        public int Result { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
