using UnityEngine;

namespace ScriptsForImagesAndVideos
{
    public class ToggleOnOff : MonoBehaviour
    {
        public void Toggle(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}