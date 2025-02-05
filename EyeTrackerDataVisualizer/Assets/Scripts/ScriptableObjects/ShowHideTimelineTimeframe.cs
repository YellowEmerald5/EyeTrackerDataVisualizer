using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShowHideTimelineTimeframe", menuName = "ScriptableObjects/ShowHideTimelineTimeframe", order = 0)]
    public class ShowHideTimelineTimeframe : ScriptableObject
    {
        public bool showTimeline = true;
        public bool showTimeframe = false;

        public void FlipActive()
        {
            showTimeline = !showTimeline;
            showTimeframe = !showTimeframe;
        }
        
        public void Reset()
        {
            showTimeline = true;
            showTimeframe = false;
        }
    }
}