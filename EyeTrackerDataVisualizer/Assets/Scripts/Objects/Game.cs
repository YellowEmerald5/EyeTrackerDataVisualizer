using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Objects
{
    public class Game
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public int UserId { get; set; }
        [NotNull,Column(Length = 100)]
        public string Name { get; }
        
        [Nullable]
        public List<ObjectInGame> Objects { get; set; }
        [Nullable]
        public int AmountOfTimesPlayed { get; set; }
        [Nullable]
        public float WindowHeight { get; set; }
        [Nullable]
        public float WindowWidth { get; set; }
        
        public Game(int amountOfTimesPlayed,string name, int userId, float windowHeight, float windowWidth)
        {
            AmountOfTimesPlayed = amountOfTimesPlayed;
            UserId = userId;
            Name = name;
            Objects = new List<ObjectInGame>();
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
        }
        
        public Game(int id,int amountOfTimesPlayed,string name, int userId, float windowHeight, float windowWidth)
        {
            Id = id;
            AmountOfTimesPlayed = amountOfTimesPlayed;
            UserId = userId;
            Name = name;
            Objects = new List<ObjectInGame>();
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
        }
    }
}