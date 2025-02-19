using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace TimeframeUtilities
{
    public class MaxValueSetUp : MonoBehaviour
    {
        public StorageSO storage;
        public TimeframeValues timeframe;
        public TMP_Text maxValue;

        private void Start()
        {
            timeframe.maximumValue = storage.TotalTimestampEntries;
            maxValue.text = "Maximum value: " + timeframe.maximumValue;
        }
    }
}