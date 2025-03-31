using System;
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
        public List<Game> TempGameList = new();
        public List<User> UserList = new();
        public List<Game> GameList = new();
        public MainMenuItemTypes CurrentItemType = MainMenuItemTypes.User;
        public float CurrentTimestamp;
        public float TotalTimestampEntries;
        public Dictionary<long, Tuple<int, int>> StartEndGazePoints = new ();
        public Camera MainCamera;
        public List<List<sensor_et>> SensorData = new();
        public int TimeDelay = 0;
        public bool SpeedChanged = false;
        public bool ShowDestroyed = true;
        public List<Color> Colors = new ();
        public Dictionary<int, Tuple<int, int>> StartAndEndPoints = new ();
        public bool twoD = true;
        
        /// <summary>
        /// Resets the values in storage. Not used outside of editor
        /// </summary>
        public void Reset()
        {
            TempUserList = new List<User>();
            TempGameList = new List<Game>();
            UserList = new List<User>();
            GameList = new List<Game>();
            CurrentItemType = MainMenuItemTypes.User;
            MainCamera = null;
            CurrentTimestamp = 0;
            TimeDelay = 0;
            TotalTimestampEntries = 0;
            SensorData = new List<List<sensor_et>>();
            SpeedChanged = false;
            ShowDestroyed = true;
            StartAndEndPoints = new Dictionary<int, Tuple<int, int>>();
            StartEndGazePoints = new Dictionary<long, Tuple<int, int>>();
            twoD = true;
        }

        /// <summary>
        /// Changes show destroyed to the opposite value
        /// </summary>
        public void ChangeShowDestroyed()
        {
            ShowDestroyed = !ShowDestroyed;
        }
    }
}