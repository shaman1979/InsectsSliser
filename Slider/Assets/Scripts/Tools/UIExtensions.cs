using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public static class UIExtensions
    {
        public static float GetFade(this Graphic graphic)
        {
            return graphic.color.a;
        }
        
        public static void SetFade(this Graphic graphic, float fade)
        {
            var color = graphic.color;
            color.a = fade;
            graphic.color = color;
        }

        public static Color GetColor(this Graphic graphic)
        {
            return graphic.color;
        }

        public static void SetColor(this Graphic graphic, Color color)
        {
            graphic.color = color;
        }
    }
}