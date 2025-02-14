using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UIRangeSliderNamespace;
using UnityEngine;

public class TimeframeSliderSetup : MonoBehaviour
{
    public StorageSO storage;
    public UIRangeSlider slider;
    void Start()
    {
        slider.maxLimit = storage.TotalTimestampEntries;
    }
}
