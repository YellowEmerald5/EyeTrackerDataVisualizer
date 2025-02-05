using System;
using System.Collections.Generic;
using Objects;

namespace DataAccessAndPreparation
{
    public class PreparedData
    {
        public List<Game> PreparedGames { get; set; }
        public List<long> TimeStamps { get; set; }
        public List<sensor_et> SensorData { get; set; }
        public Dictionary<string,Tuple<int,int>> StartAndEndPoints { get; set; }
        public float TotalTimestamps { get; set; }

        /// <summary>
        /// Creates an object used to transfer data from the database handler to set up for replay
        /// </summary>
        /// <param name="games">List of chosen games</param>
        /// <param name="timeStamps">List of unique timestamps</param>
        /// <param name="sensorData">Data points from eye tracker</param>
        /// <param name="totalTimestamps">Total amount of timestamps</param>
        public PreparedData(List<Game> games, List<long> timeStamps, Dictionary<string,Tuple<int,int>> startAndEndPoints, List<sensor_et> sensorData, float totalTimestamps)
        {
            PreparedGames = games;
            TimeStamps = timeStamps;
            SensorData = sensorData;
            StartAndEndPoints = startAndEndPoints;
            TotalTimestamps = totalTimestamps;
        }
    }
}