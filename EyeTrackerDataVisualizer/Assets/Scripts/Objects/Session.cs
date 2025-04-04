﻿using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;


namespace Objects
{
    public class Session
    {
        [PrimaryKey,Identity]
        public int Id { get; }
        [NotNull]
        public int SessionNumber { get; set; }
        [NotNull]
        public int UserId { get; set; }
        
        [Nullable]
        public List<Game> GamesList { get; set; }

        public Session(int sessionId, int sessionNumber, int userId)
        {
            UserId = userId;
            SessionNumber = sessionNumber;
            Id = sessionId;
            GamesList = new List<Game>();
        }

        public Game GetGame(int timesPlayed, string gameName)
        {
            return GamesList.FirstOrDefault(game => game.Name.Equals(gameName));
        }
    }
}