using ScriptableObjects;
using UnityEngine;

namespace ReplayControls
{
    public class GameVisibilityToggle : MonoBehaviour
    {
        public int gameId;
        public GameIdsForVisibility gameIds;
    
        /// <summary>
        /// Alters the state of the game to indicate if the game objects should be active
        /// </summary>
        /// <param name="value"></param>
        public void AlterStateInStorage(bool value)
        {
            gameIds.IdAndStateStorage[gameId] = value;
        }

        /// <summary>
        /// Adds a game to the dictionary
        /// </summary>
        public void AddGameToDictionary()
        {
            gameIds.IdAndStateStorage.Add(gameId,true);
        }
    }
}
