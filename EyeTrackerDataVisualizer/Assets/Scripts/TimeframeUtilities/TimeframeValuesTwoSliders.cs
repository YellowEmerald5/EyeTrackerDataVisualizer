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
        [SerializeField] private GameObject fill;
        [SerializeField] private RectTransform fillArea;
        
        [SerializeField] private bool wholeNumbers;

        private Vector3 _fillScale;
        private float _width;
        private float _widthFillArea;
        
        private void Start()
        {
            if (wholeNumbers)
            {
                minSlider.wholeNumbers = true;
                maxSlider.wholeNumbers = true;
            }
            minSlider.maxValue = storage.TotalTimestampEntries;
            maxSlider.maxValue = storage.TotalTimestampEntries;
            _width = minSlider.GetComponent<RectTransform>().rect.width;
            _fillScale = fill.transform.localScale;
            _width = fillArea.rect.width;
        }

        public void MinValueChanged()
        {
            if (maxSlider.value < minSlider.value)
            {
                maxSlider.value = minSlider.value;
            }
            valuesStorage.UpdateFromValue(minSlider.value);
            fill.transform.position = minSlider.handleRect.position;
            timeframeValueChanged.Raise();
        }

        public void MaxValueChanged()
        {
            if (maxSlider.value < minSlider.value)
            {
                maxSlider.value = minSlider.value;
            }

            //var difference = maxSlider.transform.position - minSlider.transform.position;
            //var percentage = difference / _width;
            valuesStorage.UpdateToValue(maxSlider.value);
            //fill.transform.localScale = new Vector3((percentage * _widthFillArea).x,_fillScale.y,_fillScale.z);
            timeframeValueChanged.Raise();
        }
    }
}