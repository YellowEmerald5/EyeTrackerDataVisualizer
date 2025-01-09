using UnityEngine;
using UnityEngine.UI;

namespace GeneralUse
{
    public class SwitchActiveForToggle : MonoBehaviour
    {
        [SerializeField] private GameObject object1;
        [SerializeField] private GameObject object2;

        /// <summary>
        /// Activates  one object and deactivates the other. First time deactivates object1 and activates object2
        /// </summary>
        /// <param name="value">Value of a toggle</param>
        public void SwitchActive(bool value)
        {
            object1.SetActive(!object1.activeSelf);
            object2.SetActive(!object2.activeSelf);
        }
    }
}