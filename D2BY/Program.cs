using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;

namespace D2BY
{
    class Program
    {
        public static readonly string Cookie = "tkz=0dcbdb65200eb858c11a8199cb5c57cc; token=ce2e1fefdabfef4c071f00817e6008a1; id=76561198172506530; __utma=74529031.759872907.1435548895.1437996670.1438003272.37; __utmz=74529031.1435548895.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); D2B_SESSION=g43a7l2ktmvutg1qm69t4ob771; __cfduid=d04770f623ebebf5d2c8679175ebbe0df1435641307; _ga=GA1.2.1905627191.1435641318; PHPSESSID=vglkac7pk3rbehve1pg91n1hf2";
        static void Main(string[] args)
        {
            CheckApi();
        }

        private static async void CheckApi()
        {
            Dota2BestYoloClient Client = new Dota2BestYoloClient(Cookie);

            //var response = await Client.SwitchTeamAsync("6409337", "8109");
            //Console.WriteLine(response.Message);

            //await Client.GetMatchDetails("8135");

            //var response = await Client.SetClaimQueueAsync("76561198140394557", "2", "");

            //await Client.GetUserStatsAsync();

            await Client.GetBetHistoryAsync();

            //Console.WriteLine(response.Message);
        }

    }
}
