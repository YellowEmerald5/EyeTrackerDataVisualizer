using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeframeValues : MonoBehaviour
{
    public float toValue;
    public float fromValue;
    public float minimumValue;
    public float maximumValue;
    public TMP_Text errorMessage;
    public bool integer;
    public TimeframeValuesStorage storage;

    /// <summary>
    /// Changes both minimum and maximum value
    /// </summary>
    /// <param name="smallestValue">Start value of the lists</param>
    /// <param name="biggestValue">Length of the timestamps list</param>
    public void ChangeBothValuesWithStrings(string smallestValue, string biggestValue)
    {
        ChangeMaxValueWithString(biggestValue);
        ChangeMinValueWithString(smallestValue);
        storage.UpdateBothValues(fromValue,toValue);
    }
    
    /// <summary>
    /// Takes a string and changes to value with it
    /// </summary>
    /// <param name="input">String to parse</param>
    public void ChangeMaxValueWithString(string input)
    {
        var currentValue = toValue;
        if (integer)
        {
            try
            {
                toValue = int.Parse(input);
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
                toValue = float.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                errorMessage.text = "Decimal numbers must be written with . as separator and only numbers";
                return;
            }
        }
        
        if (fromValue > toValue)
        {
            toValue = currentValue;
            errorMessage.text = "Max value cannot be less than min value";
            return;
        }

        if (toValue > maximumValue)
        {
            toValue = currentValue;
            errorMessage.text = "Max value cannot be greater than maximum value";
            return;
        }
        
        if (toValue < minimumValue)
        {
            toValue = currentValue;
            errorMessage.text = "Max value cannot be less than minimum value";
            return;
        }
        
        ResetText();
        storage.UpdateToValue(toValue);
    }
    
    /// <summary>
    /// Takes a string and changes from value with it
    /// </summary>
    /// <param name="input">String to parse</param>
    public void ChangeMinValueWithString(string input)
    {
        var currentValue = fromValue;
        if (integer)
        {
            try
            {
                fromValue = int.Parse(input);
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
                toValue = float.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                errorMessage.text = "Decimal numbers must be written with . as separator and only numbers";
                return;
            }
        }
        
        if (fromValue > maximumValue)
        {
            fromValue = currentValue;
            errorMessage.text = "Min value cannot be greater than maximum value";
            return;
        }

        if (fromValue > toValue)
        {
            fromValue = currentValue;
            errorMessage.text = "Min value cannot be greater than max value";
            return;
        }
        
        if (fromValue < minimumValue)
        {
            fromValue = currentValue;
            errorMessage.text = "Min value cannot be less than minimum value";
            return;
        }
        
        ResetText();
        storage.UpdateFromValue(fromValue);
    }

    /// <summary>
    /// Resets the error message
    /// </summary>
    private void ResetText()
    {
        errorMessage.text = "";
    }
}
