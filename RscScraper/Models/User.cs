namespace RscScraper.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public int? GamesPlayed { get; set; }
        public int? GamesWon { get; set; }
        public int? GamesLost { get; set; }
        public string WinPercent { get; set; }
        public int? MVPs { get; set; }
        public int? Points { get; set; }
        public int? Goals { get; set; }
        public int? Assists { get; set; }
        public int? Saves { get; set; }
        public int? Shots { get; set; }
        public string ShotPercentage { get; set; }
        public int? PointsPerGame { get; set; }
        public int? GoalsPerGame { get; set; }
        public int? AssistsPerGame { get; set; }
        public int? SavesPerGame { get; set; }
        public int? ShotsPerGame { get; set; }
        public int? Cycles { get; set; }
        public int? HatTricks { get; set; }
        public int? PlayMakers { get; set; }
        public int? Saviors { get; set; }
    }
}
