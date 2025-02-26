using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TimeframeValueStorage", menuName = "ScriptableObjects/TimeframeValuesStorage", order = 0)]
    public class TimeframeValuesStorage : ScriptableObject
    {
        public float fromValue;
        public float toValue;

        /// <summary>
        /// Updates the value the timeframe starts from
        /// </summary>
        /// <param name="value">Value chosen</param>
        public void UpdateFromValue(float value)
        {
            fromValue = value;
        }

        /// <summary>
        /// Updates the value the timeframe ends at
        /// </summary>
        /// <param name="value">Value chosen</param>
        public void UpdateToValue(float value)
        {
            toValue = value;
        }

        /// <summary>
        /// Updates to and from values
        /// </summary>
        /// <param name="lowest">From value</param>
        /// <param name="highest">To value</param>
        public void UpdateBothValues(float lowest, float highest)
        {
            fromValue = lowest;
            toValue = highest;
        }

        /// <summary>
        /// Resets both values
        /// </summary>
        public void Reset()
        {
            fromValue = 0;
            toValue = 0;
        }
    }
}