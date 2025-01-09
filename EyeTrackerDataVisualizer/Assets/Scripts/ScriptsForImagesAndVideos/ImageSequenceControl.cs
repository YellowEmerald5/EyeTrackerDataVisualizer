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

        public void Begin()
        {
            display.sprite = images[_i];
        }

        public void NextImage()
        {
            if (_i >= images.Count-1) return;
            _i++;
            display.sprite = images[_i];
        }

        public void PreviousImage()
        {
            if (_i <= 0) return;
            _i--;
            display.sprite = images[_i];
        }
    }
}