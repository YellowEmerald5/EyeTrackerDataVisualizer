using LinqToDB.Mapping;

namespace Objects
{
    public class sensor_et
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public int Id_activity { get; set; }
        [NotNull]
        public int Id_session { get; set; }
        [NotNull]
        public float PosX { get; set; }
        [NotNull]
        public float PosY { get; set; }
        [NotNull]
        public float PupilDiaX { get; set; }
        [NotNull]
        public float PupilDiaY { get; set; }
        [NotNull]
        public float HeadX { get; set; }
        [NotNull]
        public float HeadY { get; set; }
        [NotNull]
        public float HeadZ { get; set; }
        [NotNull]
        public string Validity { get; set; }
        [NotNull]
        public long Timestamp { get; set; }

        public sensor_et()
        {
            
        }

        public sensor_et(int id, int idActivity, int idSession, float posX, float posY, float pupilDiaX,
            float pupilDiaY, float headX, float headY, float headZ, string validity, long timestamp)
        {
            Id = id;
            Id_activity = idActivity;
            Id_session = idSession;
            PosX = posX;
            PosY = posY;
            PupilDiaX = pupilDiaX;
            PupilDiaY = pupilDiaY;
            HeadX = headX;
            HeadY = headY;
            HeadZ = headZ;
            Validity = validity;
            Timestamp = timestamp;
        }
    }
}