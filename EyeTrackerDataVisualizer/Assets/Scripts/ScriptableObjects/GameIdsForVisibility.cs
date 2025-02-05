using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameIdForVisibility", menuName = "ScriptableObjects/GameIdIntermediaryStorage", order = 0)]
    public class GameIdsForVisibility : ScriptableObject
    {
        public List<int> gameIDs;
    }
}