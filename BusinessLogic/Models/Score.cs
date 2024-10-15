﻿namespace BusinessLogic.Models
{
    public class Score
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Points { get; set; }
        public int Pairs { get; set; }

        public Score(string playerName, int points, int pairs) { 
            PlayerName = playerName;
            Points = points;
            Pairs = pairs;
        }
    }
}
