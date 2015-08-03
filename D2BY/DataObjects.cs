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


    public class UserStats
    {
        [JsonProperty("total_bet")]
        public int TotalBet { get; set; }

        [JsonProperty("total_won")]
        public int TotalWon { get; set; }

        [JsonProperty("thismonth_profit")]
        public int ThisMonthProfit { get; set; }

        [JsonProperty("yesterday_profit")]
        public int YesterdayProfit { get; set; }

        [JsonProperty("thisweek_profit")]
        public int ThisWeekProfit { get; set; }

        [JsonProperty("lastweek_profit")]
        public float LastWeekProfit { get; set; }

        [JsonProperty("total_profit")]
        public float TotalProfit { get; set; }

        [JsonProperty("topbet_position")]
        public int TopBetPosition { get; set; }
    }


    public enum BetType
    {
        Game1,
        WinLose
    }

    public enum BetResult
    {
        Win,
        Lose,
        Closed
    }


    public class Bet
    {
        public string Tournament { get; set; }

        public string PlacedDate { get; set; }

        public string Team1Name { get; set; }

        public string Team2Name { get; set; }

        public BetResult Result { get; set; }

        public string PlacedOn { get; set; }

        public int Team1Score { get; set; }

        public int Team2Score { get; set; }

        public BetType Type { get; set; }

        public string BetId { get; set; }
    }


}
