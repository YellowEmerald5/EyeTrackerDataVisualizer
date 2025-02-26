using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ScriptsForImagesAndVideos
{
    public class LoadVideosToDropdown : MonoBehaviour
    {
        [SerializeField] private ImagesAndVideosStorage storage;
        [SerializeField] private TMP_Dropdown dropdown;

        /// <summary>
        /// Loads the videos into the video dropdown for selection
        /// </summary>
        private void Start()
        {
            storage.SelectedVideo = storage.Videos.First();
            foreach (var video in storage.Videos)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(video.name));
            }
        }

        /// <summary>
        /// Adds the selected video to the storage
        /// </summary>
        /// <param name="value"></param>
        public void SelectedVideo(int value)
        {
            storage.SelectVideo(value-1);
        }

    }
}
