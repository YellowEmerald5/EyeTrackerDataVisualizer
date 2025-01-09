using System.Collections.Generic;
using UnityEngine;

namespace ReplayControls
{
    public class GameVisibilityControl : MonoBehaviour
    {
        public List<MeshRenderer> objectsToHide = new ();

        /// <summary>
        /// Hides objects from selected games when the toggle is not checked
        /// </summary>
        /// <param name="value">Boolean value from toggle</param>
        public void HideObjects(bool value)
        {
            foreach (var mesh in objectsToHide)
            {
                mesh.enabled = value;
            }
        }
    }
}