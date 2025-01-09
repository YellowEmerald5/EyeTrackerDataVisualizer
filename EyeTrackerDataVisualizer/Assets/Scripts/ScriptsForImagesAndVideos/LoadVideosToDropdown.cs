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

        private void Start()
        {
            storage.SelectedVideo = storage.Videos.First();
            foreach (var video in storage.Videos)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(video.name));
            }
        }

        public void SelectedVideo(int value)
        {
            storage.SelectVideo(value-1);
        }

    }
}
