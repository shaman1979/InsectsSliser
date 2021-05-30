using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public static class TextExtensions
    {
        public static void SetText(this Text textComponent, string text)
        {
            textComponent.text = text;
        }

        public static string GetText(this Text textComponent)
        {
            return textComponent.text;
        }
    }
}
