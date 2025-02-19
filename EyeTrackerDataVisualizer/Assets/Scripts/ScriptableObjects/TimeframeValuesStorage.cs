using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TimeframeValueStorage", menuName = "ScriptableObjects/TimeframeValuesStorage", order = 0)]
    public class TimeframeValuesStorage : ScriptableObject
    {
        public float fromValue;
        public float toValue;

        public void UpdateFromValue(float value)
        {
            fromValue = value;
        }

        public void UpdateToValue(float value)
        {
            toValue = value;
        }

        public void UpdateBothValues(float lowest, float highest)
        {
            fromValue = lowest;
            toValue = highest;
        }

        public void Reset()
        {
            fromValue = 0;
            toValue = 0;
        }
    }
}