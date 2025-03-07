using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace GeneralUse
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        /// <summary>
        /// Flips the value of the other toggle
        /// </summary>
        /// <param name="value">Value of the current toggle</param>
        public void FlipToggle(bool value)
        {
            if (value)
            {
                toggle.isOn = false;
            }
            else
            {
                toggle.isOn = true;
            }
            
        }
    }
}