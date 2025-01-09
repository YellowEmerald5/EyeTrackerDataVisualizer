using TMPro;
using UnityEngine;

namespace TimeLine
{
    public class TimelineDebugText : MonoBehaviour
    {
        [SerializeField] private TMP_Text textElement;
    
        /// <summary>
        /// Displays the value of the the slider in a text object
        /// </summary>
        /// <param name="value">Value of the slider</param>
        public void ChangeValue(float value)
        {
            textElement.text = value + "";
        }
    }
}
