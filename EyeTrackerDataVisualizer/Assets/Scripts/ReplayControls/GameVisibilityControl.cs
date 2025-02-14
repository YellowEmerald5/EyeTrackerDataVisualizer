using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ReplayControls
{
    public class GameVisibilityControl : MonoBehaviour
    {
        private readonly Dictionary<int,List<GameObject>> _objectsToHide = new ();
        public GameIdsForVisibility gameIdsStorage;
        public ShowHideTimelineTimeframe showHide;
        public bool TimeType; // false = timeline. true = timeframes

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

        public void ShowHideGame()
        {
            var active = TimeType == false ? showHide.showTimeline : showHide.showTimeframe;
            foreach (var pair in _objectsToHide)
            {
                foreach (var gameObject in pair.Value)
                {
                    if (gameObject.activeSelf & active)
                    {
                        gameObject.SetActive(false);
                    }else if (gameObject.activeSelf ^ active)
                    {
                        gameObject.SetActive(true);
                    }
                }
            }
        }

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