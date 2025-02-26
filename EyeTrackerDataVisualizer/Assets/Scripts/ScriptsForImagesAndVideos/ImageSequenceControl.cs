using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsForImagesAndVideos
{
    public class ImageSequenceControl : MonoBehaviour
    {
        public List<Sprite> images;
        public Image display;
        private int _i;

        /// <summary>
        /// Sets the first image in the overlay
        /// </summary>
        public void Begin()
        {
            display.sprite = images[_i];
        }

        /// <summary>
        /// Changes to the next image in the list
        /// </summary>
        public void NextImage()
        {
            if (_i >= images.Count-1) return;
            _i++;
            display.sprite = images[_i];
        }

        
        /// <summary>
        /// Changes to the previous image in the list
        /// </summary>
        public void PreviousImage()
        {
            if (_i <= 0) return;
            _i--;
            display.sprite = images[_i];
        }
    }
}