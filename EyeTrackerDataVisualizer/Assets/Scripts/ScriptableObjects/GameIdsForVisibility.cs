using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameIdForVisibility", menuName = "ScriptableObjects/GameIdIntermediaryStorage", order = 0)]
    public class GameIdsForVisibility : ScriptableObject
    {
        public Dictionary<int,bool> IdAndStateStorage = new ();
        
        public void Reset()
        {
            IdAndStateStorage = new Dictionary<int, bool>();
        }
    }
}