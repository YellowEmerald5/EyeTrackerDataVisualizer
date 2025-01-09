using UnityEngine;

namespace ScriptsForImagesAndVideos
{
    public static class CreateSprite
    {
        /// <summary>
        /// Creates a sprite from a 2D texture
        /// </summary>
        /// <param name="image">2D texture to make a sprite from</param>
        /// <param name="height">Height of the sprite</param>
        /// <param name="width">Width of the sprite</param>
        /// <returns>Image as a sprite</returns>
        public static Sprite CreateSpriteFromImage(Texture2D image, int height, int width)
        {
            var ratioHeight = 1f;
            var ratioWidth = 1f;
            if (image.height > image.width)
            {
                ratioHeight = (float)image.width / image.height;
                ratioWidth = 1;
            }else if (image.height < image.width)
            {
                ratioWidth = (float)image.height / image.width;
                ratioHeight = 1;
            }
            var w = (int) (width * ratioWidth);
            var h = (int) (height * ratioHeight);
            var rec = new Rect(0, 0, w, h);
            var rt = new RenderTexture(w, h, 24);
            RenderTexture.active = rt;
            Graphics.Blit(image, rt);
            var result = new Texture2D(w, h);
            result.ReadPixels(new Rect(0, 0, w, h), 0, 0);
            result.Apply();
            var sprite = Sprite.Create(result, rec, new Vector2(0, 0), 0.1f);
            return sprite;
        }
    }
}