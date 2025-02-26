using UnityEngine;

namespace ScriptsForImagesAndVideos
{
    public class ToggleOnOff : MonoBehaviour
    {
        /// <summary>
        /// Toggles the game object based on the given value
        /// </summary>
        /// <param name="value"></param>
        public void Toggle(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}