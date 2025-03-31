using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ReplayControls
{
    public class GameVisibilityControl : MonoBehaviour
    {
        private readonly Dictionary<int,List<GameObject>> _objectsToHide = new ();
        public GameIdsForVisibility gameIdToVisibility;
        public ShowHideTimelineTimeframe showHide;
        public GameIdForVisibilityChange keyStorage;
        public bool TimeType; // false = timeline. true = timeframes

        /// <summary>
        /// Shows or hides all game objects in objects to hide
        /// </summary>
        public void ShowHideAll()
        {
            var active = TimeType == false ? showHide.showTimeline : showHide.showTimeframe;
            foreach (var pair in _objectsToHide)
            {
                foreach (var gameObject in pair.Value)
                {
                    gameObject.SetActive(active);
                }
            }
        }

        /// <summary>
        /// Shows or hides a single game
        /// </summary>
        /// <param name="key">Game id</param>
        /// <param name="active">If the game should be active or not</param>
        public void ShowHideGame()
        {
            var game = _objectsToHide[keyStorage.gameId];
            foreach (var obj in game)
            {
                obj.SetActive(gameIdToVisibility.IdAndStateStorage[keyStorage.gameId]);
            }
        }

        /// <summary>
        /// Adds a game object to the objects to hide dictionary
        /// </summary>
        /// <param name="id">Id of the game</param>
        /// <param name="obj">Object to add</param>
        public void AddGame(int id, GameObject obj)
        {
            if (!_objectsToHide.ContainsKey(id))
            {
                _objectsToHide.Add(id,new List<GameObject>());
            }
            _objectsToHide[id].Add(obj);
            
        }
    }
}