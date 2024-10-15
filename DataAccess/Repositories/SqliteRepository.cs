using BusinessLogic;
using BusinessLogic.Models;
using DataAccess.Connectors;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
    public class SqliteRepository : IRepository
    {
        private static readonly SQLite _sqlite = new SQLite("/home/david/Projecten/Aftekenopdracht Memory/DataAccess/db.db");

        public bool AddHighscore(Score score)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"PlayerName", score.PlayerName},
                {"Points", score.Points},
                {"Pairs", score.Pairs }
            };
            int affected = _sqlite.ExecuteNonQuery("INSERT INTO highscores (PlayerName, Points, Pairs) VALUES (@PlayerName, @Points, @Pairs)", parameters);
            return affected == 1;
        }

        public List<Card> GetCards()
        {
            List<Dictionary<string, object>> results = _sqlite.ExecuteQuery("SELECT * FROM cards;");

            List<Card> cards = results.Select(row => new Card
            {
                ImageUrl = row["ImageUrl"]?.ToString(),
                ConsoleLetter = row["ConsoleLetter"]?.ToString()?[0]
            }).ToList();

            return cards;
        }

        public List<Score> GetHighscores()
        {
            List<Dictionary<string, object>> results = _sqlite.ExecuteQuery("SELECT * FROM highscores;");

            List<Score> highscores = results.Select(row => new Score(row["PlayerName"]?.ToString() ?? "Unknown player", int.Parse(row["Points"]?.ToString() ?? "0"), int.Parse(row["Pairs"]?.ToString() ?? "0"))).ToList();

            return highscores;
        }

        public bool RemoveHighscore(Score score)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Id", score.Id},
            };
            int affected = _sqlite.ExecuteNonQuery("DELETE FROM highscores where Id = @Id;", parameters);
            return affected == 1;
        }
    }
}
