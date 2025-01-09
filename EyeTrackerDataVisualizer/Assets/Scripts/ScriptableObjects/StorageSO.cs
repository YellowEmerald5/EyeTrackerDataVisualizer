using System.Collections.Generic;
using Lists;
using Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "StorageSO", menuName = "ScriptableObjects/StorageSO", order = 0)]
    public class StorageSO : ScriptableObject
    {
        public List<User> TempUserList = new();
        public List<Session> TempSessionList = new();
        public List<Game> TempGameList = new();
        public List<User> UserList = new();
        public List<Session> SessionList = new();
        public List<Game> GameList = new();
        public MainMenuItemTypes CurrentItemType = MainMenuItemTypes.User;
        public float CurrentTimestamp;
        public float TotalTimestampEntries;
        public List<long> Timestamps = new ();
        public Camera MainCamera;
        public List<sensor_et> SensorData = new();
        public int TimeDelay = 0;
        public bool SpeedChanged = false;
        public bool ShowDestroyed = true;
        public List<Color> Colors = new ();
        
        /// <summary>
        /// Resets the values in storage. Not used outside of editor
        /// </summary>
        public void ResetStorage()
        {
            TempUserList = new List<User>();
            TempSessionList = new List<Session>();
            TempGameList = new List<Game>();
            UserList = new List<User>();
            SessionList = new List<Session>();
            GameList = new List<Game>();
            MainCamera = null;
            TimeDelay = 0;
        }

        public void ChangeShowDestroyed()
        {
            ShowDestroyed = !ShowDestroyed;
        }
    }
}