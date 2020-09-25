using System.Collections.Generic;

namespace RscScraper.Models
{
    public class Tier
    {
        public Tier()
        {
            Players = new List<User>();
        }

        public string Name { get; set; }
        public IList<User> Players { get; set; }
    }
}
