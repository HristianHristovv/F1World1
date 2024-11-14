namespace F1_World.Models
{
    public class Season
    {
        public int SeasonId { get; set; }
        public int Year { get; set; }

        public int ChampionPilotId { get; set; }
        public Pilot? ChampionPilot { get; set; } // Navigation property for Pilot

        public int ChampionTeamId { get; set; }
        public Team? ChampionTeam { get; set; }
    }
}
