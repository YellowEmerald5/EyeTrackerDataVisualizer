using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using Objects;
using ScriptableObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public class DatabaseHandler : MonoBehaviour
    {
        [SerializeField] private StorageSO storage;
        private static string Server = "localhost";
        private static string Database = "objectdata";
        private static string User = "root";
        private static string Pwd = "EMPOWERpwd";
        private static string Port = "3306";
        private void Awake()
        {
            //using (var db = new DataConnection())
            //{
            //    var data = db.GetTable<User>();
            //}
            var dataConnection = getDataConnection();
            
            storage.UserList = dataConnection.GetTable<User>().Select(u => new User(u.Id,u.Nickname)).ToList();
            
            dataConnection.Dispose();
        }

        public void GetSessions()
        {
            var dataConnection = getDataConnection();
            foreach (var user in storage.UserList)
            {
                storage.SessionList = dataConnection.GetTable<Session>().Select(s => new Session(s.Id,s.UserId)).Where(s => s.UserId == user.Id).ToList();
            }
            dataConnection.Dispose();
        }

        public void GetGames()
        {
            var dataConnection = getDataConnection();
            foreach (var session in storage.SessionList)
            {
                storage.GameList = dataConnection.GetTable<Game>().Select(g => new Game(g.TimesPlayed,g.AmountOfGamesPlayed,g.Name,session.UserId,g.SessionId)).Where(g => g.SessionId == session.Id).ToList();
            }
            dataConnection.Dispose();
        }

        public void PrepareReplay()
        {
            //Get the objects in the games from the database
        }

        private DataConnection getDataConnection()
        {
            return new DataConnection(new DataOptions().UseMySql($"Server={Server};" +
                                                                               $"Database={Database};Uid={User};Pwd={Pwd};" +
                                                                               $"Port={Port};Charset=utf8;" +
                                                                               "Allow User Variables=True;"));
        }
    }
}