using System;
using GameEventScripts;
using Objects;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ReplayControls
{
    public class GameVisibilityToggle : MonoBehaviour
    {
        public Game game;
        public GameIdsForVisibility gameIds;
        public GameIdForVisibilityChange gameIdForVisibilityChange;
        public GameEvent valueChanged;
        
        public void BeginSetUp()
        {
            var textItem = gameObject.GetComponentInChildren<TMP_Text>();
            textItem.text = game.Name + " num: " + game.AmountOfTimesPlayed;
        }

        /// <summary>
        /// Alters the state of the game to indicate if the game objects should be active
        /// </summary>
        /// <param name="value"></param>
        public void AlterStateInStorage(bool value)
        {
            gameIdForVisibilityChange.gameId = game.Id;
            gameIds.IdAndStateStorage[game.Id] = value;
            valueChanged.Raise();
        }

        /// <summary>
        /// Adds a game to the dictionary
        /// </summary>
        public void AddGameToDictionary()
        {
            gameIds.IdAndStateStorage.Add(game.Id,true);
        }
    }
}
