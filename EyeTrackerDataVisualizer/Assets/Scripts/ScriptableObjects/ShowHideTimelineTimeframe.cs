using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShowHideTimelineTimeframe", menuName = "ScriptableObjects/ShowHideTimelineTimeframe", order = 0)]
    public class ShowHideTimelineTimeframe : ScriptableObject
    {
        public bool showTimeline = true;
        public bool showTimeframe = false;

        /// <summary>
        /// changes show timeline and show timeframe to the opposite value
        /// </summary>
        public void FlipActive()
        {
            showTimeline = !showTimeline;
            showTimeframe = !showTimeframe;
        }
        
        /// <summary>
        /// Resets the values of show timeline and show timeframe
        /// </summary>
        public void Reset()
        {
            showTimeline = true;
            showTimeframe = false;
        }
    }
}