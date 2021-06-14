using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public static class UIExtensions
    {
        public static float GetFade(this Image image)
        {
            return image.color.a;
        }

        public static float GetFade(this Text text)
        {
            return text.color.a;
        }

        public static void SetFade(this Image image, float fade)
        {
            var color = image.color;
            color.a = fade;
            image.color = color;
        }

        public static void SetFade(this Text text, float fade)
        {
            var color = text.color;
            color.a = fade;
            text.color = color;
        }

        public static Color GetColor(this Image image)
        {
            return image.color;
        }

        public static Color GetColor(this Text text)
        {
            return text.color;
        }

        public static void SetColor(this Image image, Color color)
        {
            image.color = color;
        }

        public static void SetColor(this Text text, Color color)
        {
            text.color = color;
        }
    }
}