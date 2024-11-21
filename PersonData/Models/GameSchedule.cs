using System;

public class GameSchedule
{
    public int GameId { get; set; } 
    public DateTime GameDate { get; set; }
    public string GameLocation { get; set; }
    public string TeamName { get; set; }
    public int? TeamScore { get; set; }
    public int? TeamTimeOfPossession { get; set; }
    public string OpponentName { get; set; }
    public int? OpponentScore { get; set; }
    public int? OpponentTimeOfPossession { get; set; }
    public string Winner { get; set; }

    public override string ToString()
    {
        return $"Game ID: {GameId}, Date: {GameDate:MM/dd/yyyy}, Location: {GameLocation}, Team: {TeamName}, " +
               $"Opponent: {OpponentName}, Score: {TeamScore}-{OpponentScore}, Winner: {Winner}, " +
               $"Team TOP: {TeamTimeOfPossession}s, Opponent TOP: {OpponentTimeOfPossession}s";
    }
}
