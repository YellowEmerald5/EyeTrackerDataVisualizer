using ScriptableObjects;
using UnityEngine;

namespace ReplayControls
{
    public class GameVisibilityToggle : MonoBehaviour
    {
        public int gameId;
        public GameIdsForVisibility gameIds;
    
        public void AlterStateInStorage(bool value)
        {
            gameIds.IdAndStateStorage[gameId] = value;
        }

        public void AddGameToDictionary()
        {
            gameIds.IdAndStateStorage.Add(gameId,true);
        }
    }
}
