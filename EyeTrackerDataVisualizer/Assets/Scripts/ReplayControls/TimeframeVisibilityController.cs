using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReplayControls
{
    public class TimeframeVisibilityController : MonoBehaviour
    {
        public Dictionary<long, List<GameObject>> pointsInTime;

        public void AddPoint(long pointInTime, GameObject visualPoint)
        {
            if (pointsInTime.ContainsKey(pointInTime))
            {
                pointsInTime[pointInTime].Add(visualPoint);
            }
            else
            {
                pointsInTime.Add(pointInTime,new List<GameObject>{visualPoint});
            }
        }

        public void HidePoints(float start, float end)
        {
            foreach (var points in pointsInTime.Where(pair => pair.Key < start || pair.Key > end))
            {
                foreach (var point in points.Value)
                {
                    point.SetActive(false);
                }
            }
            
            foreach (var points in pointsInTime.Where(pair => pair.Key >= start && pair.Key <= end))
            {
                foreach (var point in points.Value)
                {
                    point.SetActive(true);
                }
            }
        }
    }
}