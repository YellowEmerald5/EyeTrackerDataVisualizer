using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ReplayControls
{
    public class GameVisibilityControl : MonoBehaviour
    {
        private readonly Dictionary<int,GameObject> _objectsToHide = new ();
        public GameIdsForVisibility gameIdsStorage;
        public ShowHideTimelineTimeframe showHide;
        public bool TimeType; // false = timeline. true = timeframes

        public void ShowHideAll()
        {
            var active = TimeType == false ? showHide.showTimeline : showHide.showTimeframe;
            foreach (var pair in _objectsToHide)
            {
                pair.Value.SetActive(active);
            }
        }

        public void AddGame(int id, GameObject obj)
        {
            _objectsToHide.Add(id,obj);
        }
    }
}