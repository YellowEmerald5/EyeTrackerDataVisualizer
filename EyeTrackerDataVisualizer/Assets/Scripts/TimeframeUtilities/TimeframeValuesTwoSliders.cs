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
            _widthFillArea = fillArea.rect.width;
        }

        public void MinValueChanged()
        {
            if (maxSlider.value < minSlider.value)
            {
                maxSlider.value = minSlider.value;
            }
            valuesStorage.UpdateFromValue(minSlider.value);
            var minHandlePosition = minSlider.handleRect.position;
            fill.GetComponent<RectTransform>().anchorMax = new Vector2((maxSlider.handleRect.position.x/_widthFillArea)-(minHandlePosition.x/_widthFillArea), 1);
            fill.transform.position = new Vector3(minHandlePosition.x + fill.GetComponent<RectTransform>().rect.width/2,minHandlePosition.y,minHandlePosition.z);
            timeframeValueChanged.Raise();
        }

        public void MaxValueChanged()
        {
            if (maxSlider.value < minSlider.value)
            {
                maxSlider.value = minSlider.value;
            }
            
            fill.GetComponent<RectTransform>().anchorMax = new Vector2((maxSlider.handleRect.position.x/_widthFillArea)-(minSlider.handleRect.position.x/_widthFillArea), 1);
            valuesStorage.UpdateToValue(maxSlider.value);
            timeframeValueChanged.Raise();
        }
    }
}