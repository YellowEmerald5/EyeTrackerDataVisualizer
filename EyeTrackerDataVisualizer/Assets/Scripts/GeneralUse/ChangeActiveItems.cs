using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActiveItems : MonoBehaviour
{
    [Tooltip("This list is active first")]
    public List<GameObject> List1;
    [Tooltip("This list is active after value has changed")]
    public List<GameObject> List2;
    [Tooltip("Starting value of the toggle")]
    public bool StartValue;
    
    private void Start()
    {
        foreach (var item in List1)
        {
            item.SetActive(true);            
        }

        foreach (var item in List2)
        {
            item.SetActive(false);
        }
    }

    public void ChangeActive(bool value)
    {
        if (StartValue)
        {
            foreach (var item in List1)
            {
                item.SetActive(value);            
            }

            foreach (var item in List2)
            {
                item.SetActive(!value);
            }
        }
        else
        {
            foreach (var item in List1)
            {
                item.SetActive(!value);            
            }

            foreach (var item in List2)
            {
                item.SetActive(value);
            }
        }
        
    }
}
