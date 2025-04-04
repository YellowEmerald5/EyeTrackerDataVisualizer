﻿using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Objects
{
    public class ObjectInGame
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Column(Length = 100)]
        public string Name { get; set; }
        [NotNull]
        public int GameId { get; set; }
        [NotNull]
        public Aoi Aoi { get; set; }
        [Nullable]
        public List<Point> Points { get; set; }
        [NotNull]
        public long TimeSpawn { get; set; }
        [NotNull]
        public float SpawnPositionX { get; set; }
        [NotNull]
        public float SpawnPositionY { get; set; }
        [NotNull]
        public float SpawnPositionZ { get; set; }
        [NotNull]
        public long TimeDestroyed { get; set; }
        [NotNull]
        public float EndPositionX { get; set; }
        [NotNull]
        public float EndPositionY { get; set; }
        [NotNull]
        public float EndPositionZ { get; set; }

        public ObjectInGame(string name, Aoi aoi, int gameId, long timeSpawn, float spawnPositionX, float spawnPositionY, float spawnPositionZ)
        {
            GameId = gameId;
            Name = name;
            Aoi = aoi;
            Points = new List<Point>();
            TimeSpawn = timeSpawn;
            SpawnPositionX = spawnPositionX;
            SpawnPositionY = spawnPositionY;
            SpawnPositionZ = spawnPositionZ;
        }
        
        public ObjectInGame(int id, string name, int gameId, long timeSpawn, float spawnPositionX, float spawnPositionY, float spawnPositionZ, long timeDestroyed, float endPositionX, float endPositionY, float endPositionZ)
        {
            Id = id;
            GameId = gameId;
            Name = name;
            //Aoi = aoi;
            //Points = points;
            TimeSpawn = timeSpawn;
            SpawnPositionX = spawnPositionX;
            SpawnPositionY = spawnPositionY;
            SpawnPositionZ = spawnPositionZ;
            TimeDestroyed = timeDestroyed;
            EndPositionX = endPositionX;
            EndPositionY = endPositionY;
            EndPositionZ = endPositionZ;
        }
    }
}