using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using MySqlConnector;
using Objects;
using UnityEngine;

namespace DataAccessAndPreparation
{
    public static class DatabaseHandler
    {
        private static string Server = "localhost";
        private static string Database = "objectdata";
        private static string User = "root";
        private static string Pwd = "EMPOWERpwd";
        private static string Port = "3306";
        
        /// <summary>
        /// Fetches all users from the database
        /// </summary>
        /// <returns>List users</returns>
        public static List<User> GetUsers()
        {
            var dataConnection = GetDataConnection();
            
            var users = dataConnection.GetTable<User>().Select(u => new User(u.Id,u.Nickname)).ToList();
            
            dataConnection.Dispose();
            return users;
        }

        /// <summary>
        /// Fetches sessions belonging to the chosen users from the database
        /// </summary>
        /// <param name="chosenUsers">Users chosen for replay</param>
        /// <returns>List of sessions</returns>
        public static List<Session> GetSessions(List<User> chosenUsers)
        {
            if (chosenUsers.Count == 0) return new List<Session>();
            var dataConnection = GetDataConnection();
            var listSessions = new List<Session>();
            foreach (var user in chosenUsers)
            {
                var sessions = dataConnection.GetTable<Session>().Where(s => s.UserId == user.Id)
                    .Select(s =>
                        new Session(s.Id, s.SessionNumber, s.UserId))
                    .ToList();
                user.Sessions = new List<Session>();
                user.Sessions.AddRange(sessions);
                listSessions.AddRange(sessions);
            }

            dataConnection.Dispose();
            return listSessions;
        }

        /// <summary>
        /// Fetches games belonging to the chosen sessions from the database 
        /// </summary>
        /// <param name="selectedSessions">List of selected sessions</param>
        /// <returns>List of games</returns>
        public static List<Game> GetGames(List<Session> selectedSessions)
        {
            if (selectedSessions.Count == 0) return new List<Game>();
            var dataConnection = GetDataConnection();
            var games = new List<Game>();
            foreach (var session in selectedSessions)
            {
                games.AddRange( dataConnection.GetTable<Game>()
                    .Select(g => new Game(g.TimesPlayed, g.AmountOfGamesPlayed, g.Name, g.UserId, g.SessionId))
                    .Where(g => g.SessionId == session.Id)
                    .ToList());
            }
            
            dataConnection.Dispose();
            return games;
        }

        /// <summary>
        /// Gets object data for the chosen games
        /// </summary>
        /// <param name="games">List of chosen games</param>
        /// <returns>Data used for setting up the replays</returns>
        public static PreparedData PrepareReplay(List<Game> games)
        {
            var dataConnection = GetDataConnection();
            var timestamps = new List<long>();
            var sensorData = new List<List<sensor_et>>();
            var totalTimestamps = 0;
            var startAndEndPoints = new Dictionary<string,Tuple<int,int>>();
            var startEndGazePoints = new Dictionary<long, Tuple<int, int>>();
            foreach (var game in games)
            {
                game.Objects = dataConnection.GetTable<ObjectInGame>().Select(o =>
                        new ObjectInGame(o.Name, o.GameId, o.TimeSpawn, o.SpawnPositionX,
                            o.SpawnPositionY,
                            o.SpawnPositionZ, o.TimeDestroyed, o.EndPositionX, o.EndPositionY, o.EndPositionZ))
                    .Where(o => o.GameId == game.Id).ToList();

                var start = 0L;
                var end = 0L;
                var first = true;
                var gameStart = 0L;
                var gameEnd = 0L;
                foreach (var obj in game.Objects)
                {
                    obj.Aoi = dataConnection.GetTable<Aoi>().Select(a => new Aoi(a.Id, a.ObjectName, a.TimeSpawn,
                        a.StartPositionX, a.StartPositionY, a.TimeDestroy, a.EndPositionX, a.EndPositionY)).FirstOrDefault(a => a.ObjectName.Equals(obj.Name));
                    obj.Aoi.Sizes = dataConnection.GetTable<AoiSize>()
                        .Select(s => new AoiSize(s.Id, s.AoiId, s.Height, s.Width))
                        .Where(s => s.AoiId.Equals(obj.Aoi.Id)).ToList();
                    obj.Aoi.Origins = dataConnection.GetTable<AoiOrigin>()
                        .Select(o => new AoiOrigin(o.Id,o.AoiId,o.PosX,o.PosY))
                        .Where(o => o.AoiId.Equals(obj.Aoi.Id)).ToList();
                    obj.Points = dataConnection.GetTable<Point>()
                        .Select(p => new Point(p.Id, p.ObjectName, p.Time, p.PosX, p.PosY, p.PosZ))
                        .Where(p => p.ObjectName.Equals(obj.Name)).ToList();

                    foreach (var point in obj.Points.Where(point => !timestamps.Contains(point.Time)))
                    {
                        timestamps.Add(point.Time);
                        if (point.Time < start)
                        {
                            start = point.Time;
                        }

                        if (point.Time > end)
                        {
                            end = point.Time;
                        }

                        if (!first) continue;
                        start = point.Time;
                        first = false;
                    }

                    var objectStartTime = obj.Points[0].Time;
                    var objectEndTime = obj.Points[^1].Time;

                    
                    if (objectStartTime < gameStart || gameStart == 0)
                    {
                        gameStart = objectStartTime;
                    }
                    
                    if (objectEndTime > gameEnd || gameEnd == 0)
                    {
                        gameEnd = objectEndTime;
                    }
                    timestamps.Sort();
                    var objectStartPoint = timestamps.FindIndex(timestamp => timestamp == objectStartTime);
                    var objectEndPoint = timestamps.FindIndex(timestamp => timestamp == objectEndTime);
                    startAndEndPoints.Add(obj.Name,new Tuple<int, int>(objectStartPoint,objectEndPoint));

                }
                
                sensorData.Add(new List<sensor_et>());
                try
                {
                    sensorData[^1].AddRange(dataConnection.GetTable<sensor_et>().Select(s =>
                            new sensor_et(s.Id,s.Id_activity,s.Id_session,s.PosX,s.PosY,s.PupilDiaX,s.PupilDiaY,s.HeadX,s.HeadY,s.HeadZ,s.Validity,s.Timestamp))
                        .Where(s => s.Id_activity == game.Id && s.Timestamp > gameStart && s.Timestamp < gameEnd).ToList());
                }
                catch (MySqlException e)
                {
                    sensorData[^1] = ReadFromCSV.GetSensorDataFromCSV().Where(d => d.Id_activity == 1121 && d.Validity.Equals("1")).ToList();
                    Console.Write(e.ToString());
                }

                for (var i = 0; i < sensorData.Count; i++)
                {
                    for (var k = 0; k < sensorData[i].Count; k++)
                    {
                        if (!timestamps.Contains(sensorData[i][k].Timestamp))
                        {
                            timestamps.Add(sensorData[i][k].Timestamp);
                        }
                    }
                }

                if (sensorData[^1].Count > 0)
                {
                    timestamps.Sort();
                    var gazeStartPoint = timestamps.FindIndex(timestamp => timestamp == sensorData[^1][0].Timestamp);
                    var gazeEndPoint = timestamps.FindIndex(timestamp => timestamp == sensorData[^1][^1].Timestamp);
                    startEndGazePoints.Add(sensorData[^1][0].Timestamp,
                            new Tuple<int, int>(gazeStartPoint, gazeEndPoint));
                }
            }
            totalTimestamps = timestamps.Count-1;
            return new PreparedData(games,startEndGazePoints,startAndEndPoints,sensorData,totalTimestamps);
        }


        /// <summary>
        /// Prepares a database connection
        /// </summary>
        /// <returns>Database connection</returns>
        private static DataConnection GetDataConnection()
        {
            return new DataConnection(new DataOptions().UseMySql($"Server={Server};" +
                                                                               $"Database={Database};Uid={User};Pwd={Pwd};" +
                                                                               $"Port={Port};Charset=utf8;" +
                                                                               "Allow User Variables=True;"));
        }

    }
}