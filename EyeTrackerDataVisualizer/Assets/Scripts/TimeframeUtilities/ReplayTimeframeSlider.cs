using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class ReplayTimeframeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject fill;
    [SerializeField] private TimeframeValuesStorage valuesStorage;
    [SerializeField] private StorageSO storage;
    [SerializeField] private Slider minSlider;
    [SerializeField] private Slider maxSlider;
    [SerializeField] private RectTransform fillArea;

    private bool _play;

    private int i;
    private float _widthFillArea;

    private void Start()
    {
        _widthFillArea = fillArea.rect.width;
    }

    public void MinValueChanged()
    {
        slider.minValue = valuesStorage.fromValue;
        if (slider.handleRect.position.x < minSlider.handleRect.position.x)
        {
            slider.handleRect.position = minSlider.handleRect.position;
        }
        var minHandlePosition = slider.handleRect.position;
        fill.GetComponent<RectTransform>().anchorMax = new Vector2((maxSlider.handleRect.position.x/_widthFillArea)-(minHandlePosition.x/_widthFillArea), 1);
        fill.transform.position = new Vector3(minHandlePosition.x + fill.GetComponent<RectTransform>().rect.width/2,minHandlePosition.y,minHandlePosition.z);
    }
    
    public void MaxValueChanged()
    {
        slider.maxValue = valuesStorage.toValue;
        if (slider.handleRect.position.x > maxSlider.handleRect.position.x)
        {
            slider.handleRect.position = maxSlider.handleRect.position;
        }
        fill.GetComponent<RectTransform>().anchorMax = new Vector2(_widthFillArea * (slider.maxValue / slider.minValue), 1);
    }

    private void Update()
    {
        if (i == storage.TimeDelay && _play)
        {
            slider.value++;
            if (slider.value >= slider.maxValue)
            {
                slider.value = slider.minValue;
            }
        }

        if (i < storage.TimeDelay)
        {
            i++;
        }
        else
        {
            i = 0;
        }
    }

    public void ValueChanged()
    {
        if (slider.handleRect.position.x < minSlider.handleRect.position.x)
        {
            slider.handleRect.position = minSlider.handleRect.position;
        }

        if (slider.handleRect.position.x > maxSlider.handleRect.position.x)
        {
            slider.handleRect.position = maxSlider.handleRect.position;
        }
    }

    public void ChangePlayState()
    {
        _play = !_play;
    }
}
