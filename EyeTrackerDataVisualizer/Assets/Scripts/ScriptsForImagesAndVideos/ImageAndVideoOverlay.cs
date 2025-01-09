using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsForImagesAndVideos
{
    public class ImageAndVideoOverlay : MonoBehaviour
    {
        [SerializeField] private ImagesAndVideosStorage storage;
        private List<Sprite> _sprites;
    }
}
