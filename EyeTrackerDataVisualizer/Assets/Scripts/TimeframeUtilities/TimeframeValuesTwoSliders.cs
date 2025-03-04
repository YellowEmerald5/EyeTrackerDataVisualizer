using GameEventScripts;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TimeframeUtilities
{
    public class TimeframeValuesTwoSliders : MonoBehaviour
    {
        [SerializeField] private Slider minSlider;
        [SerializeField] private Slider maxSlider;
        [SerializeField] private StorageSO storage;
        [SerializeField] private TimeframeValuesStorage valuesStorage;
        [SerializeField] private GameEvent timeframeValueChanged;
        [SerializeField] private bool wholeNumbers;
        
        private void Start()
        {
            if (wholeNumbers)
            {
                minSlider.wholeNumbers = true;
                maxSlider.wholeNumbers = true;
            }
            minSlider.maxValue = storage.TotalTimestampEntries;
            maxSlider.maxValue = storage.TotalTimestampEntries;
        }

        public void MinValueChanged()
        {
            if (maxSlider.value < minSlider.value)
            {
                maxSlider.value = minSlider.value;
            }
            valuesStorage.UpdateFromValue(minSlider.value);
            timeframeValueChanged.Raise();
        }

        public void MaxValueChanged()
        {
            if (maxSlider.value < minSlider.value)
            {
                maxSlider.value = minSlider.value;
            }
            valuesStorage.UpdateToValue(maxSlider.value);
            timeframeValueChanged.Raise();
        }
    }
}