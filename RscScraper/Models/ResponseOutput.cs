using System;
using System.Collections.Generic;

namespace RscScraper.Models
{
    public class ResponseOutput
    {
        public ResponseOutput()
        {
            Tiers = new List<Tier>();
        }
        public DateTime? LastUpdate { get; set; }
        public IList<Tier> Tiers { get; set; }
    }
}
