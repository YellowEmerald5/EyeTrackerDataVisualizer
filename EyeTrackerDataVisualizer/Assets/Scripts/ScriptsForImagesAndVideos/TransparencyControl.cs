using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScriptsForImagesAndVideos
{
    public class TransparencyControl : MonoBehaviour
    {
        public List<Image> images = new ();

        /// <summary>
        /// Changes the transparency of the images based on given value
        /// </summary>
        /// <param name="value">Slider value</param>
        public void ChangeTransparency(float value)
        {
            foreach (var image in images)
            {
                image.color = new Color(1, 1, 1, value);
            }
        }
    }
}