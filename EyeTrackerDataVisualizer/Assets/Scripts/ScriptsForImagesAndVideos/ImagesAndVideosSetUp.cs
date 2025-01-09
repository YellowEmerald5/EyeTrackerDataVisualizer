using System;
using System.Collections.Generic;
using System.Linq;
using GameEventScripts;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ScriptsForImagesAndVideos
{
    public class ImagesAndVideosSetUp : MonoBehaviour
    {
        [SerializeField] private ImagesAndVideosStorage storage;
        [SerializeField] private GameObject videoPlayer;
        [SerializeField] private GameObject image;
        [SerializeField] private GameObject toggleImagesAndVideos;
        [SerializeField] private Transform imagesAndVideosScrollViewContent;
        [SerializeField] private GameEvent nextImage;
        [SerializeField] private GameEvent previousImage;
        [SerializeField] private Slider transparencySlider;
        
        private void Start()
        {
            if (storage.SelectedVideo != null)
            {
                var video = storage.SelectedVideo;
                var videoObject = Instantiate(videoPlayer, transform);
                videoObject.SetActive(true);
                var videoPlayerScript = videoObject.GetComponent<VideoPlayer>();
                videoPlayerScript.clip = video;
                var toggleOnOff = videoObject.AddComponent<ToggleOnOff>();
                var toggleObject = Instantiate(toggleImagesAndVideos,imagesAndVideosScrollViewContent);
                toggleObject.SetActive(true);
                var toggle = toggleObject.GetComponent<Toggle>();
                var text = toggleImagesAndVideos.GetComponentInChildren<Text>();
                text.text = video.name;
                toggle.onValueChanged.AddListener(toggleOnOff.Toggle);
            }

            if (storage.SelectedImages.Count <= 0) return;
            var rect = image.GetComponent<RectTransform>().rect;
            var spritesList = storage.SelectedImages.Select(img => CreateSprite.CreateSpriteFromImage(img, (int) rect.height, (int) rect.width)).ToList();
            var control = image.AddComponent<ImageSequenceControl>();
            control.images = spritesList;
            var img = image.GetComponent<Image>();
            control.display = img;
            var transparency = gameObject.AddComponent<TransparencyControl>();
            img.color = new Color(1, 1, 1, 0.5f);
            transparency.images.Add(img);
            transparencySlider.onValueChanged.AddListener(transparency.ChangeTransparency);
            var toggleForImages = Instantiate(toggleImagesAndVideos,imagesAndVideosScrollViewContent);
            var togg = toggleForImages.GetComponent<Toggle>();
            toggleForImages.SetActive(true);
            var toggleScript = image.AddComponent<ToggleOnOff>();
            togg.onValueChanged.AddListener(toggleScript.Toggle);
            var imageToggleText = toggleForImages.GetComponentInChildren<Text>();
            imageToggleText.text = "Images";
            control.Begin();
            AddGameEventListener(image,nextImage,control.NextImage);
            AddGameEventListener(image,previousImage,control.PreviousImage);
        }

        private static void AddGameEventListener(GameObject obj, GameEvent gameEvent,UnityAction action)
        {
            var listener = obj.AddComponent<GameEventListener>();
            listener.gameEvent = gameEvent;
            var ev = new UnityEvent();
            ev.AddListener(action);
            listener.response = ev;
            listener.RegisterListener();
        }
    }
}