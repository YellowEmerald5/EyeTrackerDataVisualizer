using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsForImagesAndVideos
{
    public class ImageButton : MonoBehaviour
    {
        public int position;
        private int _selectedPosition;
        public ImagesAndVideosStorage storage;
        private bool _selected;
        public Image image;
        private int _previousCount;

        /// <summary>
        /// Adds or removes the image from SelectedImages
        /// </summary>
        public void ImageClicked()
        {
            if (!_selected)
            {
                _selectedPosition = storage.SelectImage(position);
                image.color = new Color(1, 1, 1, 0.25f);
                _previousCount = storage.SelectedImages.Count;
                _selected = true;
            }
            else
            {
                var currentCount = storage.SelectedImages.Count;
                if (currentCount < _previousCount)
                {
                    _selectedPosition -= _previousCount - currentCount;
                }
                storage.DeselectImage(_selectedPosition);
                image.color = new Color(1, 1, 1, 1);
                _selected = false;
            }
            
        }
    }
}