using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D2BY
{
    public class HTMLParser
    {
        public static List<Bet> ParseBetsFromResponse(string response)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(response);
            foreach (var node in document.DocumentNode.ChildNodes)
            {
                if (node.Name == "div")
                {
                    Bet bet = new Bet();
                    foreach (var innerNode in node.ChildNodes)
                    {
                        // Get Win / Lose / Closed Status
                        if (innerNode.Attributes.AttributesWithName("class").FirstOrDefault()?.Value == "result-t")
                        {
                            if (innerNode.ChildNodes.First().InnerText == "win")
                            {
                                bet.Result = BetResult.Win;
                            }
                            else if(innerNode.ChildNodes.First().InnerText == "lose")
                            {
                                bet.Result = BetResult.Lose;
                            }
                            else
                            {
                                bet.Result = BetResult.Closed;
                            }
                        }

                        // Get Tournament Name
                        else if(innerNode.Attributes.AttributesWithName("class").FirstOrDefault()?.Value == "series-t")
                        {
                            bet.Tournament = innerNode.ChildNodes.FirstOrDefault()?.InnerText;
                        }

                        // Get Time
                        else if(innerNode.Attributes.AttributesWithName("class").FirstOrDefault()?.Value == "time-t")
                        {
                            bet.PlacedDate = Regex.Replace(innerNode.ChildNodes.FirstOrDefault()?.InnerText, @"<[^>]+>|&nbsp;", " ");
                        }

                        // Get ID
                        else if(innerNode.Attributes.AttributesWithName("class").FirstOrDefault()?.Value == "hide-item-row")
                        {
                            bet.BetId = innerNode.Attributes.AttributesWithName("id").FirstOrDefault()?.Value;
                            if(bet.BetId.Split('-').First() == "VALUE_G1")
                            {
                                bet.Type = BetType.Game1;
                            }
                            else if(bet.BetId.Split('-').First() == "VALUE_WL")
                            {
                                bet.Type = BetType.WinLose;
                            }
                        }

                        // Get Team Details
                        else if(innerNode.Attributes.AttributesWithName("class").FirstOrDefault()?.Value == "opt-t")
                        {
                            int count = 0;
                            int spanCount = 0;
                            foreach(var innerInnerNode in innerNode.ChildNodes)
                            {
                                if (innerInnerNode.Name == "span")
                                {
                                    string teamName = "";
                                    if (innerInnerNode.InnerText.Contains("[&#10003;]"))
                                    {
                                        teamName = innerInnerNode.InnerText.Substring(0, innerInnerNode.InnerText.Length - 11);
                                        bet.PlacedOn = teamName;
                                    }
                                    if (spanCount == 0)
                                    {
                                        bet.Team1Name = teamName;
                                    }
                                    else
                                    {
                                        bet.Team2Name = teamName;
                                    }
                                    spanCount++;
                                }
                                if (count == 2)
                                {
                                    var score = innerInnerNode.InnerText.Trim();
                                    bet.Team1Score = Int32.Parse(score.Split(':')[0]);
                                    bet.Team2Score = Int32.Parse(score.Split(':')[1]);
                                }
                                count++;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
