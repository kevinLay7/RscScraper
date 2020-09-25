using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using RscScraper.Models;

namespace RscScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rsc : ControllerBase
    {
        private const string CacheKey = "rsc";
        private IMemoryCache _cache;
        public Rsc(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if(!_cache.TryGetValue(CacheKey, out ResponseOutput output))
            {
                var uri = new Uri("https://www.rocketsoccarconfederation.com/na/s9-stats/s9-player-stats");

                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(uri);
                var contentHolder = htmlDoc.DocumentNode.SelectSingleNode("//article").ChildNodes.First(x => x.HasClass("entry-content"));
                var titles = contentHolder.ChildNodes.Where(x => x.Name == "h2").Select(x => x.InnerHtml).ToArray();
                var containerDivs = contentHolder.ChildNodes.Where(x => x.Name == "div").ToArray();

                output = new ResponseOutput();
                for (int i = 0; i < containerDivs.Length; i++)
                {
                    var tier = new Tier();
                    tier.Name = titles[i];
                    output.Tiers.Add(tier);

                    var table = containerDivs[i].ChildNodes.First(x => x.Name == "table");
                    var tbody = table.ChildNodes.First(x => x.Name == "tbody");
                    var rows = tbody.ChildNodes.Where(x => x.Name == "tr").ToList();
                    foreach (var row in rows)
                    {
                        var statColumns = row.ChildNodes.Where(x => x.Name == "td").ToArray();
                        var user = MapRowToUser(statColumns);
                        tier.Players.Add(user);
                    }
                }

                output.LastUpdate = DateTime.Now;

                var entryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(15));
                _cache.Set(CacheKey, output, entryOptions);
            }

            return Ok(output);
        }

        private User MapRowToUser(HtmlNode[] columns)
        {
            var c = columns;

            var output = new User()
            {
                Name = c[0].InnerText,
                Team = c[1].InnerText,
                GamesPlayed = MapStringToInt(c[2].InnerText),
                GamesWon = MapStringToInt(c[3].InnerText),
                GamesLost = MapStringToInt(c[4].InnerText),
                WinPercent = c[5].InnerText,
                MVPs = MapStringToInt(c[6].InnerText),
                Points = MapStringToInt(c[7].InnerText),
                Goals = MapStringToInt(c[8].InnerText),
                Assists = MapStringToInt(c[9].InnerText),
                Saves = MapStringToInt(c[10].InnerText),
                Shots = MapStringToInt(c[11].InnerText),
                ShotPercentage = c[12].InnerText,
                PointsPerGame = MapStringToInt(c[13].InnerText),
                GoalsPerGame = MapStringToInt(c[14].InnerText),
                AssistsPerGame = MapStringToInt(c[15].InnerText),
                SavesPerGame = MapStringToInt(c[16].InnerText),
                ShotsPerGame = MapStringToInt(c[17].InnerText),
                Cycles = MapStringToInt(c[18].InnerText),
                HatTricks =  MapStringToInt(c[19].InnerText),
                PlayMakers = MapStringToInt(c[20].InnerText),
                Saviors = MapStringToInt(c[21].InnerText),
            };

            return output;
        }

        private int? MapStringToInt(string input)
        {
            int.TryParse(input, out int result);
            return result;
        }
    }
}
