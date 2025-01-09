using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ImageAndVideoStorage", menuName = "ScriptableObjects/ImageAndVideoStorage", order = 0)]
    public class ImagesAndVideosStorage : ScriptableObject
    {
        public List<Texture2D> Images = new ();
        public List<Texture2D> SelectedImages = new();
        public List<VideoClip> Videos = new ();
        public VideoClip SelectedVideo;

        /// <summary>
        /// Adds an image that is clicked on to selected images
        /// </summary>
        /// <param name="position">Position of the image in the Images list</param>
        /// <returns>Position of image in SelectedImages</returns>
        public int SelectImage(int position)
        {
            SelectedImages.Add(Images[position]);
            return SelectedImages.Count - 1;
        }
        
        /// <summary>
        /// Removes an image that is clicked on from SelectedImages
        /// </summary>
        /// <param name="position">Position of the image in the SelectedImages list</param>
        public void DeselectImage(int position)
        {
            SelectedImages.RemoveAt(position);
        }

        /// <summary>
        /// Adds a video that is clicked on to SelectedVideos
        /// </summary>
        /// <param name="position">Position of the video in the Videos list</param>
        public void SelectVideo(int position)
        {
            SelectedVideo = Videos[position];
        }

        /// <summary>
        /// Removes all selected images and videos from the scriptable object. Not used outside the editor
        /// </summary>
        public void ResetStorage()
        {
            SelectedImages = new List<Texture2D>();
            SelectedVideo = null;
        }
    }
}