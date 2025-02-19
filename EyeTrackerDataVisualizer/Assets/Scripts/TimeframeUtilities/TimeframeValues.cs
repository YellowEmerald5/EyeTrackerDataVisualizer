using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;

public class TimeframeValues : MonoBehaviour
{
    public float maxValue;
    public float minValue;
    public float minimumValue;
    public float maximumValue;
    public TMP_Text errorMessage;
    public bool integer;
    public TimeframeValuesStorage storage;

    public void ChangeBothValues(string smallestValue, string biggestValue)
    {
        ChangeMaxValueWithString(biggestValue);
        ChangeMinValueWithString(smallestValue);
        storage.UpdateBothValues(minValue,maxValue);
    }
    public void ChangeMaxValueWithString(string input)
    {
        var currentValue = maxValue;
        if (integer)
        {
            try
            {
                maxValue = int.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                errorMessage.text = "Only decimal numbers are accepted";
                return;
            }
        }
        else
        {
            try
            {
                maxValue = float.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                errorMessage.text = "Decimal numbers must be written with . as separator and only numbers";
                return;
            }
        }
        
        if (minValue > maxValue)
        {
            maxValue = currentValue;
            errorMessage.text = "Max value cannot be less than min value";
            return;
        }

        if (maxValue > maximumValue)
        {
            maxValue = currentValue;
            errorMessage.text = "Max value cannot be greater than maximum value";
            return;
        }
        
        if (maxValue < minimumValue)
        {
            maxValue = currentValue;
            errorMessage.text = "Max value cannot be less than minimum value";
            return;
        }
        
        ResetText();
        storage.UpdateToValue(maxValue);
    }
    
    public void ChangeMinValueWithString(string input)
    {
        var currentValue = minValue;
        if (integer)
        {
            try
            {
                minValue = int.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                errorMessage.text = "Only numbers are accepted";
                return;
            }
        }
        else
        {
            try
            {
                maxValue = float.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                errorMessage.text = "Decimal numbers must be written with . as separator and only numbers";
                return;
            }
        }
        
        if (minValue > maximumValue)
        {
            minValue = currentValue;
            errorMessage.text = "Min value cannot be greater than maximum value";
            return;
        }

        if (minValue > maxValue)
        {
            minValue = currentValue;
            errorMessage.text = "Min value cannot be greater than max value";
            return;
        }
        
        if (minValue < minimumValue)
        {
            minValue = currentValue;
            errorMessage.text = "Min value cannot be less than minimum value";
            return;
        }
        
        ResetText();
        storage.UpdateFromValue(minValue);
    }

    private void ResetText()
    {
        errorMessage.text = "";
    }
}
