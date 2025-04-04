using System.Collections.Generic;
using LinqToDB.Mapping;
using UnityEngine;

namespace Objects
{
    public class Aoi
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public int ObjectId { get; set; }
        [Nullable]
        public List<AoiOrigin> Origins { get; set; }
        [Nullable]
        public List<AoiSize> Sizes { get; set; }
        [NotNull]
        public long TimeSpawn { get; set; }
        [NotNull]
        public long TimeDestroy { get; set; }
        [NotNull]
        public float StartPositionX { get; set; }
        [NotNull]
        public float StartPositionY { get; set; }
        [NotNull]
        public float EndPositionX { get; set; }
        [NotNull]
        public float EndPositionY { get; set; }
        
        public Aoi(int objectId,long timeSpawn,Vector3 position)
        {
            ObjectId = objectId;
            TimeSpawn = timeSpawn;
            StartPositionX = position.x;
            StartPositionY = position.y;
            Origins = new List<AoiOrigin>();
            Sizes = new List<AoiSize>();
        }
        
        public Aoi(int id, int objectId,long timeSpawn,float startPositionX, float startPositionY, long timeDestroy, float endPositionX, float endPositionY)
        {
            Id = id;
            ObjectId = objectId;
            TimeSpawn = timeSpawn;
            StartPositionX = startPositionX;
            StartPositionY = startPositionY;
            TimeDestroy = timeDestroy;
            EndPositionX = endPositionX;
            EndPositionY = endPositionY;
        }
    }
}
