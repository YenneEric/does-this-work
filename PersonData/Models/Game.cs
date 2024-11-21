using System;

namespace PersonData.Models
{
    public class Game
    {
        public int GameId { get; }
        public string Location { get; }
        public bool Canceled { get; }

        

        public Game(int gameId, string location, bool canceled)
        {
            GameId = gameId;

            Location = location;
            Canceled = canceled;
        }
    }
}
