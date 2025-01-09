using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsForImagesAndVideos
{
    public class LoadImagesToScrollView : MonoBehaviour
    {
        [SerializeField] private ImagesAndVideosStorage storage;
        [SerializeField] private GameObject representation;
        private const int Height = 75;
        private const int Width = 75;

        /// <summary>
        /// Creates buttons for the images in image and video storage and enters them in the scroll view;
        /// </summary>
        private void Start()
        {
            for (var i = 0; i < storage.Images.Count; i++)
            {
                var button = Instantiate(representation, transform);
                button.SetActive(true);
                var imageButton = button.AddComponent<ImageButton>();
                imageButton.position = i;
                imageButton.storage = storage;
                var buttonScript = button.GetComponent<Button>();
                buttonScript.onClick.AddListener(imageButton.ImageClicked);
                var imageComponent = button.GetComponent<Image>();
                imageComponent.sprite = CreateSprite.CreateSpriteFromImage(storage.Images[i],Height,Width);
                imageButton.image = imageComponent;
            }
        }
    }
}
