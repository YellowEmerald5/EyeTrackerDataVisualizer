using System;
using System.Collections.Generic;
using Objects;

namespace DataAccessAndPreparation
{
    public class PreparedData
    {
        public List<Game> PreparedGames { get; set; }
        public Dictionary<long,Tuple<int,int>> StartEndGazePoints { get; set; }
        public List<List<sensor_et>> SensorData { get; set; }
        public Dictionary<int,Tuple<int,int>> StartAndEndPoints { get; set; }
        public float TotalTimestamps { get; set; }

        /// <summary>
        /// Creates an object used to transfer data from the database handler to set up for replay
        /// </summary>
        /// <param name="games">List of chosen games</param>
        /// <param name="timeStamps">List of unique timestamps</param>
        /// <param name="sensorData">Data points from eye tracker</param>
        /// <param name="totalTimestamps">Total amount of timestamps</param>
        public PreparedData(List<Game> games, Dictionary<long,Tuple<int,int>> startAndEndGazePoints, Dictionary<int,Tuple<int,int>> startAndEndPoints, List<List<sensor_et>> sensorData, float totalTimestamps)
        {
            PreparedGames = games;
            StartEndGazePoints = startAndEndGazePoints;
            SensorData = sensorData;
            StartAndEndPoints = startAndEndPoints;
            TotalTimestamps = totalTimestamps;
        }
    }
}