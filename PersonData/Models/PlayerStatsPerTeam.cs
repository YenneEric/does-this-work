using System;

namespace PersonData.Models
{
    public class GamePlayerStats
    {
        public int GameId { get; }
        public int PlayerId { get; } 
        public string PlayerName { get; }
        public string Position { get; }
        public int? RushingYards { get; }
        public int? ReceivingYards { get; }
        public int? ThrowingYards { get; }
        public int? Tackles { get; }
        public int? Sacks { get; }
        public int? Turnovers { get; }
        public int? InterceptionsCaught { get; }
        public int? Touchdowns { get; }
        public int? Punts { get; }
        public int? FieldGoalsMade { get; }

        internal GamePlayerStats(
            int gameId,
            int playerId,
            string playerName,
            string position,
            int? rushingYards,
            int? receivingYards,
            int? throwingYards,
            int? tackles,
            int? sacks,
            int? turnovers,
            int? interceptionsCaught,
            int? touchdowns,
            int? punts,
            int? fieldGoalsMade)
        {
            GameId = gameId;
            PlayerId = playerId;
            PlayerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            RushingYards = rushingYards;
            ReceivingYards = receivingYards;
            ThrowingYards = throwingYards;
            Tackles = tackles;
            Sacks = sacks;
            Turnovers = turnovers;
            InterceptionsCaught = interceptionsCaught;
            Touchdowns = touchdowns;
            Punts = punts;
            FieldGoalsMade = fieldGoalsMade;
        }
    }
}
