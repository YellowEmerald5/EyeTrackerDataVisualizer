using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sprites", menuName = "ScriptableObjects/Sprites", order = 0)]

    public class Sprites : ScriptableObject
    {


        [SerializeField] private Dictionary<string,List<Sprite>> _sprites;

        
        public List<Sprite> GetSpritesFromGameId(string gameId)
        {
            if (_sprites.ContainsKey(gameId))
            {
                return _sprites[gameId];
            }
            else
            {
                return null;
            }
        }
    }
}