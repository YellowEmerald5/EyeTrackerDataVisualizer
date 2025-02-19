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