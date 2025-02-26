using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace ReplayControls
{
    public class TimeframeVisibilityController : MonoBehaviour
    {
        public Dictionary<float, List<GameObject>> PointsInTime = new ();
        public Dictionary<float, List<GameObject>> GazePointsInTime = new ();
        public TimeframeValuesStorage storage;

        /// <summary>
        /// Adds a single position to the points in time dictionary
        /// </summary>
        /// <param name="pointInTime">Timestamp for the position</param>
        /// <param name="visualPoint">Game object representing the object</param>
        public void AddObjectPoint(long pointInTime, GameObject visualPoint)
        {
            if (PointsInTime.ContainsKey(pointInTime))
            {
                PointsInTime[pointInTime].Add(visualPoint);
            }
            else
            {
                PointsInTime.Add(pointInTime,new List<GameObject>{visualPoint});
            }
        }

        /// <summary>
        /// Adds a single gaze point to the gaze points in time dictionary
        /// </summary>
        /// <param name="timeValue">Timestamp for the gaze point</param>
        /// <param name="gazePoint">Game object representing the gaze point</param>
        public void AddGazePoint(long timeValue, GameObject gazePoint)
        {
            if (GazePointsInTime.ContainsKey(timeValue))
            {
                GazePointsInTime[timeValue].Add(gazePoint);
            }
            else
            {
                GazePointsInTime.Add(timeValue,new List<GameObject>{gazePoint});
            }
        }

        /// <summary>
        /// Hides all points outside of the timeframe range and shows all points inside the range
        /// </summary>
        public void HidePoints()
        {
            var start = storage.fromValue;
            var end = storage.toValue;
            
            foreach (var points in PointsInTime.Where(pair => pair.Key < start || pair.Key > end))
            {
                foreach (var point in points.Value)
                {
                    point.SetActive(false);
                }
            }
            
            foreach (var points in PointsInTime.Where(pair => pair.Key >= start && pair.Key <= end))
            {
                foreach (var point in points.Value)
                {
                    point.SetActive(true);
                }
            }
            
            foreach (var points in GazePointsInTime.Where(pair => pair.Key < start || pair.Key > end))
            {
                foreach (var point in points.Value)
                {
                    point.SetActive(false);
                }
            }
            
            foreach (var points in GazePointsInTime.Where(pair => pair.Key >= start && pair.Key <= end))
            {
                foreach (var point in points.Value)
                {
                    point.SetActive(true);
                }
            }
        }
    }
}