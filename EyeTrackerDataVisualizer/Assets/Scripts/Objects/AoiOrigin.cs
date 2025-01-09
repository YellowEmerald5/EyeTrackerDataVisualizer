using LinqToDB.Mapping;
using UnityEngine;

namespace Objects
{
    public class AoiOrigin
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [NotNull, Column(Length = 100)]
        public string AoiId { get; set; }
        [NotNull]
        public float PosX { get; set; }
        [NotNull]
        public float PosY { get; set; }
        
        public AoiOrigin(string areaOfInterestId,Vector3 origin)
        {
            AoiId = areaOfInterestId;
            PosX = origin.x;
            PosY = origin.y;
        }

        public AoiOrigin(int id, string aoiId, float posX, float posY)
        {
            Id = id;
            AoiId = aoiId;
            PosX = posX;
            PosY = posY;
        }
    }
}