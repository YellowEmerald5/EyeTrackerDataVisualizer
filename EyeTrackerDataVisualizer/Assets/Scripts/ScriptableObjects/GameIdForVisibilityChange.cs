using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameIdForVisibilityChange", menuName = "ScriptableObjects/GameIdForVisibilityChange", order = 0)]
    public class GameIdForVisibilityChange : ScriptableObject
    {
        public int gameId;
    }
}